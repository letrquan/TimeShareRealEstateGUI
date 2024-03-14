namespace APIDataAccess.DTO.ResponseModels
{
    public class FacilityViewModel
    {
        public int? FacilityId { get; set; }
        public string? FacilityName { get; set; }
        public string? Description { get; set; }
        public int? FacilityType { get; set; }
        public int? DepartmentId { get; set; }
    }
}