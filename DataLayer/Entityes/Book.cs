using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entityes
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string Genre { get; set; }
        public int AuthorId { get; set; }
    }
}
