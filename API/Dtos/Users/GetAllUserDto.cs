namespace API.Dtos.Users
{
    public class GetAllUserDto
    {
        public int userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string account { get; set; }
        public string role { get; set; }
        public string email { get; set; }
        public string contact { get; set; }
    }
}