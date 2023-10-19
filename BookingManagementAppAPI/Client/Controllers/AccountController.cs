using BookingManagementApp.DTOs;
using BookingManagementApp.Models;
using Client.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository repository;

        public AccountController(IAccountRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var result = await repository.Login(login);
            if (result is null)
            {
                return RedirectToAction("Login", "Account");
            }
            else if (result.Code == 409)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
            else if (result.Code == 200)
            {
                HttpContext.Session.SetString("JWToken", result.Data.Token);
                return RedirectToAction("Index", "Home");
            }
            return View();

            
        }

        

        /*public async Task<JsonResult> GetAllAccount()
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
        public async Task<IActionResult> Create(CreateAccountDto Account)
        {
            var result = await repository.Post(Account);
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
            var deleteAccount = new AccountDto();
            if (result.Data != null)
            {
                deleteAccount =(AccountDto)result.Data;
            }
           
            return View(deleteAccount);
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
            var detailAccount = new AccountDto();
            if (result.Data != null)
            {
                detailAccount = (AccountDto)result.Data;
            }

            return View(detailAccount);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await repository.Get(id);
            var editAccount = new AccountDto();
            if (result.Data != null)
            {
                editAccount = (AccountDto)result.Data;
            }

            return View(editAccount);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AccountDto Account)
        {
            var result = await repository.Put(Account.Guid ,Account);
            if (result.Code == 200)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();

        }*/

    }
}

