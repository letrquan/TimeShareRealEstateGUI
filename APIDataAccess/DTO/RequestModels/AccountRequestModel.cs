using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccess.DTO.RequestModels
{
    public class AccountRequestModel
    {

        [RegularExpression(@"^\D{1,10}$", ErrorMessage = "Max length 10 character, don't contain number!")]
        public string? FirstName { get; set; }
        [RegularExpression(@"^\D{1,10}$", ErrorMessage = "Max length 10 character, don't contain number!")]
        public string? LastName { get; set; }
        [RegularExpression(@"^\D{2,150}$", ErrorMessage = "Max length 150 character, don't contain number!")]
        public string? FullName { get; set; }
        [Phone(ErrorMessage = "Phone is Invalidate!")]
        public string? Phone { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public int? Role { get; set; }

        [EmailAddress(ErrorMessage = "Email is Invalidate!")]
        public string Email { get; set; }

        [PasswordPropertyText]
        public string Password { get; set; }
        public int? Status { get; set; }

    }
}
