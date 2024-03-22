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
                case 9:
                    return "PENDING_APPROVAL";
                default:
                    return "DISABLED";
            }
        }
        //contruct type to string
        public static string ConstructionTypeToString(int? type)
        {
            switch (type)
            {
                case 1:
                    return "RESORT";
                case 2:
                    return "BOUTIQUE_HOTEL";
                case 3:
                    return "MOTEL";
                case 4:
                    return "INN";
                case 5:
                    return "VACATION_RENTAL";
                case 6:
                    return "GUESTHOUSE";
                case 7:
                    return "LODGE";
                case 8:
                    return "COTTAGE";
                case 9:
                    return "APARTMENT_HOTEL";
                case 10:
                    return "BUNGALOW";
                case 11:
                    return "CHALET";
                case 12:
                    return "COUNTRY_HOUSE";
                default:
                    return "HOMESTAY";
            }
        }
        //department status to string
        public static string DepartmentStatusToString(int? status)
        {
            switch (status)
            {
                case 0:
                    return "INACTIVE";
                case 1:
                    return "ACTIVE";
                case 2:
                    return "UNDER_CONSTRUCTION";
                case 3:
                    return "EXPANSION";
                case 4:
                    return "MERGED";
                case 5:
                    return "CLOSED";
                case 6:
                    return "PENDING_APPROVAL";
                case 7:
                    return "UNDER_REVIEW";
                case 8:
                    return "REORGANIZED";
                case 9:
                    return "TEMPORARY_SUSPENSION";
                default:
                    return "DISABLED";
            }
        }
        // reservation status to string
        public static string ReservationStatusToString(int? status)
        {
            switch (status)
            {
                case 1:
                    return "CONFIRMED";
                case 2:
                    return "PENDING";
                case 3:
                    return "CANCELLED";
                case 4:
                    return "COMPLETED";
                case 5:
                    return "WAITLISTED";
                case 6:
                    return "CHECKED_IN";
                case 7:
                    return "CHECKED_OUT";
                case 8:
                    return "IN_PROGRESS";
                case 9:
                    return "NO_SHOW";
                case 10:
                    return "ON_HOLD";
                default:
                    return "ACTIVE";

            }
        }
    }
}
