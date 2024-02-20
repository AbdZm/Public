using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCRProject.Models
{
    public class Template
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public Category Cat { get; set; }
        public int AttributeOCRId { get; set; }
        public AttributeOCR Att { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public string Note { get; set; }
    }
}
