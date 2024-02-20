using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCRProject.Models
{
    public class OCRScan
    {
        public int Id { get; set; }
        public int TemplateId { get; set; }
        public Template Tem { get; set; }
        public int PageId { get; set; }
        public Page Pg { get; set; }
        public string Value { get; set; }
        public string Note { get; set; }

    }
}
