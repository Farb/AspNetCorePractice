using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesWithEfCore.Data;
using RazorPagesWithEfCore.Models;

namespace RazorPagesWithEfCore.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly RazorPagesWithEfCore.Data.SchoolContext _context;

        public CreateModel(RazorPagesWithEfCore.Data.SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        #region 防止过度方法1 使用TryUpdateModelAsync
        /*
         
        [BindProperty]
        public Student Student { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            //_context.Students.Add(Student);
            //await _context.SaveChangesAsync();

            var emptyStudent = new Student();

            if (await TryUpdateModelAsync<Student>(
                emptyStudent,
                "student",   // Prefix for form value.
                s => s.FirstMidName, s => s.LastName, s => s.EnrollmentDate))
            {
                _context.Students.Add(emptyStudent);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return RedirectToPage("./Index");
        }

         */
        #endregion


        #region 防止过度发布方法2 使用ViewModel

        #endregion
        [BindProperty]
        public StudentVM StudentVM { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var entry = _context.Add(new Student());
            entry.CurrentValues.SetValues(StudentVM);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
