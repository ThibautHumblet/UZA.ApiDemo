namespace TestWebApi.Models.Query
{
    public class FindEmployeeQuery
    {
        public string? EmployeeCode { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public DateTime? FromBirthDate { get; set; }
        public DateTime? ToBirthDate { get; set; }
        public string? Sex { get; set; }
        public bool? Inactive { get; set; }
    }
}
