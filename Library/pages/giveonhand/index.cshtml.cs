using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.pages.giveonhand
{
    public class indexModel : PageModel
    {
        public List<Book> Books;
        public List<Reader> Readers;

        public IActionResult OnGet(int? book, int? reader)
        {
            if(book!=null && reader!=null)
            {
                TestDB.GiveOnHand((int)book, (int)reader);
                return RedirectToPage("/index");
            }
            Books = TestDB.ReturnBooksAtStatus(Statuses.InLibrary);
            Readers = TestDB.ReturnReaders();
            return Page();
        }
    }
}