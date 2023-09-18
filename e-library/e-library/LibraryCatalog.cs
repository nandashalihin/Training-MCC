using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_library
{
    class LibraryCatalog
    {
        private List<Book> books = new List<Book>();

        public void AddBook(Book book)
        {
            books.Add(book);
            Console.WriteLine("Buku berhasil ditambahkan ke dalam katalog.");
        }

        public void RemoveBook(Book book)
        {
            if (books.Contains(book))
            {
                books.Remove(book);
                Console.WriteLine("Buku berhasil dihapus dari katalog.");
            }
            else
            {
                Console.WriteLine("Buku tidak ditemukan dalam katalog.");
            }
        }

        public Book FindBook(string title)
        {
            foreach (var book in books)
            {
                if (book.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    return book;
                }
            }
            return null;
        }

        public void ListBooks(List<Book> bookList)
        {
            Console.WriteLine("Daftar Buku dalam Katalog:");
            foreach (var book in books)
            {
                Console.WriteLine();
                Console.WriteLine($"-------------------------------------------------------");
                Console.WriteLine($"Judul:          {book.Title}");
                Console.WriteLine($"Penulis:        {book.Author}");
                Console.WriteLine($"Tahun Terbit:   {book.PublicationYear}");
                Console.WriteLine($"-------------------------------------------------------");
                Console.WriteLine();

            }
        }

        public void ListAllBooks()
        {
            ListBooks(books);
        }
    }
}
