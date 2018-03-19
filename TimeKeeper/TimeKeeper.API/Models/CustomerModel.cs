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
        [MaxLength(50, ErrorMessage = "Name cannot be longer than 50 characters")]
        public string Name { get; set; }
        [FileExtensions(ErrorMessage = "Wrong file format", Extensions = ".jpg")]
        public string Image { get; set; }
        [MaxLength(3, ErrorMessage = "Monogram cannot be longer than 3 characters")]
        public string Monogram { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Contact person is required")]
        [MaxLength(50, ErrorMessage = "Contact cannot be longer than 50 characters")]
        [RegularExpression(@"^(\p{L}+[\s]\p{L}+)$", ErrorMessage = "Contact cannot contain numbers or extra white spaces")]
        public string Contact { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Wrong email format")]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Wrong phone format")]
        public string Phone { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Road is required")]
        [RegularExpression(@"^(\p{L}+)[,][\s][0-9]+$", ErrorMessage = "Road is in wrong format, needs to be 'name, number'")]
        public string Address_Road { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Zip code is required")]
        [RegularExpression(@"[0-9]{4,16}$", ErrorMessage = "Zip code must only contain numbers")]
        public string Address_ZipCode { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "City is required")]
        [RegularExpression(@"^(\p{L})+[\s]{0,1}(\p{L})+$")]
        public string Address_City { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Customer status is required")]
        [RegularExpression("^(Client|Prospect)$",ErrorMessage = "Customer status must be Client or Prospect")]
        public string Status { get; set; }

        public ICollection<ProjectModel> Projects { get; set; }

        public CustomerModel()
        {
            Projects = new List<ProjectModel>();
        }
    }
}