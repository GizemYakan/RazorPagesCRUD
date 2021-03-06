using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorCrudUrun.Data;
using RazorCrudUrun.Models;

namespace RazorCrudUrun.Pages.Urunler
{
    [Authorize]
    public class DuzenleModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Urun Urun { get; set; }

        public DuzenleModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult OnGet(int id)
        {
            Urun = _db.Urunler.Find(id);
            if (Urun == null)
            {
                return RedirectToPage("/Urunler/Index");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _db.Urunler.Update(Urun);
            try
            {
                _db.SaveChanges();
            }
            catch (Exception)
            {

                throw new Exception($"Urun: {Urun.Id} bulunamadư."); ;
            }

            return RedirectToPage("/Urunler/Index");
        }
    }
}
