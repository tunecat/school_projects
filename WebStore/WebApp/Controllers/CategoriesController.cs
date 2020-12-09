using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebApp.ViewModels;
using Category = BLL.App.DTO.Category;

namespace WebApp.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly IOptions<RequestLocalizationOptions> LocalizationOptions;

        public CategoriesController(IAppBLL bll, IOptions<RequestLocalizationOptions> localizationOptions)
        {
            _bll = bll;
            LocalizationOptions = localizationOptions;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var categories = await _bll.CategoryService.GetAllParentCategoriesAsync();
            
            return View(categories);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _bll.CategoryService.FirstOrDefaultAsync(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public async Task<IActionResult> Create()
        {
            CategoryCreateEditViewModel vm = new CategoryCreateEditViewModel();
            vm.CategorySelectList = new SelectList(
                await _bll.CategoryService.GetAllParentCategoriesAsync(), nameof(Category.Id), nameof(Category.Name));

            return View(vm);
        }

        public IActionResult CreateParentCategory()
        {
            CategoryCreateEditViewModel vm = new CategoryCreateEditViewModel();
            return View(vm);
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateEditViewModel vm)
        {
            var cultureItems = LocalizationOptions.Value.SupportedUICultures
                .Select(c => new {Value = c.Name, Text = c.DisplayName}).ToList();
            
            if (ModelState.IsValid)
            {
                var translations = new Dictionary<string, string>();
                foreach (var cultureItem in cultureItems)
                {
                    translations[cultureItem.Value] = vm.Category.Name;
                }

                vm.Category.Translation = JsonConvert.SerializeObject(translations, Formatting.Indented);

                // vm.Category.Id = Guid.NewGuid();
                _bll.CategoryService.Add(vm.Category);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.CategorySelectList = new SelectList(await _bll.CategoryService.GetAllParentCategoriesAsync(),
                nameof(Category.Id), nameof(Category.Name),
                vm.Category.ParentCategoryId);
            return View(vm);
        }


        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            CategoryCreateEditViewModel vm = new CategoryCreateEditViewModel();
            if (id == null)
            {
                return NotFound();
            }


            var category = await _bll.CategoryService.FirstOrDefaultAsync(id.Value);
            vm.Category = category;
            if (category == null)
            {
                return NotFound();
            }

            vm.CategorySelectList = new SelectList(await _bll.CategoryService.GetAllParentCategoriesAsync(),
                nameof(Category.Id), nameof(Category.Name),
                vm.Category.ParentCategoryId);

            return View(vm);
        }


        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CategoryCreateEditViewModel vm)
        {
            if (id != vm.Category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bll.CategoryService.UpdateAsync(vm.Category);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _bll.CategoryService.ExistsAsync(vm.Category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            vm.CategorySelectList = new SelectList(await _bll.CategoryService.AllAsync(), nameof(Category.Id),
                nameof(Category.Name),
                vm.Category.ParentCategoryId);

            return View(vm);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> EditTranslation(Guid? id)
        {
            CategoryTranslateViewModel vm = new CategoryTranslateViewModel();
            if (id == null)
            {
                return NotFound();
            }

            var category = await _bll.CategoryService.FirstOrDefaultAsync(id.Value);
            if (category == null)
            {
                return NotFound();
            }


            if (string.IsNullOrEmpty(category.Translation) || category.Translation == "{}")
            {
                var cultureItems = LocalizationOptions.Value.SupportedUICultures
                    .Select(c => new {Value = c.Name, Text = c.DisplayName}).ToList();
                var translations = new Dictionary<string, string>();
                foreach (var cultureItem in cultureItems)
                {
                    translations[cultureItem.Value] = category.Name;
                }

                vm.Translations = translations;
            }
            else
            {
                var trans = JsonConvert.DeserializeObject<Dictionary<string, string>>(category.Translation);
                category.TranslationObjects = trans;
                vm.Translations = category.TranslationObjects;
            }

            return View(vm);
        }

        // POST: Categories/EditTranslation/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTranslation(Guid id, CategoryTranslateViewModel vm)
        {
            vm.Translations["en"] = vm.en!;
            vm.Translations["et"] = vm.et!;
            vm.Translations["ru"] = vm.ru!;
            vm.CategoryId = id;

            var category = await _bll.CategoryService.FirstOrDefaultAsync(vm.CategoryId);
            if (category == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                category.Translation = JsonConvert.SerializeObject(vm.Translations);
                try
                {
                    await _bll.CategoryService.UpdateAsync(category);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                }

                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _bll.CategoryService.FirstOrDefaultAsync(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var category = await _bll.CategoryService.FirstOrDefaultAsync(id);
            await _bll.CategoryService.RemoveAsync(category.Id);

            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}