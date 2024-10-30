using System.ComponentModel.DataAnnotations;

namespace NDISBudget.Areas.Company.Data
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName
        {
            get;
            set;
        }
        [Required]
        [DataType(DataType.Password)]
        public string Password
        {
            get;
            set;
        }
        
        
    }
}
