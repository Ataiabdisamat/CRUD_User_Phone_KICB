using Microsoft.AspNetCore.Mvc;
using PhoneUserKICB.BLL.DTO;
using PhoneUserKICB.BLL.Interfaces;
using PhoneUserKICB.PL.Models.ViewModels;

namespace PhoneUserKICB.PL.Controllers
{
    [Route("phone")]
    public class PhoneController : Controller
    {
        private readonly IPhoneService _phoneService;
        private readonly IUserService _userService;

        public PhoneController(IPhoneService phoneService, IUserService userService)
        {
            _phoneService = phoneService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var phones = await _phoneService.GetAllPhonesAsync();
            var viewModel = phones.Select(phone => new PhoneViewModel
            {
                Id = phone.Id,
                PhoneNumber = phone.PhoneNumber,
                UserId = phone.UserId
            });
            return View(viewModel);
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            var users = await _userService.GetAllUsersAsync();
            ViewBag.Users = users.Select(u => new { u.Id, u.Name });
            return View(new PhoneViewModel());
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PhoneViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _phoneService.CreatePhoneAsync(new PhoneDto
                {
                    PhoneNumber = model.PhoneNumber,
                    UserId = model.UserId
                });
                return RedirectToAction(nameof(Index));
            }

            var users = await _userService.GetAllUsersAsync();
            ViewBag.Users = users.Select(u => new { u.Id, u.Name });
            return View(model);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var phone = await _phoneService.GetPhoneByIdAsync(id);
            if (phone == null) return NotFound();

            var users = await _userService.GetAllUsersAsync();
            ViewBag.Users = users.Select(u => new { u.Id, u.Name });

            var viewModel = new PhoneViewModel
            {
                Id = phone.Id,
                PhoneNumber = phone.PhoneNumber,
                UserId = phone.UserId
            };
            return View(viewModel);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PhoneViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _phoneService.UpdatePhoneAsync(new PhoneDto
                {
                    Id = model.Id,
                    PhoneNumber = model.PhoneNumber,
                    UserId = model.UserId
                });
                return RedirectToAction(nameof(Index));
            }

            var users = await _userService.GetAllUsersAsync();
            ViewBag.Users = users.Select(u => new { u.Id, u.Name });
            return View(model);
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var phone = await _phoneService.GetPhoneByIdAsync(id);
            if (phone == null) return NotFound();

            return View(new PhoneViewModel
            {
                Id = phone.Id,
                PhoneNumber = phone.PhoneNumber,
                UserId = phone.UserId
            });
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var phone = await _phoneService.GetPhoneByIdAsync(id);
            if (phone == null)
            {
                return NotFound();
            }

            var phoneViewModel = new PhoneViewModel
            {
                Id = phone.Id,
                PhoneNumber = phone.PhoneNumber,
                UserId = phone.UserId
            };

            // Можно добавить информацию о пользователе, если нужно
            var user = await _userService.GetUserByIdAsync(phone.UserId);
            ViewBag.UserName = user?.Name;

            return View(phoneViewModel);
        }

        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _phoneService.DeletePhoneAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
