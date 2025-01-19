using Microsoft.AspNetCore.Mvc;
using PhoneUserKICB.BLL.DTO;
using PhoneUserKICB.BLL.Interfaces;
using PhoneUserKICB.PL.Models.ViewModels;

namespace PhoneUserKICB.PL.Controllers
{
    [Route("users")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            var viewModel = users.Select(user => new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth
            });
            return View(viewModel);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View(new UserViewModel());
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            var isEmailUnique = await IsEmailUnique(model.Email);
            if (isEmailUnique is JsonResult jsonResult && jsonResult.Value is bool isUnique && !isUnique)
            {
                ModelState.AddModelError("Email", "Пользователь с таким email уже существует.");
            }
            if (ModelState.IsValid)
            {
                await _userService.CreateUserAsync(new UserDto
                {
                    Name = model.Name,
                    Email = model.Email,
                    DateOfBirth = model.DateOfBirth
                });
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            var viewModel = new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth
            };
            return View(viewModel);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _userService.UpdateUserAsync(new UserDto
                {
                    Id = model.Id,
                    Name = model.Name,
                    Email = model.Email,
                    DateOfBirth = model.DateOfBirth
                });
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            return View(new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth
            });
        }
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userViewModel = new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth
            };

            return View(userViewModel);
        }

        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userService.DeleteUserAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("check-email")]
        public async Task<IActionResult> IsEmailUnique(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user != null)
            {
                return Json(false); 
            }
            return Json(true); 
        }
    }
}
