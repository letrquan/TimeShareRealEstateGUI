using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccess.DTO.RequestModels
{
    public class StaffOfProjectRequestModel
    {
        [RegularExpression(@"^(?=.*[0-9])\d+$", ErrorMessage = "Staff Id is Invalid!")]
        public int StaffId { get; set; }
        [RegularExpression(@"^(?=.*[0-9])\d+$", ErrorMessage = "Project Id Id is Invalid!")]
        public int ProjectId { get; set; }
    }
}
