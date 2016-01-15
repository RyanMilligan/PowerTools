using Microsoft.VisualStudio.Text.Tagging;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Utilities;

namespace VarIdentifier.var
{
    [Export(typeof(ITaggerProvider))]
    [ContentType("text")]
    [TagType(typeof(VarTag))]
    class VarTaggerProvider : ITaggerProvider
    {
        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            return buffer.Properties.GetOrCreateSingletonProperty(() => new VarTagger(buffer)) as ITagger<T>;
        }
    }
}
