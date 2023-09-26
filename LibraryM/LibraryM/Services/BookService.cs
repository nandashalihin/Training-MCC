using LibraryM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryM.Services
{
    public class BookService
    {
        private readonly DataService _dataService;

        public BookService(DataService dataService)
        {
            _dataService = dataService;
        }

        public void AddBook(Book book)
        {
            _dataService.Books.Add(book);
        }

        public List<Book> GetAllBooks()
        {
            return _dataService.Books;
        }

        public void EditBook(string isbn, Book updatedBook)
        {
            var book = _dataService.Books.Find(b => b.ISBN == isbn);
            
                // Hapus buku lama dari koleksi
                _dataService.Books.Remove(book);

                // Tambahkan buku yang telah diubah
                _dataService.Books.Add(updatedBook);

                Console.WriteLine("Buku telah diubah.");
            
        }



        public Book DeleteBook(string isbn)
        {
            var bookToDelete = _dataService.Books.FirstOrDefault(b => b.ISBN == isbn);
            if (bookToDelete != null)
            {
                _dataService.Books.Remove(bookToDelete);
                Console.WriteLine("Buku telah dihapus.");
            }
            else
            {
                Console.WriteLine("Buku dengan ISBN yang diberikan tidak ditemukan.");
            }

            return bookToDelete;
        }
    }
}


