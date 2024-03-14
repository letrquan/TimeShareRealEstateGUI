using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccess.DTO.RequestModels
{
    public class UsageRightRequestModel
    {
        [RegularExpression(@"^(?=.*[0-9])\d+$", ErrorMessage = "Customer Id is Invalid!")]
        public int CustomerId { get; set; }
        [RegularExpression(@"^(?=.*[0-9])\d+$", ErrorMessage = "Reservation Id is Invalid!")]
        public int ReservationId { get; set; }
        public int? Status { get; set; }
    }
}
