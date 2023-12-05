namespace FullStack_API.Models
{
    public class EmployeeJWT
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public EmployeeJWT(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
