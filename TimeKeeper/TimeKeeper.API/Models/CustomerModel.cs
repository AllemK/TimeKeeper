using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Models
{
    public class CustomerModel
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Monogram { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; }
        public CustomerStatus Status { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}