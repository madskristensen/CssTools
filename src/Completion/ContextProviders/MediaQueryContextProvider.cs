using Microsoft.CSS.Core.Parser;
using Microsoft.CSS.Core.TreeItems.AtDirectives;
using Microsoft.CSS.Editor.Completion;
using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace CssTools
{
    [Export(typeof(ICssCompletionContextProvider))]
    [Name("MediaQueryCompletionContextProvider")]
    internal class MediaQueryCompletionContextProvider : ICssCompletionContextProvider
    {
        public IEnumerable<Type> ItemTypes
        {
            get
            {
                return new Type[] { typeof(MediaQuery), };
            }
        }

        public CssCompletionContext GetCompletionContext(ParseItem item, int position)
        {
            var token = item.StyleSheet.ItemBeforePosition(position);

            // Don't handle expressions. MediaFeatureContextProvider.cs does that
            if (token.FindType<MediaExpression>() != null)
                return null;

            return new CssCompletionContext((CssCompletionContextType)612, token.Start, token.Length, null);
        }
    }
}
