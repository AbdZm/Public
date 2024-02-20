using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectUni.Models
{
    public class MyUser
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string SecretKey { get; set; }
        [Required]
        public bool Status { get; set; }
      
        public List<AccountUser> AccountUser { get; set; }


        public MyUser()
        {

        }
    }
}
