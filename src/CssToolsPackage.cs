using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;

namespace CssTools
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version, IconResourceID = 400)]
    [Guid("8a85e089-0667-415f-a01c-f2f7564e7bac")]
    public sealed class CssToolsPackage : AsyncPackage
    {
    }
}
