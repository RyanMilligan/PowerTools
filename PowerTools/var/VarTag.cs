using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using System.Text.RegularExpressions;
using VarIdentifier.Shared;
using Microsoft.VisualStudio.Text;
using System;

namespace VarIdentifier.var
{
    internal class VarTag : ITrackingTag
    {
        public VarTag(Match info)
        {
            Info = info;
        }

        public Match Info { get; set; }

        public ITextSnapshotLine Line
        {
            get; set;
        }
    }
}