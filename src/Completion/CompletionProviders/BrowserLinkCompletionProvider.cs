using Microsoft.CSS.Core.TreeItems;
using Microsoft.CSS.Editor.Completion;
using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace CssTools
{
    [Export(typeof(ICssCompletionProvider))]
    [Name("BrowserLinkCompletionProvider")]
    internal class BrowserLinkCompletionProvider : ICssCompletionListProvider
    {
        public CssCompletionContextType ContextType
        {
            get { return CssCompletionContextType.PropertyValue; }
        }

        public IEnumerable<ICssCompletionListEntry> GetListEntries(CssCompletionContext context)
        {
            Declaration dec = context.ContextItem.FindType<Declaration>();

            if (dec == null || dec.PropertyName == null)
                yield break;

            if (dec.PropertyName.Text.EndsWith("width", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var browser in BrowserInfo.BrowserCapDictionary.Values.OrderByDescending(b => b.Width))
                {
                    string value = browser.Width + "px";
                    yield return new BrowserCompletionListEntry(value, browser.Name);
                }
            }
            else if (dec.PropertyName.Text.EndsWith("height", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var browser in BrowserInfo.BrowserCapDictionary.Values.OrderByDescending(b => b.Height))
                {
                    string value = browser.Height + "px";
                    yield return new BrowserCompletionListEntry(value, browser.Name);
                }
            }
        }
    }
}
