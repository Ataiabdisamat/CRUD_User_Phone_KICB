using Microsoft.AspNetCore.Mvc;
using PhoneUserKICB.BLL.DTO;
using PhoneUserKICB.BLL.Interfaces;
using PhoneUserKICB.PL.Models.ViewModels;

namespace PhoneUserKICB.PL.Controllers
{
    /// <summary>
    /// Controller for user
    /// </summary>
    [Route("Users")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet("")]

        public async Task<IActionResult> Index(string searchTerm, int page = 1)
        {
            int pageSize = 5;

            var users = await _userService.GetFilteredUsersAsync(searchTerm);

            var totalUsers = users.Count();
            var usersPaged = users.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var viewModel = new UserListViewModel
            {
                Users = usersPaged.Select(user => new UserViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    DateOfBirth = user.DateOfBirth
                }).ToList(),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(totalUsers / (double)pageSize),
                SearchTerm = searchTerm
            };

            return View(viewModel);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View(new UserViewModel());
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            var isEmailUnique = await IsEmailUnique(model.Email);
            if (isEmailUnique is JsonResult jsonResult && jsonResult.Value is bool isUnique && !isUnique)
            {
                ModelState.AddModelError("Email", "User with this email already exist");
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

        [HttpGet("Edit/{id}")]
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

        [HttpPost("Edit/{id}")]
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

        [HttpGet("Delete/{id}")]
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
        [HttpGet("Detail/{id}")]
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

        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userService.DeleteUserAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("CheckEmail")]
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
