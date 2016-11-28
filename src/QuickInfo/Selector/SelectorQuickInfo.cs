using Microsoft.CSS.Core.Parser;
using Microsoft.CSS.Core.TreeItems.Selectors;
using Microsoft.CSS.Editor.Document;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.Web.Languages.Less.Parser.Mixin;
using Microsoft.Web.Languages.Scss.Parser.MixinDirective;
using System;
using System.Collections.Generic;

namespace CssTools
{
    internal class SelectorQuickInfo : IQuickInfoSource
    {
        private ITextBuffer _buffer;

        public SelectorQuickInfo(ITextBuffer subjectBuffer)
        {
            _buffer = subjectBuffer;
        }

        public void AugmentQuickInfoSession(IQuickInfoSession session, IList<object> qiContent, out ITrackingSpan applicableToSpan)
        {
            applicableToSpan = null;

            if (session == null || qiContent == null)
                return;

            // Map the trigger point down to our buffer.
            SnapshotPoint? point = session.GetTriggerPoint(_buffer.CurrentSnapshot);

            if (!point.HasValue)
                return;

            var tree = CssEditorDocument.FromTextBuffer(_buffer);
            ParseItem item = tree.StyleSheet.ItemBeforePosition(point.Value.Position);

            if (item == null || !item.IsValid)
                return;

            Selector sel = item.FindType<Selector>();

            if (sel == null)
                return;

            // Mixins don't have specificity
            if (sel.SimpleSelectors.Count == 1)
            {
                var subSelectors = sel.SimpleSelectors[0].SubSelectors;

                if (subSelectors.Count == 1 &&
                    subSelectors[0] is LessMixinDeclaration &&
                    subSelectors[0] is ScssMixinDeclaration)
                    return;
            }

            applicableToSpan = _buffer.CurrentSnapshot.CreateTrackingSpan(item.Start, item.Length, SpanTrackingMode.EdgeNegative);

            if (_buffer.ContentType.DisplayName.Equals("css", StringComparison.OrdinalIgnoreCase))
            {
                qiContent.Add(GenerateContent(sel));
                return;
            }
        }

        private static string GenerateContent(Selector sel)
        {
            SelectorSpecificity specificity = new SelectorSpecificity(sel);

            return "Selector specificity:\t\t" + specificity.ToString().Trim();
        }

        public void Dispose() { }
    }
}
