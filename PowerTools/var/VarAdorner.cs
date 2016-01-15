using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text.Editor;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace VarIdentifier.var
{
    internal class VarAdorner : ContentControl
    {
        public VarAdorner(IWpfTextView view, VarTag tag, IQuickInfoBroker quickInfo)
        {
            tag = Tag;
            View = view;
            QuickInfo = quickInfo;
        }

        public new VarTag Tag { get; set; }
        public IWpfTextView View { get; set; }
        
        public IQuickInfoBroker QuickInfo { get; set; }
        public IQuickInfoSourceProvider QuickInfoSourceProvider { get; set; }

        public void Update(VarTag newTag)
        {
            Tag = newTag;
            
            var point = newTag.Line.Snapshot.CreateTrackingPoint(newTag.Line.Start + newTag.Info.Groups["var"].Index, Microsoft.VisualStudio.Text.PointTrackingMode.Positive);
            var session = QuickInfo.CreateQuickInfoSession(View, point, false);
            session.PresenterChanged += Session_PresenterChanged;
            session.QuickInfoContent.CollectionChanged += QuickInfoContent_CollectionChanged;
            session.Start();
        }

        private void QuickInfoContent_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void Session_PresenterChanged(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}