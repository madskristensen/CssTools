using Microsoft.CSS.Core.TreeItems;
using Microsoft.CSS.Editor.Completion;
using Microsoft.VisualStudio.Utilities;
using Microsoft.Web.Editor.Completion;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace CssTools
{
    [Export(typeof(ICssCompletionListFilter))]
    [Name("Inherit/Initial Filter")]
    internal class HideInheritInitialCompletionListFilter : ICssCompletionListFilter
    {
        public void FilterCompletionList(IList<CssCompletionEntry> completions, CssCompletionContext context)
        {
            if (context.ContextType != CssCompletionContextType.PropertyValue)
                return;

            // Only show inherit/initial/unset on the "all" property
            Declaration dec = context.ContextItem.FindType<Declaration>();

            if (dec != null && dec.PropertyNameText == "all")
                return;

            foreach (CssCompletionEntry entry in completions)
            {
                if (entry.DisplayText == "initial" || entry.DisplayText == "inherit" || entry.DisplayText == "unset")
                {
                    entry.FilterType = CompletionEntryFilterTypes.NeverVisible;
                }
            }
        }
    }
}