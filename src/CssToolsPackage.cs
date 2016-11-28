using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Win32;

namespace CssTools
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", Vsix.Version, IconResourceID = 400)]
    [Guid(CssToolsPackage.PackageGuidString)]
    public sealed class CssToolsPackage : Package
    {
        public const string PackageGuidString = "8a85e089-0667-415f-a01c-f2f7564e7bac";

        public CssToolsPackage()
        {
        }

        protected override void Initialize()
        {
            base.Initialize();
        }
    }
}
