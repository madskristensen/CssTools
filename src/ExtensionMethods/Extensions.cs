using Microsoft.VisualStudio.Text;
using System;
using System.Linq;
using System.Windows.Media.Imaging;

namespace CssTools
{
    static class Extensions
    {
        ///<summary>Runs a callback when an iamge is fully downloaded, or immediately if the image has already been downloaded.</summary>
        public static void OnDownloaded(this BitmapSource image, Action callback)
        {
            if (image.IsDownloading)
                image.DownloadCompleted += (s, e) => callback();
            else
                callback();
        }

        ///<summary>Replaces a TextBuffer's entire content with the specified text.</summary>
        public static void SetText(this ITextBuffer buffer, string text)
        {
            buffer.Replace(new Span(0, buffer.CurrentSnapshot.Length), text);
        }

        ///<summary>Test the numericality of sequence.</summary>
        public static bool IsNumeric(this string input)
        {
            return input.All(digit => char.IsDigit(digit) || digit.Equals('.'));
        }

        ///<summary>Find the cloumn position in the last line.</summary>
        public static int GetLineColumn(this string text, int targetIndex, int lineNumber)
        {
            var result = targetIndex - text.NthIndexOfCharInString('\n', lineNumber);

            return Math.Max(0, result);
        }

        //<summary>Find the nth occurance of needle in haystack.</summary>.
        public static int NthIndexOfCharInString(this string strHaystack, char charNeedle, int intOccurrenceToFind)
        {
            if (intOccurrenceToFind < 1) return 0;

            int intReturn = -1;
            int count = 0;
            int n = 0;

            while (count < intOccurrenceToFind && (n = strHaystack.IndexOf(charNeedle, n)) != -1)
            {
                n++;
                count++;
            }

            if (count == intOccurrenceToFind) intReturn = n;

            return intReturn;
        }
    }
}
