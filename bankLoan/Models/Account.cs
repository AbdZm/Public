using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectUni.Models
{
    public class Account
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Description { get; set; }
        
        public bool Status { get; set; }
        public List<AccountUser> AccountUser { get; set; }

        public Account()
        {
        }

    }
}
