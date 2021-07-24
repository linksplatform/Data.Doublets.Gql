using Platform.Collections.Stacks;
using Platform.Converters;
using Platform.Data;
using Platform.Data.Doublets;
using Platform.Data.Doublets.CriterionMatchers;
using Platform.Data.Doublets.Memory;
using Platform.Data.Doublets.Memory.United.Generic;
using Platform.Data.Doublets.Sequences.Converters;
using Platform.Data.Doublets.Sequences.Walkers;
using Platform.Data.Doublets.Unicode;
using Platform.Data.Numbers.Raw;
using Platform.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TLinkAddress = System.UInt64;

namespace LinksStorage
{
    public class Storage
    {
        private readonly TLinkAddress _unicodeSequenceMarker;

        private readonly TLinkAddress _meaningRoot;

        private readonly AddressToRawNumberConverter<TLinkAddress> _addressToNumberConverter;

        private readonly RawNumberToAddressConverter<TLinkAddress> _numberToAddressConverter;

        private readonly IConverter<string, TLinkAddress> _stringToUnicodeSequenceConverter;

        private readonly IConverter<TLinkAddress, string> _unicodeSequenceToStringConverter;

        private readonly UnitedMemoryLinks<UInt64> Links;

        private readonly TLinkAddress _unicodeSymbolMarker;

        private readonly TLinkAddress _setMarker;

        private readonly TLinkAddress _fileMarker;

        private readonly TLinkAddress Any;

        private TLinkAddress GetOrCreateNextMapping(TLinkAddress currentMappingIndex) => Links.Exists(currentMappingIndex) ? currentMappingIndex : Links.CreateAndUpdate(_meaningRoot, Links.Constants.Itself);

        private TLinkAddress GetOrCreateMeaningRoot(TLinkAddress meaningRootIndex) => Links.Exists(meaningRootIndex) ? meaningRootIndex : Links.CreatePoint();
        public Storage(string DBFilename)
        {
            var linksConstants = new LinksConstants<TLinkAddress>(enableExternalReferencesSupport: true);
            var dataMemory = new FileMappedResizableDirectMemory(DBFilename);
            Links = new UnitedMemoryLinks<UInt64>(dataMemory, UnitedMemoryLinks<UInt64>.DefaultLinksSizeStep, linksConstants, IndexTreeType.Default);
            var link = Links.Create();
            link = Links.Update(link, newSource: link, newTarget: link);
            ushort currentMappingLinkIndex = 1;
            Any = Links.Constants.Any;
            _meaningRoot = GetOrCreateMeaningRoot(currentMappingLinkIndex++);
            _unicodeSymbolMarker = GetOrCreateNextMapping(currentMappingLinkIndex++);
            _unicodeSequenceMarker = GetOrCreateNextMapping(currentMappingLinkIndex++);
            _setMarker = GetOrCreateNextMapping(currentMappingLinkIndex++);
            _fileMarker = GetOrCreateNextMapping(currentMappingLinkIndex++);
            _addressToNumberConverter = new AddressToRawNumberConverter<TLinkAddress>();
            _numberToAddressConverter = new RawNumberToAddressConverter<TLinkAddress>();
            var balancedVariantConverter = new BalancedVariantConverter<TLinkAddress>(Links);
            var unicodeSymbolCriterionMatcher = new TargetMatcher<TLinkAddress>(Links, _unicodeSymbolMarker);
            var unicodeSequenceCriterionMatcher = new TargetMatcher<TLinkAddress>(Links, _unicodeSequenceMarker);
            var charToUnicodeSymbolConverter = new CharToUnicodeSymbolConverter<TLinkAddress>(Links, _addressToNumberConverter, _unicodeSymbolMarker);
            var unicodeSymbolToCharConverter = new UnicodeSymbolToCharConverter<TLinkAddress>(Links, _numberToAddressConverter, unicodeSymbolCriterionMatcher);
            var sequenceWalker = new RightSequenceWalker<TLinkAddress>(Links, new DefaultStack<TLinkAddress>(), unicodeSymbolCriterionMatcher.IsMatched);
            _stringToUnicodeSequenceConverter = new CachingConverterDecorator<string, TLinkAddress>(new StringToUnicodeSequenceConverter<TLinkAddress>(Links, charToUnicodeSymbolConverter, balancedVariantConverter, _unicodeSequenceMarker));
            _unicodeSequenceToStringConverter = new CachingConverterDecorator<TLinkAddress, string>(new UnicodeSequenceToStringConverter<TLinkAddress>(Links, unicodeSequenceCriterionMatcher, sequenceWalker, unicodeSymbolToCharConverter));
        }
        public TLinkAddress Convert(string str) => _stringToUnicodeSequenceConverter.Convert(str);

        public string Convert(TLinkAddress address) => _unicodeSequenceToStringConverter.Convert(address);

        public List<Link> GetAllLinks()
        {
            List<Link> links = new() { };
            var query = new Link<UInt64>(index: Any, source: Any, target: Any);
            Links.Each(link =>
            {
                links.Add(new Link(){Id = (long)Links.GetIndex(link), from_id = (long)Links.GetSource(link), to_id = (long)Links.GetTarget(link)});
                return Links.Constants.Continue;
            }, query);
            return links;
        }
    }
}
