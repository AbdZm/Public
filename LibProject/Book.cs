using System;
using System.Collections.Generic;
using System.Text;

namespace LibProject
{
    abstract class Book : Library, Information
    {
        string bookName;
        string author;
        int pages;
        public Book()
        {
            
        }
        public Book(string bookName, string author, int pages, string libName, int libID) : base(libName, libID)
        {
            this.bookName = bookName;
            this.author = author;
            this.pages = pages;
        }
        abstract public void Welcome(string sentince);

        public string X()
        {
            return "Book Information Interface !!";
        }
    }
}
