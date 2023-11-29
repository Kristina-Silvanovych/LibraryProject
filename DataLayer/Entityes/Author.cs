using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entityes
{
    public class Author
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string FathersName { get; set; }
        public DateTime BirthDate { get; set; }
        public List<Book> Books { get; set; }
    }
}
