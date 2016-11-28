using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;

namespace CssTools
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", Vsix.Version, IconResourceID = 400)]
    [Guid(PackageGuidString)]
    public sealed class CssToolsPackage : Package
    {
        public const string PackageGuidString = "8a85e089-0667-415f-a01c-f2f7564e7bac";

        protected override void Initialize()
        {

        }
    }
}
