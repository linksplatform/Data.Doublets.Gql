using Platform.Data.Doublets.Memory.Split.Generic;
using Platform.Memory;
using System;

namespace Platform.Data.Doublets.Gql.Server
{
    public class Data
    {
        public static string DefaultDatabaseFileName = "db.links";
        private static SplitMemoryLinks<ulong>? _disposableLinks;

        public static ILinks<ulong> CreateLinks()
        {
            // var disposableLinks = new UnitedMemoryLinks<ulong>(new FileMappedResizableDirectMemory(DefaultDatabaseFileName), UnitedMemoryLinks<ulong>.DefaultLinksSizeStep, new LinksConstants<ulong>(enableExternalReferencesSupport: true), IndexTreeType.Default);
            _disposableLinks = new SplitMemoryLinks<ulong>(new FileMappedResizableDirectMemory(DefaultDatabaseFileName), new FileMappedResizableDirectMemory(DefaultDatabaseFileName + ".index"), SplitMemoryLinks<ulong>.DefaultLinksSizeStep, new LinksConstants<ulong>(true));
            return new SynchronizedLinks<ulong>(_disposableLinks.DecorateWithAutomaticUniquenessAndUsagesResolution());
        }

        public static void DisposeLinks()
        {
            try
            {
                _disposableLinks?.Dispose();
                _disposableLinks = null;
            }
            catch (Exception)
            {
                // Suppress exceptions during disposal to prevent AccessViolationException
            }
        }
    }
}
