using Microsoft.VisualStudio.Web.BrowserLink;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;

namespace CssTools
{
    [Export(typeof(IBrowserLinkExtensionFactory))]
    public class BrowserInfoFactory : IBrowserLinkExtensionFactory
    {
        public BrowserLinkExtension CreateExtensionInstance(BrowserLinkConnection connection)
        {
            return new BrowserInfo();
        }

        public string GetScript()
        {
            using (Stream stream = GetType().Assembly.GetManifestResourceStream("CssTools.BrowserLink.BrowserInfo.BrowserInfo.js"))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }

    public class BrowserInfo : BrowserLinkExtension
    {
        private static Dictionary<string, BrowserCap> _browserCapDictionary = new Dictionary<string, BrowserCap>();
        private BrowserLinkConnection _current;

        public static Dictionary<string, BrowserCap> BrowserCapDictionary { get { return _browserCapDictionary; } }

        public override void OnConnected(BrowserLinkConnection connection)
        {
            _current = connection;

            base.OnConnected(connection);
        }

        public override void OnDisconnecting(BrowserLinkConnection connection)
        {
            if (_browserCapDictionary.ContainsKey(connection.ConnectionId))
                _browserCapDictionary.Remove(connection.ConnectionId);

            base.OnDisconnecting(connection);
        }

        [BrowserLinkCallback] // This method can be called from JavaScript
        public void CollectInfo(int width, int height)
        {
            var browserCap = new BrowserCap
            {
                Name = _current.AppName,
                Width = width,
                Height = height,
            };

            _browserCapDictionary[_current.ConnectionId] = browserCap;
        }
    }

    public class BrowserCap
    {
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}