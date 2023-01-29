#nullable disable
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MonitoriOn.Views.AdminSettings
{
    public static class AdminSettingsNavPages
    {
        public static string Index => "Index";
        public static string SetAdmin => "SetAdmin";
        public static string DeleteAdmin => "DeleteAdmin";

        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static string SetAdminNavClass(ViewContext viewContext) => PageNavClass(viewContext, SetAdmin);

        public static string DeleteAdminNavClass(ViewContext viewContext) => PageNavClass(viewContext, DeleteAdmin);

        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["AdminSettingsActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
