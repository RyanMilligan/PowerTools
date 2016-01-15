using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Text;
using VarIdentifier.Shared;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Language.Intellisense;

namespace VarIdentifier.var
{
    class VarAdornerTagger : IntraTextAdornmentTagTransformer<VarTag, VarAdorner>
    {
        internal static ITagger<IntraTextAdornmentTag> GetTagger(IWpfTextView view, Lazy<ITagAggregator<VarTag>> tag, IQuickInfoBroker quickInfo)
        {
            return view.Properties.GetOrCreateSingletonProperty<VarAdornerTagger>(() => new VarAdornerTagger(view, tag.Value, quickInfo));
        }

        public VarAdornerTagger(IWpfTextView view, ITagAggregator<VarTag> tag, IQuickInfoBroker quickInfo) : base(view, tag)
        {
            QuickInfo = quickInfo;
        }

        public override void Dispose()
        {
            base.Dispose();

            base.view.Properties.RemoveProperty(typeof(VarAdornerTagger));
        }

        
        public IQuickInfoBroker QuickInfo { get; set; }

        protected override VarAdorner CreateAdornment(VarTag data, SnapshotSpan span)
        {
            return new VarAdorner(view, data, QuickInfo);
        }

        protected override bool UpdateAdornment(VarAdorner adornment, VarTag data)
        {
            adornment.Update(data);
            return true;
        }
    }
}
