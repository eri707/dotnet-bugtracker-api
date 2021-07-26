using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTrackerApi.Models;
using BugTrackerApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerApi.Pages
{
    public class AddBugModel : PageModel
    {
        private IBugsRepository _bugsRepository;
        [BindProperty]
        public AddBugViewModel Bug { get; set; }
        
        public AddBugModel(IBugsRepository bugsRepository)
        {
            _bugsRepository = bugsRepository;
        }

        public ActionResult OnPost() // where should it be used??
        { // what is ModelState??
            if (!ModelState.IsValid)
            {
                return Page(); //??
            }
            _bugsRepository.AddBug(Bug); // what is this Project??
            return RedirectToPage("/Index"); // why Index??
        }
    }
}
