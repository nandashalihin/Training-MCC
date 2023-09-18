using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_library
{
    class LibraryApp
    {
        static void Main()
        {
            LibraryCatalog catalog = new LibraryCatalog();

            while (true)
            {
                Console.WriteLine("Pilihan Menu:");
                Console.WriteLine("1. Tambah Buku");
                Console.WriteLine("2. Hapus Buku");
                Console.WriteLine("3. Cari Buku");
                Console.WriteLine("4. List Buku");
                Console.WriteLine("5. Keluar");
                Console.Write("Masukkan pilihan: ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    try
                    {
                        switch (choice)
                        {
                            case 1:
                                Console.Write("Masukkan judul buku: ");
                                string title = Console.ReadLine();
                                Console.Write("Masukkan nama penulis: ");
                                string author = Console.ReadLine();
                                Console.Write("Masukkan tahun terbit: ");
                                if (int.TryParse(Console.ReadLine(), out int publicationYear))
                                {
                                    Book newBook = new Book
                                    {
                                        Title = title,
                                        Author = author,
                                        PublicationYear = publicationYear
                                    };
                                    catalog.AddBook(newBook);
                                }
                                else
                                {
                                    ErrorHandler.HandleInvalidInputError();
                                }
                                break;
                            case 2:
                                Console.Write("Masukkan judul buku yang ingin dihapus: ");
                                string bookTitleToRemove = Console.ReadLine();
                                Book bookToRemove = catalog.FindBook(bookTitleToRemove);
                                if (bookToRemove != null)
                                {
                                    catalog.RemoveBook(bookToRemove);
                                }
                                else
                                {
                                    ErrorHandler.HandleError(new Exception("Buku tidak ditemukan dalam katalog."));
                                }
                                break;
                            case 3:
                                Console.Write("Masukkan judul buku yang ingin dicari: ");
                                string bookTitleToFind = Console.ReadLine();
                                Book foundBook = catalog.FindBook(bookTitleToFind);
                                if (foundBook != null)
                                {
                                    catalog.ListBooks(new List<Book> { foundBook });
                                }
                                else
                                {
                                    ErrorHandler.HandleError(new Exception("Buku tidak ditemukan dalam katalog."));
                                }
                                break;
                            case 4:
                                catalog.ListAllBooks();
                                break;
                            case 5:
                                Environment.Exit(0);
                                break;
                            default:
                                ErrorHandler.HandleError(new Exception("Pilihan tidak valid."));
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorHandler.HandleError(ex);
                    }
                }
                else
                {
                    ErrorHandler.HandleInvalidInputError();
                }
            }
        }
    }
}