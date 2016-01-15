using Microsoft.VisualStudio.Text.Tagging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text.Classification;

namespace VarIdentifier.var
{
    [Export(typeof(IViewTaggerProvider))]
    [ContentType("Text")]
    [ContentType("projection")]
    [TagType(typeof(IntraTextAdornmentTag))]
    class VarAdornerTaggerProvider : IViewTaggerProvider
    {
        [Import]
        public IBufferTagAggregatorFactoryService BufferTagAggregator { get; set; }

        [Import]
        public IQuickInfoBroker QuickInfo { get; set; }

        //[Import]
        public IQuickInfoSourceProvider QuickInfoSourceProvider { get; set; }

        [Import]
        public IClassifierAggregatorService Classifier { get; set; }

        public ITagger<T> CreateTagger<T>(ITextView textView, ITextBuffer buffer) where T : ITag
        {
            if (buffer != textView.TextBuffer)
            {
                return null;
            }

            return (ITagger<T>)VarAdornerTagger.GetTagger((IWpfTextView)textView, new Lazy<ITagAggregator<VarTag>>(() => BufferTagAggregator.CreateTagAggregator<VarTag>(textView.TextBuffer)), QuickInfo);
        }
    }
}
