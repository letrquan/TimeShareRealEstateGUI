using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccess.Utils
{
    public class DisplayUtils
    {
        public static string RoleToString(int role)
        {
            switch (role)
            {
                case 1:
                    return "ADMIN";
                case 2:
                    return "STAFF";
                default:
                    return "CUSTOMER";
            }
        }
        //status to string
        public static string StatusToString(int status)
        {
            switch (status)
            {
                case 1:
                    return "INACTIVE";
                case 2:
                    return "ACTIVE";
                case 3:
                    return "SUSPENDED";
                case 4:
                    return "LOCKED";
                case 5:
                    return "PENDING_ACTIVATION";
                case 6:
                    return "CLOSED";
                case 7:
                    return "RESTRICTED";
                case 8:
                    return "EXPIRED";
                case 9 :
                    return "PENDING_APPROVAL";
                default:
                    return "DISABLED";
            }
        }
    }
}
