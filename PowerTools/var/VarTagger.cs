using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VarIdentifier.Shared;

namespace VarIdentifier.var
{
    class VarTagger : RegexTagger<VarTag>
    {
        public VarTagger(ITextBuffer buffer) : base(buffer, new Regex(@"v(?<var>ar)\s+(?<name>[\w\d]+)\s+(?:in|=)", RegexOptions.Compiled | RegexOptions.CultureInvariant))
        {

        }

        [Import]
        public IVsEditorAdaptersFactoryService EditorAdapters { get; set; }

        protected override IEnumerable<Tuple<VarTag, int, int>> TryCreateTagForMatch(Match match)
        {
            yield return CreateMatch(new VarTag(match), match.Groups["name"]);
        }
    }
}
