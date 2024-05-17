

using Microsoft.AspNetCore.Mvc.Rendering;

namespace HrGpIntegration.Custom_SelectLists
{
    public class CustomSelectListItem : SelectListItem
    {
        public string? ModelName { get; set; }
    }
}
