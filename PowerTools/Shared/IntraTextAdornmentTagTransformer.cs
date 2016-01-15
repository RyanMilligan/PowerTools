using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VarIdentifier.Shared
{
    abstract class IntraTextAdornmentTagTransformer<TTag, TAdorner> : IntraTextAdornmentTagger<TTag, TAdorner>
        where TTag : ITag
        where TAdorner : UIElement
    {
        protected readonly ITagAggregator<TTag> dataTagger;
        protected readonly PositionAffinity? adornmentAffinity;


        /// <param name="adornmentAffinity">Determines whether adornments based on data tags with zero-length spans 
        /// will stick with preceding or succeeding text characters.</param> 
        protected IntraTextAdornmentTagTransformer(IWpfTextView view, ITagAggregator<TTag> dataTagger, PositionAffinity adornmentAffinity = PositionAffinity.Successor)
            : base(view)
        {
            this.adornmentAffinity = adornmentAffinity;
            this.dataTagger = dataTagger;


            this.dataTagger.TagsChanged += HandleDataTagsChanged;
        }


        protected override IEnumerable<Tuple<SnapshotSpan, PositionAffinity?, TTag>> GetAdornmentData(NormalizedSnapshotSpanCollection spans)
        {
            if (spans.Count == 0)
                yield break;


            ITextSnapshot snapshot = spans[0].Snapshot;


            foreach (IMappingTagSpan<TTag> dataTagSpan in dataTagger.GetTags(spans))
            {
                NormalizedSnapshotSpanCollection dataTagSpans = dataTagSpan.Span.GetSpans(snapshot);


                // Ignore data tags that are split by projection. 
                // This is theoretically possible but unlikely in current scenarios. 
                if (dataTagSpans.Count != 1)
                    continue;


                SnapshotSpan span = dataTagSpans[0];


                PositionAffinity? affinity = span.Length > 0 ? null : adornmentAffinity;


                yield return Tuple.Create(span, affinity, dataTagSpan.Tag);
            }
        }


        private void HandleDataTagsChanged(object sender, TagsChangedEventArgs args)
        {
            var changedSpans = args.Span.GetSpans(view.TextBuffer.CurrentSnapshot);
            InvalidateSpans(changedSpans);
        }


        public virtual void Dispose()
        {
            dataTagger.Dispose();
        }

    }
}
