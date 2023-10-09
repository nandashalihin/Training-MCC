using BookingManagementApp.Contracts;
using BookingManagementApp.DTOs;
using BookingManagementApp.Models;
using Microsoft.AspNetCore.Mvc;
using BookingManagementApp.Data;
using BookingManagementApp.Utilities.Handlers;
using System.Net;
using Microsoft.EntityFrameworkCore;
using System;
using BookingManagementApp.Utilities.Handlers.Enums;
using System.Transactions;
using BookingManagementApp.Repositories;
using BookingManagementApp.Handlers;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Security.Principal;

namespace BookingManagementApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly IRepository<Education> _educationRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IEmailHandler _emailHandler;
        private readonly ITokenHandler _tokenHandler;

        public AccountController(IAccountRepository accountRepository, IEmployeeRepository employeeRepository, IUniversityRepository universityRepository, IRepository<Education> educationRepository, IEmailHandler emailHandler, ITokenHandler tokenHandler, IAccountRoleRepository accountRoleRepository, IRoleRepository roleRepository)
        {
            _accountRepository = accountRepository;
            _employeeRepository = employeeRepository;
            _universityRepository = universityRepository;
            _educationRepository = educationRepository;
            _emailHandler = emailHandler;
            _tokenHandler = tokenHandler;
            _accountRoleRepository = accountRoleRepository;
            _roleRepository = roleRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            // Mendapatkan semua data account dari repository
            var result = _accountRepository.GetAll();

            // Jika tidak ada data ditemukan, kembalikan respons NotFound
            if (!result.Any())
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            // Melakukan mapping setiap item dalam variabel result ke dalam objek AccountDto
            var data = result.Select(x => (AccountDto)x);

            // Mengembalikan respons OK dengan data yang sesuai
            return Ok(new ResponseOKHandler<IEnumerable<AccountDto>>(data));
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            // Mendapatkan data account berdasarkan GUID dari repository
            var result = _accountRepository.GetByGuid(guid);

            // Jika data tidak ditemukan, kembalikan respons NotFound
            if (result is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "ID Not Found"
                });
            }

            // Mengembalikan respons OK dengan data AccountDto yang sesuai
            return Ok(new ResponseOKHandler<AccountDto>((AccountDto)result));
        }

        [HttpPost]
        public IActionResult Create(CreateAccountDto accountDto)
        {
            try
            {
                Account toCreate = accountDto;
                toCreate.Password = HashingHandler.HashPassword(toCreate.Password);
                // Membuat account baru menggunakan data yang diterima dalam request
                var result = _accountRepository.Create(toCreate);

                // Jika gagal membuat account, kembalikan respons BadRequest
                if (result is null)
                {
                    return BadRequest(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Failed to create data"
                    });
                }

                // Mengembalikan respons OK dengan data AccountDto yang sesuai
                return Ok(new ResponseOKHandler<AccountDto>((AccountDto)result));
            }
            catch (Exception ex)
            {
                // Jika terjadi kesalahan, mengembalikan respons dengan status kode 500 (InternalServerError)
                // dan pesan kesalahan yang sesuai
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "An error occurred while creating data",
                    Error = ex.Message
                });
            }
        }


        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                // Mendapatkan objek account berdasarkan GUID dari repository
                var accountById = _accountRepository.GetByGuid(guid);

                // Jika data tidak ditemukan, kembalikan respons NotFound
                if (accountById is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "ID Not Found"
                    });
                }

                // Menghapus objek account dari repository
                var result = _accountRepository.Delete(accountById);

                // Jika gagal menghapus, kembalikan respons BadRequest
                if (!result)
                {
                    return BadRequest(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Failed to delete data"
                    });
                }

                // Mengembalikan respons OK dengan pesan "Data Deleted"
                return Ok(new ResponseOKHandler<string>("Data Deleted"));
            }
            catch (Exception ex)
            {
                // Jika terjadi kesalahan, mengembalikan respons dengan status kode 500 (InternalServerError)
                // dan pesan kesalahan yang sesuai
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "An error occurred while deleting data",
                    Error = ex.Message
                });
            }
        }


        [HttpPut]
        public IActionResult Update(AccountDto accountDto)
        {
            try
            {
                // Mendapatkan objek account berdasarkan GUID dari repository
                var accountById = _accountRepository.GetByGuid(accountDto.Guid);

                // Jika data tidak ditemukan, kembalikan respons NotFound
                if (accountById is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "ID Not Found"
                    });
                }

                Account toUpdate = accountDto;

                //Inisialiasi nilai CreatedDate agar tidak ada perubahan dari data awal
                toUpdate.CreatedDate = accountById.CreatedDate;

                //Melakukan Update dengan parameter toUpdate
                var result = _accountRepository.Update(toUpdate);

                // Jika gagal memperbarui, kembalikan respons BadRequest
                if (!result)
                {
                    return BadRequest(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Failed to Update Data"
                    });
                }

                // Mengembalikan respons OK dengan pesan "Data Updated"
                return Ok(new ResponseOKHandler<string>("Data Updated"));
            }
            catch (Exception ex)
            {
                // Jika terjadi kesalahan, mengembalikan respons dengan status kode 500 (InternalServerError)
                // dan pesan kesalahan yang sesuai
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "An error occurred while updating data",
                    Error = ex.Message
                });
            }
        }

        [HttpPost("forgotpassword")]
        public IActionResult GetOtp(string email)
        {
            var employee = _employeeRepository.GetGuidByEmail(email);
            if (employee is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound, //Inisialisasi nilai atribut Code
                    Status = HttpStatusCode.NotFound.ToString(), //Inisialisai nilai atribut Status
                    Message = "Email is not registered" //Inisialisasi nilai atribut Message
                });
            }

            try
            {
                //Mendapatkan data Account berdasarkan guid
                var accountByGuid = _accountRepository.GetByGuid(employee.Guid);


                //Mengecek apakah accountByGuid bernilai null
                if (accountByGuid is null)
                {
                    //Mengembalikan nilai response NotFound dengan response body berupa objek ResponseErrorHandler
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound, //Inisialisasi atribut Code dengan nilai 500
                        Status = HttpStatusCode.NotFound.ToString(), //Inisialisasi atribut Status dengan nilai NotFound
                        Message = "Account Was Not Created. Please register your account first!" //Inisialisasi nilai atribut Message
                    });
                }
                //Menyimpan data dari parameter ke dalam objek toUpdate, serta dilakukan mapping secara implisit
                Account toUpdate = new AccountDto
                {
                    Guid = employee.Guid,
                    Otp = GenerateHandler.GenerateRandomOTP(),
                    IsUsed = false,
                    ExpiredTime = DateTime.Now.AddMinutes(5),
                    Password = accountByGuid.Password
                };
                //Inisialiasi nilai CreatedDate agar tidak ada perubahan dari data awal
                toUpdate.CreatedDate = accountByGuid.CreatedDate;

                //Melakukan Update dengan parameter toUpdate
                _accountRepository.Update(toUpdate);
                _emailHandler.Send(subject: "Forgot Password", body: $"Your OTP is {toUpdate.Otp}", email);
                //Mengembalikan nilai response OK dengan response body berupa objek ResponseOKHandler dengan argumen string
                return Ok(new ResponseOKHandler<int>(toUpdate.Otp));
            }
            catch (ExceptionHandler ex)
            {
                //Mengembalikan nilai berupa Respon Status 500 dan objek ResponseErrorHandler
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError, //Inisialiasi atribut Code dengan nilai 500
                    Status = HttpStatusCode.InternalServerError.ToString(), //Inisialisasi atribut Status dengan nilai InternalServerError
                    Message = "Failed to create data", //Inisialisasi atribut Message
                    Error = ex.Message //Inisialisasi nilai atribut Error berupa Message dari ExceptionHandler
                });
            }
        }

        [HttpPut("ChangePassword")]
        public IActionResult ChangePassword([FromBody] ChangePasswordDto request)
        {
            // Mendapatkan Guid berdasarkan email
            var guid = _employeeRepository.GetGuidByEmail(request.Email);
            if (guid is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Email is not registered"
                });
            }

            try
            {
                // Mendapatkan data akun berdasarkan Guid
                var accountByGuid = _accountRepository.GetByGuid(guid.Guid);

                if (accountByGuid is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Account Was Not Created. Please register your account first!"
                    });
                }

                // Mengecek apakah OTP sesuai
                if (request.OTP != accountByGuid.Otp)
                {
                    return BadRequest(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Invalid OTP"
                    });
                }

                // Mengecek apakah OTP sudah digunakan
                if (accountByGuid.IsUsed)
                {
                    return BadRequest(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "OTP has already been used"
                    });
                }

                // Mengecek apakah OTP sudah expired
                if (DateTime.Now > accountByGuid.ExpiredTime)
                {
                    return BadRequest(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "OTP has expired"
                    });
                }

                // Mengecek apakah NewPassword dan ConfirmPassword sesuai
                if (request.NewPassword != request.ConfirmPassword)
                {
                    return BadRequest(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "NewPassword and ConfirmPassword do not match"
                    });
                }

                // Update password dengan yang baru
                accountByGuid.Password = request.NewPassword;
                accountByGuid.IsUsed = true;
                _accountRepository.Update(accountByGuid);

                return Ok(new ResponseOKHandler<string>("Password changed successfully"));
            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to change password",
                    Error = ex.Message
                });
            }
        }

        [HttpPost("Register")]
        public IActionResult Register(RegistrationDto request)
        {
            using (var transaction = new TransactionScope())
            {
                try
                {
                    // Cek apakah universitas sudah ada berdasarkan UniversityCode
                    var existingUniversity = _universityRepository.GetGuidByCode(request.UniversityCode);
                    var genderEnum = Enum.Parse<GenderLevel>(request.Gender);

                    University newUniversity = null; // Deklarasikan objek newUniversity di luar blok if

                    if (existingUniversity == null)
                    {
                        // Jika universitas belum ada, tambahkan universitas baru dengan menggunakan DTO
                        var universityDto = new CreateUniversityDto
                        {
                            Code = request.UniversityCode,
                            Name = request.UniversityName
                        };
                        newUniversity = universityDto; // Menggunakan operator implicit

                        var resultu = _universityRepository.Create(newUniversity);
                        existingUniversity = resultu.Guid;
                    }

                    var lastNik = _employeeRepository.GetLastNik();
                    var generatedNik = GenerateHandler.GenerateNik(lastNik);

                    // Tambahkan data Employee
                    var employee = new Employee
                    {
                        Guid = Guid.NewGuid(),
                        Nik = generatedNik, // Atur Nik yang dihasilkan
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        BirthDate = request.BirthDate,
                        Gender = genderEnum,
                        HiringDate = request.HiringDate,
                        Email = request.Email,
                        PhoneNumber = request.PhoneNumber
                    };

                   var resulte  = _employeeRepository.Create(employee);
                    

                    var educationDto = new CreateEducationDto
                    {
                        Guid = employee.Guid,
                        Major = request.Major,
                        Degree = request.Degree,
                        Gpa = request.Gpa,
                        UniversityGuid = existingUniversity
                    };
                    var education = educationDto; // Menggunakan operator implicit

                    _educationRepository.Create(education);

                    // Tambahkan data Account
                    var accountDto = new CreateAccountDto
                    {
                        Guid = employee.Guid,
                        Password = request.Password,
                        IsDeleted = false,
                        Otp = 0, // Anda dapat mengatur ini ke nilai default yang sesuai
                        IsUsed = false,
                        ExpiredTime = DateTime.Now // Anda dapat mengatur ini ke nilai default yang sesuai
                    };
                    var account = accountDto; // Menggunakan operator implicit

                    _accountRepository.Create(account);

                    transaction.Complete();

                    return Ok(new ResponseOKHandler<string>("Registration successful"));
                }
                catch (Exception ex)
                {
                    
                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status500InternalServerError,
                        Status = HttpStatusCode.InternalServerError.ToString(),
                        Message = "Failed to register",
                        Error = ex.Message
                    });
                }
            }
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            var employee = _employeeRepository.GetGuidByEmail(loginDto.Email);
            if (employee is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound, 
                    Status = HttpStatusCode.NotFound.ToString(), 
                    Message = "Email was not registered" 
                });
            }

            string? hashPassword = _accountRepository.GetPasswordByGuid(employee.Guid);
            if (hashPassword == null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound, 
                    Status = HttpStatusCode.NotFound.ToString(), 
                    Message = "Account Was Not Created. Please register your account first!" 
                });
            }
            var account = _accountRepository.GetByGuid(employee.Guid);
            if (!HashingHandler.VerifyPassword(loginDto.Password, hashPassword))
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status400BadRequest, 
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Wrong Password" 
                });
            }
            var claims = new List<Claim>();
            claims.Add(new Claim("Email", employee.Email));
            claims.Add(new Claim("username", string.Concat(employee.FirstName + " " + employee.LastName)));

            var getRoleName = from ar in _accountRoleRepository.GetAll()
                              join r in _roleRepository.GetAll() on ar.RoleGuid equals r.Guid
                              where ar.AccountGuid == account.Guid
                              select r.Name;

            foreach (var roleName in getRoleName)
            {
                claims.Add(new Claim(ClaimTypes.Role, roleName));
            }

            var token = _tokenHandler.Generate(claims);

            return Ok(new ResponseOKHandler<object>(new {Token = token}, "Login Suskes"));
        }



    }
}
