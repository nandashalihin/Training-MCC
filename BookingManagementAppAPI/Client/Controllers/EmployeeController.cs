using BookingManagementApp.DTOs;
using BookingManagementApp.Models;
using Client.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
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
                listEmployee = result.Data.ToList();
            }

            return View(listEmployee);
        }




    }
}
