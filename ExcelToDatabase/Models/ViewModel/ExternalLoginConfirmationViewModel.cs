using System.ComponentModel.DataAnnotations;

namespace HrGpIntegration.Models.ViewModel
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
