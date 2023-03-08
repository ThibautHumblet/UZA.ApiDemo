namespace TestWebApi.Models.Crud
{
    public class SaveEmployeeIn
    {
        public string Lastname { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;
        public DateTime Birthdate { get; set; } = new DateTime();
        public string Sex { get; set; } = "U";
    }
}
