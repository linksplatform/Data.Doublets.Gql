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
using Platform.Data.Doublets.Memory;
using Platform.Data.Doublets.Memory.United.Generic;
using Platform.Data.Doublets.Memory.Split.Generic;
using Platform.Memory;
using System.Collections.Generic;

namespace Platform.Data.Doublets.Gql.Server
{
    public class Data
    {
        public static string DefaultDatabaseFileName = "db.links";

        public static ILinks<ulong> CreateLinks()
        {
          // var disposableLinks = new UnitedMemoryLinks<ulong>(new FileMappedResizableDirectMemory(DefaultDatabaseFileName), UnitedMemoryLinks<ulong>.DefaultLinksSizeStep, new LinksConstants<ulong>(enableExternalReferencesSupport: true), IndexTreeType.Default);
          var disposableLinks = new SplitMemoryLinks<ulong>(new FileMappedResizableDirectMemory(DefaultDatabaseFileName), new FileMappedResizableDirectMemory(DefaultDatabaseFileName + ".index"), SplitMemoryLinks<ulong>.DefaultLinksSizeStep, new LinksConstants<ulong>(enableExternalReferencesSupport: true));
          return new SynchronizedLinks<ulong>(disposableLinks.DecorateWithAutomaticUniquenessAndUsagesResolution());
        }
    }
}
