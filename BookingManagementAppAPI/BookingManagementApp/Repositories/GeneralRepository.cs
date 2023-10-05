using BookingManagementApp.Contracts;  // Mengimpor namespace 'BookingManagementApp.Contracts' untuk menggunakan 'IRepository<TEntity>'
using BookingManagementApp.Data;      // Mengimpor namespace 'BookingManagementApp.Data' untuk menggunakan 'BookingManagementDbContext'
using BookingManagementApp.Utilities.Handlers;  // Mengimpor namespace 'BookingManagementApp.Utilities.Handlers' untuk menggunakan 'ExceptionHandler'


namespace BookingManagementApp.Repositories
{
    // Membuat kelas 'GeneralRepository<TEntity>' yang mengimplementasikan 'IRepository<TEntity>'
    // TEntity adalah tipe entitas yang akan digunakan oleh repositori ini (digenerikkan).
    public class GeneralRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        // Membuat bidang yang melindungi DbContext yang akan digunakan untuk berinteraksi dengan database.
        protected readonly BookingManagementDbContext _context;

        // Konstruktor yang menerima BookingManagementDbContext sebagai parameter.
        // Konstruktor ini akan digunakan untuk menginisialisasi bidang _context.
        protected GeneralRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        // Implementasi metode dari IRepository<TEntity>
        public IEnumerable<TEntity> GetAll()
        {
            // Mengambil semua entitas dari repositori data menggunakan Entity Framework Core.
            return _context.Set<TEntity>().ToList();
        }

        public TEntity? GetByGuid(Guid guid)
        {
            // Mengambil satu entitas berdasarkan GUID yang diberikan menggunakan Entity Framework Core.
            var entity = _context.Set<TEntity>().Find(guid);
            _context.ChangeTracker.Clear();  // Menghapus entitas dari Change Tracker untuk mencegah efek samping yang tidak diinginkan.
            return entity;
        }

        public TEntity? Create(TEntity entity)
        {
            try
            {
                // Menambahkan entitas baru ke repositori data menggunakan Entity Framework Core.
                _context.Set<TEntity>().Add(entity);
                _context.SaveChanges();  // Menyimpan perubahan ke database.
                return entity;
            }
            catch (Exception ex)
            {
                // Jika terjadi kesalahan, lemparkan ExceptionHandler dengan pesan kesalahan yang sesuai.
                throw new ExceptionHandler(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public bool Update(TEntity entity)
        {
            try
            {
                // Memperbarui entitas yang ada dalam repositori data menggunakan Entity Framework Core.
                _context.Set<TEntity>().Update(entity);
                _context.SaveChanges();  // Menyimpan perubahan ke database.
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(TEntity entity)
        {
            try
            {
                // Menghapus entitas dari repositori data menggunakan Entity Framework Core.
                _context.Set<TEntity>().Remove(entity);
                _context.SaveChanges();  // Menyimpan perubahan ke database.
                return true;
            }
            catch (Exception ex)
            {
                // Jika terjadi kesalahan, lemparkan ExceptionHandler dengan pesan kesalahan yang sesuai.
                throw new ExceptionHandler(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
