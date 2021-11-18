using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Task5_Nix.ViewModels
{
    public class UserRegistrationModel : IRegistrationData
    {
        private string defRole = "user";

        public string UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Пароль должен быть минимум 8 символов и состоять из чисел и букв")]
        public string Password { get; set; }

        public string Role
        {
            get
            {
                return defRole;
            }
            set
            {
                defRole = value;
            }
        }
        [Required]
        public string RepeatPassword { get; set; }

        [Required]
        [StringLength(2, ErrorMessage = "Размер строки серии пасспорта не должен превышать 2 букв")]
        public string PassportSeries { get; set; }
        [Required]
        public string PassportNum { get; set; }
    }
}
