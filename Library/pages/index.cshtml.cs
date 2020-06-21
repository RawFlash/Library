using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.pages
{
    public class indexModel : PageModel
    {
        public string HrefToGiveOnHand;

        public string HrefToGiveInLibrary;

        public string HrefToReturn;
        public void OnGet()
        {
            HrefToGiveOnHand = "/giveonhand";
            HrefToGiveInLibrary = "/giveinlibrary";
            HrefToReturn = "/returnbook";
        }
    }
}