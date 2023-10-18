using BookingManagementApp.DTOs;
using BookingManagementApp.Models;
using Client.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class UniversityController : Controller
    {
        private readonly IUniversityRepository repository;

        public UniversityController(IUniversityRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetAllUniversity()
        {
            var result = await repository.Get();
            return Json(result.Data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUniversityDto University)
        {
            var result = await repository.Post(University);
            Console.WriteLine(result);
            Console.WriteLine("Code " + result.Code);
            Console.WriteLine("Status " + result.Status);
            Console.WriteLine("Message " + result.Message);

            if (result.Code == 200)
            {
                // Data berhasil ditambahkan, Anda dapat mengarahkan pengguna ke halaman lain atau menampilkan pesan sukses
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await repository.Get(id);
            var deleteUniversity = new UniversityDto();
            if (result.Data != null)
            {
                deleteUniversity =(UniversityDto)result.Data;
            }
           
            return View(deleteUniversity);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(Guid id)
        {
            var result = await repository.Delete(id);
            if (result.Code == 200)
            {
                return RedirectToAction(nameof(Index));
            }
            
                return View();
            
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var result = await repository.Get(id);
            var detailUniversity = new UniversityDto();
            if (result.Data != null)
            {
                detailUniversity = (UniversityDto)result.Data;
            }

            return View(detailUniversity);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await repository.Get(id);
            var editUniversity = new UniversityDto();
            if (result.Data != null)
            {
                editUniversity = (UniversityDto)result.Data;
            }

            return View(editUniversity);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UniversityDto University)
        {
            var result = await repository.Put(University.Guid ,University);
            if (result.Code == 200)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();

        }

    }
}

