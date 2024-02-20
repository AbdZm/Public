using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectUni.Models
{
    public class AccountUser
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int Amount { get; set; }
        
        public int AccountId { get; set; }
        
        public Account Account { get; set; }
        
        public int MyUserId { get; set; }
        
        public MyUser MyUser { get; set; }
    }
}
