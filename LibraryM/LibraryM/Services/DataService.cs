using LibraryM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryM.Services
{
    public class DataService
    {
        public List<Book> Books { get; } = new List<Book>();
        public List<Member> Members { get; } = new List<Member>();
        public List<Loan> Loans { get; } = new List<Loan>();
    }
}
