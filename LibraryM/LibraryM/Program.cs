using System;
using LibraryM.Models;
using LibraryM.Services;
namespace LibraryM
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataService = new DataService();
            var bookService = new BookService(dataService);
            var memberService = new MemberService(dataService);
            var loanService = new LoanService(dataService);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Selamat datang di Aplikasi Manajemen Pustaka!");
                Console.WriteLine("Pilih Layanan:");
                Console.WriteLine("1. Manajemen Buku");
                Console.WriteLine("2. Manajemen Anggota");
                Console.WriteLine("3. Peminjaman Buku");
                Console.WriteLine("0. Keluar");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        BookMenu(bookService);
                        break;
                    case "2":
                        MemberMenu(memberService);
                        break;
                    case "3":
                        LoanMenu(loanService);
                        break;
                    case "0":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid.");
                        break;
                }

                Console.WriteLine("Tekan Enter untuk melanjutkan...");
                Console.ReadLine();
            }
        }

        static void BookMenu(BookService bookService)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Manajemen Buku:");
                Console.WriteLine("1. Tambah Buku");
                Console.WriteLine("2. Edit Buku");
                Console.WriteLine("3. Hapus Buku");
                Console.WriteLine("4. Lihat Daftar Buku");
                Console.WriteLine("0. Kembali ke Menu Utama");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Judul: ");
                        string title = Console.ReadLine();
                        Console.Write("Pengarang: ");
                        string author = Console.ReadLine();
                        Console.Write("ISBN: ");
                        string isbn = Console.ReadLine();

                        bookService.AddBook(new Book { Title = title, Author = author, ISBN = isbn });
                        Console.WriteLine("Buku telah ditambahkan.");
                        break;
                    case "2":
                        Console.Write("ISBN buku yang akan diubah: ");
                        string bookIsbn = Console.ReadLine();

                        // Cari buku berdasarkan ISBN
                        var bookToEdit = bookService.GetAllBooks().FirstOrDefault(b => b.ISBN == bookIsbn);

                        if (bookToEdit != null)
                        {
                            Console.Write("Judul: ");
                            string updatedTitle = Console.ReadLine();
                            Console.Write("Pengarang: ");
                            string updatedAuthor = Console.ReadLine();
                            Console.Write("ISBN: ");
                            string updatedIsbn = Console.ReadLine();

                            bookService.EditBook(bookIsbn, new Book { Title = updatedTitle, Author = updatedAuthor, ISBN = updatedIsbn });
                        }
                        else
                        {
                            Console.WriteLine("Buku dengan ISBN yang diberikan tidak ditemukan.");
                        }
                        break;

                    case "3":
                        // Hapus buku
                        Console.Write("ISBN buku yang akan dihapus: ");
                        string bookToDelete = Console.ReadLine();

                        var deletedBook = bookService.DeleteBook(bookToDelete);
                        break;
                    case "4":
                        List<Book> books = bookService.GetAllBooks();
                        Console.WriteLine("Daftar Buku:");
                        foreach (var book in books)
                        {
                            Console.WriteLine($"Judul: {book.Title}, Pengarang: {book.Author}, ISBN: {book.ISBN}");
                        }
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Pilihan tidak valid.");
                        break;
                }

                Console.WriteLine("Tekan Enter untuk melanjutkan...");
                Console.ReadLine();
            }
        }

        static void MemberMenu(MemberService memberService)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Manajemen Anggota:");
                Console.WriteLine("1. Tambah Anggota");
                Console.WriteLine("2. Edit Anggota");
                Console.WriteLine("3. Hapus Anggota");
                Console.WriteLine("4. Lihat Daftar Anggota");
                Console.WriteLine("0. Kembali ke Menu Utama");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Nama: ");
                        string name = Console.ReadLine();
                        Console.Write("Email: ");
                        string email = Console.ReadLine();
                        Console.Write("Nomor Keanggotaan: ");
                        int membershipNumber = int.Parse(Console.ReadLine());

                        memberService.AddMember(new Member { Name = name, Email = email, MembershipNumber = membershipNumber });
                        Console.WriteLine("Anggota telah ditambahkan.");
                        break;
                    case "2":
                        Console.Write("Nomor keanggotaan anggota yang akan diubah: ");
                        int memberNumber = int.Parse(Console.ReadLine());

                        // Cari anggota berdasarkan nomor keanggotaan
                        var memberToEdit = memberService.GetAllMembers().FirstOrDefault(m => m.MembershipNumber == memberNumber);

                        if (memberToEdit != null)
                        {
                            Console.Write("Nama: ");
                            string updatedName = Console.ReadLine();
                            Console.Write("Email: ");
                            string updatedEmail = Console.ReadLine();
                            Console.Write("Nomor Keanggotaan: ");
                            int updatedMembershipNumber = int.Parse(Console.ReadLine());

                            memberService.EditMember(memberNumber, new Member { Name = updatedName, Email = updatedEmail, MembershipNumber = updatedMembershipNumber });
                            Console.WriteLine("Anggota telah diubah.");
                        }
                        else
                        {
                            Console.WriteLine("Anggota dengan nomor keanggotaan yang diberikan tidak ditemukan.");
                        }
                        break;
                    case "3":
                        Console.Write("Nomor keanggotaan anggota yang akan dihapus: ");
                        int memberToDelete = int.Parse(Console.ReadLine());

                        // Cari anggota berdasarkan nomor keanggotaan
                        var deletedMember = memberService.DeleteMember(memberToDelete);

                        if (deletedMember != null)
                        {
                            Console.WriteLine("Anggota telah dihapus.");
                        }
                        else
                        {
                            Console.WriteLine("Anggota dengan nomor keanggotaan yang diberikan tidak ditemukan.");
                        }
                        break;
                    case "4":
                        List<Member> members = memberService.GetAllMembers();
                        Console.WriteLine("Daftar Anggota:");
                        foreach (var member in members)
                        {
                            Console.WriteLine($"Nama: {member.Name}, Email: {member.Email}, Nomor Keanggotaan: {member.MembershipNumber}");
                        }
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Pilihan tidak valid.");
                        break;
                }

                Console.WriteLine("Tekan Enter untuk melanjutkan...");
                Console.ReadLine();
            }
        }

        static void LoanMenu(LoanService loanService)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Peminjaman Buku:");
                Console.WriteLine("1. Pinjam Buku");
                Console.WriteLine("2. Lihat Status Peminjaman");
                Console.WriteLine("0. Kembali ke Menu Utama");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("ISBN buku yang akan dipinjam: ");
                        string isbnToBorrow = Console.ReadLine();
                        Console.Write("Nomor keanggotaan: ");
                        int membershipToBorrow;
                        if (int.TryParse(Console.ReadLine(), out membershipToBorrow))
                        {
                            loanService.BorrowBook(isbnToBorrow, membershipToBorrow);
                        }
                        else
                        {
                            Console.WriteLine("Nomor keanggotaan tidak valid. Peminjaman gagal.");
                        }
                        break;
                    case "2":
                        Console.Write("Nomor keanggotaan: ");
                        int membershipToCheck;
                        if (int.TryParse(Console.ReadLine(), out membershipToCheck))
                        {
                            List<Loan> loans = loanService.GetMemberLoans(membershipToCheck);
                            if (loans.Count > 0)
                            {
                                Console.WriteLine("Status Peminjaman:");
                                foreach (var loan in loans)
                                {
                                    Console.WriteLine($"Buku: {loan.BorrowedBook.Title}, Peminjam: {loan.Borrower.Name}, Tanggal Pinjam: {loan.BorrowDate}, Tanggal Kembali: {loan.ReturnDate}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Belum ada peminjaman untuk anggota ini.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nomor keanggotaan tidak valid.");
                        }
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Pilihan tidak valid.");
                        break;
                }

                Console.WriteLine("Tekan Enter untuk melanjutkan...");
                Console.ReadLine();
            }
        }

    }
}
