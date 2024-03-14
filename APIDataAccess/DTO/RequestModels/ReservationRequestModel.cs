using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccess.DTO.RequestModels
{
    public class ReservationRequestModel
    {
        [RegularExpression(@"^(?=.*[0-9])\d+$", ErrorMessage = "Customer Id is Invalid!")]
        public int CustomerId { get; set; }
        [RegularExpression(@"^(?=.*[0-9])\d+$", ErrorMessage = "AvailableTime Id is Invalid!")]
        public int AvailableTimeId { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime ReservationDate { get; set; }
        public decimal? ReservationFee { get; set; }
        public int? Status { get; set; }
    }
}
