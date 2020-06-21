using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.pages.returnbook
{
    public class indexModel : PageModel
    {
        public List<Book> BooksUseInLibrary;
        public List<Book> BooksUseOnHands;

        public IActionResult OnGet(int? book)
        {
            if (book != null)
            {
                TestDB.ReturnBook((int)book);
                return RedirectToPage("/index");
            }
            BooksUseInLibrary = TestDB.ReturnBooksAtStatus(Statuses.UseInLibrary);
            BooksUseOnHands = TestDB.ReturnBooksAtStatus(Statuses.UseOnHands);
            return Page();
        }
    }
}