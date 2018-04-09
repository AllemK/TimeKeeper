using System;

namespace TimeKeeper.DAL.Entities
{
    public abstract class BaseClass<T>
    {
        public T Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Deleted { get; set; }

        public BaseClass()
        {
            CreatedBy = 0;
            CreatedOn = DateTime.UtcNow;
            Deleted = false;
        }
    }
}
