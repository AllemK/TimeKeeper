using System.Collections.Generic;

namespace TimeKeeper.API.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public List<string> Teams { get; set; }
        public string Provider { get; set; }
    }
}