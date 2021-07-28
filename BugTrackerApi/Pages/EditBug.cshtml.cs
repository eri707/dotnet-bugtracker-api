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
    public class EditBugModel : PageModel
    {
        private IBugsRepository _bugsRepository;
        [BindProperty] // the process that takes values from HTTP requests and maps them to handler method parameters or PageModel properties.
        public UpdateBugViewModel Bug { get; set; }
        public Guid BugId;
        public Guid ProjectId;

        public EditBugModel(IBugsRepository bugsRepository) 
        {
            _bugsRepository = bugsRepository;
        }
        public void OnGet(Guid id) // get existing a bug
        {
            BugId = id;
            var existingBug = _bugsRepository.GetBug(id);
            ProjectId = existingBug.ProjectId;
            Bug = new UpdateBugViewModel();
            Bug.Title = existingBug.Title;
            Bug.Priority = existingBug.Priority;
            Bug.Description = existingBug.Description;
            Bug.ReproSteps = existingBug.ReproSteps;
            Bug.ActualResults = existingBug.ActualResults;
            Bug.ExpectedResults = existingBug.ExpectedResults;
        }

        public ActionResult OnPost(Guid id) // add the editted bug
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var existingBug = _bugsRepository.GetBug(id);
            _bugsRepository.UpdateBug(id, Bug);
            return RedirectToPage("./Project", new { id = existingBug.ProjectId.ToString() });
        }
    }
}
