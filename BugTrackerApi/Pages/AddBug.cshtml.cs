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
        public Guid ProjectId { get; set; }

        public AddBugModel(IBugsRepository bugsRepository)
        {
            _bugsRepository = bugsRepository;
        }

        public ActionResult OnPost() 
        { 
            if (!ModelState.IsValid)
            {
                return Page(); 
            }
            _bugsRepository.AddBug(Bug); 
            return RedirectToPage("./Project", new { id = Bug.ProjectId.ToString() }); 
        }
        public void OnGet([FromQuery]Guid projectId) // retrieve query parameters from URL(This is model binder)
        {
            ProjectId = projectId;
        }
    }
}
