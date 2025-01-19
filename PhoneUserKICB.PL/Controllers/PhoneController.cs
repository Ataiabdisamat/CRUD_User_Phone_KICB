using Microsoft.AspNetCore.Mvc;
using PhoneUserKICB.BLL.DTO;
using PhoneUserKICB.BLL.Interfaces;
using PhoneUserKICB.DAL.Entities;
using PhoneUserKICB.PL.Models.ViewModels;

namespace PhoneUserKICB.PL.Controllers
{
    /// <summary>
    /// Controller for phone
    /// </summary>
    [Route("Phone")]
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
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 5; 

            var phones = await _phoneService.GetAllPhonesAsync();

            var totalPhones = phones.Count();

            var phonesPaged = phones.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var viewModel = new List<PhoneViewModel>();
            foreach (var phone in phonesPaged)
            {
                var user = await _userService.GetUserByIdAsync(phone.UserId);
                viewModel.Add(new PhoneViewModel
                {
                    Id = phone.Id,
                    PhoneNumber = phone.PhoneNumber,
                    UserId = phone.UserId,
                    UserName = user.Name,
                    UserEmail = user.Email
                });
            }

            return View(new PhoneListViewModel
            {
                Phones = viewModel,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(totalPhones / (double)pageSize)
            });
        }

        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            var users = await _userService.GetAllUsersAsync();
            ViewBag.Users = users.Select(u => new { u.Id, u.Name });
            return View(new PhoneViewModel());
        }

        [HttpPost("Create")]
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

        [HttpGet("Edit/{id}")]
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

        [HttpPost("Edit/{id}")]
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

        [HttpGet("Delete/{id}")]
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

        [HttpGet("Detail/{id}")]
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

            
            var user = await _userService.GetUserByIdAsync(phone.UserId);
            ViewBag.UserName = user?.Name;

            return View(phoneViewModel);
        }

        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _phoneService.DeletePhoneAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
