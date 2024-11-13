using Microsoft.AspNetCore.Mvc;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.ViewModels.TagViewModels;

namespace MiniMvcProject.ADMIN.Controllers
{
    [ValidateAntiForgeryToken]
    public class TagController : Controller
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        public async Task<IActionResult> Index()
        {
            var tags = await _tagService.GetListAsync();

            return View(tags);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TagCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _tagService.CreateAsync(vm);
            return RedirectToAction(nameof(Index));
        }
    }
}
