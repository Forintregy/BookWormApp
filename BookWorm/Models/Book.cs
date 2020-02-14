using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWorm
{
    [DebuggerDisplay("{Author} - {Title}")]
    public class Book
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Text { get; set; }
        
        public Book(string author, string title, string link)
        {
            Author = author;
            Title = title;
            Link = link;
        }

        public override string ToString()
        {
            return Author + ": " + Title;
        }
    }
}
