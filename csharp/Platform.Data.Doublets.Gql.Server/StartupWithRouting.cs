using GraphQL.Server;
using GraphQL.Server.Ui.Altair;
using GraphQL.Server.Ui.GraphiQL;
using GraphQL.Server.Ui.Playground;
using GraphQL.Server.Ui.Voyager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Platform.Data.Doublets.Gql.Schema;
using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Server
{
    public class StartupWithRouting
    {
        public readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public StartupWithRouting(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) => services.AddRouting()
            .AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    });
            })
            .AddSingleton(sp => Data.CreateLinks())
            .AddSingleton<LinksSchema>()
            .AddGraphQL((options, provider) =>
            {
                options.EnableMetrics = Environment.IsDevelopment();
                var logger = provider.GetRequiredService<ILogger<Startup>>();
                options.UnhandledExceptionDelegate = ctx => logger.LogError("{Error} occurred", ctx.OriginalException.Message);
            })
            .AddDefaultEndpointSelectorPolicy()
            .AddSystemTextJson(deserializerSettings => { }, serializerSettings => { })
            .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = Environment.IsDevelopment())
            .AddWebSockets()
            .AddDataLoader()
            .AddGraphTypes(typeof(LinksSchema));

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseWebSockets();
            app.UseRouting();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQLWebSockets<LinksSchema>();
                endpoints.MapGraphQL<LinksSchema, GraphQLHttpMiddlewareWithLogs<LinksSchema>>(pattern: "/v1/graphql");
                endpoints.MapGraphQLPlayground(new PlaygroundOptions
                {
                    BetaUpdates = true,
                    RequestCredentials = RequestCredentials.Omit,
                    HideTracingResponse = false,
                    EditorCursorShape = EditorCursorShape.Line,
                    EditorTheme = EditorTheme.Light,
                    EditorFontSize = 14,
                    EditorReuseHeaders = true,
                    EditorFontFamily = "Consolas",
                    PrettierPrintWidth = 80,
                    PrettierTabWidth = 2,
                    PrettierUseTabs = true,
                    SchemaDisableComments = false,
                    SchemaPollingEnabled = true,
                    SchemaPollingEndpointFilter = "*localhost*",
                    SchemaPollingInterval = 5000,
                    Headers = new Dictionary<string, object> { ["MyHeader1"] = "MyValue", ["MyHeader2"] = 42},
                    GraphQLEndPoint = "/v1/graphql"
                });
                endpoints.MapGraphQLGraphiQL(new GraphiQLOptions { Headers = new Dictionary<string, string> { ["X-api-token"] = "130fh9823bd023hd892d0j238dh" }, GraphQLEndPoint = "/v1/graphql"});
                endpoints.MapGraphQLAltair(new AltairOptions { Headers = new Dictionary<string, string> { ["X-api-token"] = "130fh9823bd023hd892d0j238dh" }, GraphQLEndPoint = "/v1/graphql"});
                endpoints.MapGraphQLVoyager(new VoyagerOptions { Headers = new Dictionary<string, object> { ["MyHeader1"] = "MyValue", ["MyHeader2"] = 42 }, GraphQLEndPoint = "/v1/graphql"});
            });
        }
    }
}
