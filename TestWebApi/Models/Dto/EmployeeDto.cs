namespace TestWebApi.Models.Dto
{
    public class EmployeeDto
    {
        public string EmployeeCode { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;
        public DateTime Birthdate { get; set; } = new DateTime();
        public string Sex { get; set; } = "U";
        public bool Inactive { get; set; }
    }
}
