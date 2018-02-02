namespace TimeKeeper.DAL.Models
{
    public enum Type
    {
        Developer,
        Lead,
        Project_Manager
    }
    public class Role
    {
        public string Name { get; set; }
        public decimal HourlyPrice { get; set; }
        public decimal MonthlyPrice { get; set; }
        public int Type { get; set; }
    }
}