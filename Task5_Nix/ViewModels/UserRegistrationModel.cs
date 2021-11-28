using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Task5_Nix.ViewModels
{
    public class UserRegistrationModel : UserLoginModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string RepeatPassword { get; set; }

        [Required]
        [StringLength(2, ErrorMessage = "Размер строки серии пасспорта не должен превышать 2 букв")]
        public string PassportSeries { get; set; }
        [Required]
        public string PassportNum { get; set; }
    }
}
