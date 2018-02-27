using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        [MaxLength(50,ErrorMessage = "Name cannot be longer than 50 characters")]
        //[RegularExpression()]
        public string Name { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }
        public string Monogram { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address_Road { get; set; }
        public string Address_ZipCode { get; set; }
        public string Address_City { get; set; }
        public string Status { get; set; }

        public ICollection<ProjectModel> Projects { get; set; }

        public CustomerModel()
        {
            Projects = new List<ProjectModel>();
        }
    }
}