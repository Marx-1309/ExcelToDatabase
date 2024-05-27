using System.ComponentModel.DataAnnotations;

namespace HrGpIntegration.Models.ViewModel
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
