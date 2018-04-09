using TimeKeeper.Utility;

namespace TimeKeeper.API.Reports
{
    public class PersonalModel
    {
        [Precision(5,2)]
        public decimal TotalHours { get; set; }
        public decimal Utilization { get; set; }
        public int BradfordFactor { get; set; }
        public string[] Days { get; set; }

        public PersonalModel(int size)
        {
            Days = new string[size];
        }
    }
}