using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;

namespace VarIdentifier.Shared
{
    internal interface ITrackingTag : ITag
    {
        ITextSnapshotLine Line { get; set; }
    }
}