using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesWithEfCore.Data;
using RazorPagesWithEfCore.Models;

namespace RazorPagesWithEfCore.Pages.Courses
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesWithEfCore.Data.SchoolContext _context;

        public IndexModel(RazorPagesWithEfCore.Data.SchoolContext context)
        {
            _context = context;
        }

        public IList<Course> Courses { get;set; }

        public async Task OnGetAsync()
        {
            Courses = await _context.Courses
                .Include(c => c.Department)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
