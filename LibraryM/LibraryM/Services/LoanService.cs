using LibraryM.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryM.Services
{
    public class LoanService
    {
        private readonly DataService _dataService;

        public LoanService(DataService dataService)
        {
            _dataService = dataService;
        }

        public void BorrowBook(string isbn, int membershipNumber)
        {
            var book = _dataService.Books.Find(b => b.ISBN == isbn);
            var member = _dataService.Members.Find(m => m.MembershipNumber == membershipNumber);

            if (book != null && member != null)
            {
                var loan = new Loan
                {
                    BorrowedBook = book,
                    Borrower = member,
                    BorrowDate = DateTime.Now,
                    ReturnDate = DateTime.Now.AddDays(14) // Contoh: periode peminjaman selama 14 hari
                };

                _dataService.Loans.Add(loan);
                Console.WriteLine("Buku telah dipinjam.");
            }
            else
            {
                Console.WriteLine("Buku atau anggota yang dimaksud tidak ditemukan. Peminjaman gagal.");
            }
        }

        public List<Loan> GetMemberLoans(int membershipNumber)
        {
            return _dataService.Loans.FindAll(l => l.Borrower.MembershipNumber == membershipNumber);
        }
    }
}
