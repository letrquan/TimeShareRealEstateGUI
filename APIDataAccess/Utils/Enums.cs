using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccess.Utils
{
    #region filter order
    public enum Month
    {
        January = 1,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December,
    }
    public enum SortOrder
    {
        Ascending,
        Descending,
    }

    public enum AccountOrderFilter
    {
        AccountId,
        FirstName,
        LastName,
        FullName,
        Phone,
        City,
        State,
        Country,
        Address,
        Role,
        Email,
        Password,
        Status,
        TotalRevenue,
        TotalCommission,

    }

    public enum ProjectOrderFilter
    {
        ProjectId,
        ProjectName,
        PriorityType,
        ProjectCode,
        TotalSlot,
        StartDate,
        EndDate,
        Status,
        RegistrationEndDate,
        RegistrationOpeningDate,
        TotalRevenue
    }

    public enum DepartmentFilter
    {
        DepartmentId,
        DepartmentName,
        OwnerId,
        Address,
        City,
        State,
        Country,
        Floors,
        Price,
        ConstructionType,
        Description,
        Status,
        Capacity,
        TotalRevenue,
    }
    #endregion
    public enum AccountRole
    {
        NULL,
        ADMIN,
        STAFF,
        CUSTOMER
    }

    public enum AccountStatus
    {
        DISABLED, //Tắt
        INACTIVE,
        ACTIVE,
        SUSPENDED, //tạm ngưng
        LOCKED,
        PENDING_ACTIVATION, //Chờ kích hoạt
        CLOSED,
        RESTRICTED, //Hạn chế
        EXPIRED, //Hết hạn
        PENDING_APPROVAL, //Chờ duyệt

    }

    public enum ProjectStatus
    {
        CANCELLED,
        INACTIVE,
        ACTIVE,
        SUSPENDED, //đình chỉ 
        CLOSURE, //kết thúc
        PLANNING, //lập kế hoăchj
        INITIATION //khởi tạo
    }

    public enum PriorityTypeProject
    {
        SEQUENTIAL,
        RANDOM
    }

    public enum AvailableStatus
    {
        UNAVAILABLE,
        AVAILABLE,
        BUSY,
        FREE,
        BOOKED,
        RESERVED,
        PENDING_APPROVAL,
    }

    public enum ContractType
    {
        MONTHLY_CONTRACT, //hàng tháng
        QUARTERLY_CONTRACT, //hàng quý
        ANNUAL_CONTRACT, //hàng năm
        ONE_TIME_PAYMENT, //1 lần
        INSTALLMENT_PAYMENT, //trả góp
        PREPAID_CONTRACT, // trả trước
        POSTPAID_CONTRACT, // trả sau
    }

    public enum ContractStatus
    {
        INACTIVE,
        ACTIVE,
        EXPIRED, //Hết hạn
        RENEWED, //Gia hạn
        TERMINATED, //Chấm dứt
    }

    public enum CustomerRequestType
    {
        REPAIR, //sửa chữa
        CLEANING_SERVICE, //vệ sinh
        INTERNET_CONNECTION, // kết nối internet
        ISSUE_RESOLUTION, //giải quyết vấn đề
        CONTRACT_TERMINATION, // chấm dứt hợp đồng
    }

    public enum CustomerRequestStatus
    {
        OPEN, //Yêu cầu đã được đưa ra và đang chờ xử lý.
        IN_PROGRESS, //Yêu cầu đang được xử lý hoặc sửa chữa.
        RESOLVED, //Yêu cầu đã được hoàn thành và giải quyết.
        CLOSED, //Yêu cầu đã được xử lý và đóng lại.
    }

    public enum DepartmentConstructionType //dạng mô hình như homestay,vv
    {
        HOMESTAY,
        RESORT,
        BOUTIQUE_HOTEL, //Khách sạn nhỏ với thiết kế và dịch vụ độc đáo, tập trung vào trải nghiệm cá nhân.
        MOTEL,
        INN, //Tương tự như guesthouse, thường có không gian ấm cúng và dịch vụ cá nhân.
        VACATION_RENTAL, //Cung cấp chỗ ở tư nhân, thường là nhà, căn hộ hoặc biệt thự cho thuê ngắn hạn
        GUESTHOUSE, //Nhà nghỉ nhỏ, thân thiện, thường do gia đình vận hành.
        LODGE, //Nơi chỗ ở thoải mái, thường tập trung ở các khu vực núi cao hoặc thiên nhiên.
        COTTAGE, //Nhà nghỉ nhỏ thường nằm ở vùng nông thôn, có thiết kế truyền thống.
        APARTMENT_HOTEL,//Cung cấp căn hộ có đầy đủ tiện nghi như khách sạn, nhưng với không gian sống như ở nhà.
        BUNGALOW, //Nhà ở một tầng thấp, thường có mái lợp hình cạnh.
        CHALET, //Nhà gỗ nằm ở khu vực núi cao, thường được sử dụng như nơi nghỉ dưỡng.
        COUNTRY_HOUSE, //Nhà ở ở khu vực nông thôn, thường có diện tích lớn và thiết kế truyền thống.
    }

    public enum DepartmentStatus
    {
        INACTIVE,
        ACTIVE,
        UNDER_CONSTRUCTION, //Đang xây dựng 
        EXPANSION, //mở rộng
        MERGED, //Đã sáp nhập
        CLOSED, // Đóng 
        PENDING_APPROVAL, //Chờ duyệt
        UNDER_REVIEW, //Đang xem xét
        REORGANIZED, //Đã tổ chức lại
        TEMPORARY_SUSPENSION, //Tạm ngưng tạm thời
    }

    public enum FacilityType
    {
        AIR = 0,
        TV = 1,
        WASHING_MACHINE = 2,
        SECURITY = 3,
        CAMERA = 4,
        SINK = 5,
        CHAIR = 6,
        LOCKER = 7,
        BED = 8,
        TABLE = 9,

    }

    public enum OwnerStatus
    {
        INACTIVE,
        ACTIVE,
        PENDING_APPROVAL,
        APPROVED,
        REJECTED, //bị từ chối
        SUSPENDED, //Đình chỉ
        TERMINATED, // Chấm dứt
        UNDER_REVIEW, //đang xem xét
        PENDING_RENEWAL, //Chờ gia hạn
        EXPIRED, //đã gia hạn
    }

    public enum ReservationStatus
    {
        DISABLE,
        ACTIVE,
        CONFIRMED, //XÁC NHẬN 3
        PENDING, //Chờ xử lý 1
        CANCELLED,
        COMPLETED,
        WAITLISTED, //Đang đợi xác nhận 2
        CHECKED_IN, //Đã nhận phòng
        CHECKED_OUT, //Đã trả phòng
        IN_PROGRESS, //Chờ thanh toán 4
        NO_SHOW, //Khách không xuất hiện
        ON_HOLD, //Đã giữ chỗ 5
    }

    public enum UsageHistoryStatus
    {
        CHECKED_IN, //Đã nhận phòng
        CHECKED_OUT, //Đã trả phòng
        NO_SHOW, //Khách không xuất hiện
        WAITLISTED,// Đang chờ đợi
    }

    public enum UsageRightStatus
    {
        DISABLE,
        ACTIVE,
        INACTIVE,
        EXPIRED, //hết hạn
        REVOKED, //thu hồi
        LIMITED, //hạn chế
        CUSTOM, //tÙY chỉnh theo yêu cầu 
    }

}
