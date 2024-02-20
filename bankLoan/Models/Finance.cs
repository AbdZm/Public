using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectUni.Models
{
    public class Finance
    {
        public int Id { get; set; } 
        public int MyUserId { get; set; }
        public MyUser MyUser { get; set; }
        [Required]
        public string SecretKey { get; set; }
        [Required]
        public string Type { get; set; }
        /*Start DataMining Model*/
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Age { get; set; }
        [Required]
        public string Married { get; set; }
        [Required]
        public int Dependents { get; set; }
        [Required]
        public string Education { get; set; }
        [Required]
        public string SelfEmployed { get; set; }
        [Required]
        public int ApplicantIncome { get; set; }
        [Required]
        public string IncomeContinuation { get; set; }
        public double IncomeRate { get; set; }
        public string IncomeRisk { get; set; }
        [Required]
        public int CoapplicantIncome { get; set; }
        [Required]
        public int FinanceAmount { get; set; }
        [Required]
        public int FinanceAmountTerm { get; set; }
        [Required]
        public int Credit_History { get; set; }
        public string Loan_Status { get; set; }
        public System.DateTime ReleaseDate { get; set; } = System.DateTime.Today;
        public bool Done { get; set; }
        public bool final_decision { get; set; } 
        
    }
    public enum Gender
    {
        Male,
        Female
    }
    public enum Types
    {
        Personal,
        Education,
        Business

    }
    public enum Age
    {
        Young,
        Adult,
        Middle,
        Senior
    }
    public enum Education
    {
        Graduate,
        NotGraduate
    }
    public enum YN
    {
        Yes,
        No
    }
    public enum SelfEmployed
    {

        Yes,
        No
    }
}
