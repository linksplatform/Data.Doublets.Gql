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
