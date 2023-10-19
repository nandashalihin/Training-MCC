using BookingManagementApp.DTOs;
using BookingManagementApp.Models;
using Client.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;
/*[Authorize(Roles = "manager")]*/
public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository repository;

        public EmployeeController(IEmployeeRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var result = await repository.Get();
            var listEmployee = new List<EmployeeDto>();
            if (result != null)
            {
                listEmployee = result.Data.Select(x => (EmployeeDto)x).ToList();
            }

            return View(listEmployee);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto employee)
        {
            var result = await repository.Post(employee);
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
            var deleteEmployee = new EmployeeDto();
            if (result.Data != null)
            {
                deleteEmployee =(EmployeeDto)result.Data;
            }
           
            return View(deleteEmployee);
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
            var detailEmployee = new EmployeeDto();
            if (result.Data != null)
            {
                detailEmployee = (EmployeeDto)result.Data;
            }

            return View(detailEmployee);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await repository.Get(id);
            var editEmployee = new EmployeeDto();
            if (result.Data != null)
            {
                editEmployee = (EmployeeDto)result.Data;
            }

            return View(editEmployee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeDto employee)
        {
            var result = await repository.Put(employee.Guid ,employee);
            if (result.Code == 200)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();

        }

    }


