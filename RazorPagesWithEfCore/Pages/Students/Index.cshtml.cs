using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RazorPagesWithEfCore.Data;
using RazorPagesWithEfCore.Models;

namespace RazorPagesWithEfCore.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesWithEfCore.Data.SchoolContext _context;
        private readonly MvcOptions _mvcOptions;
        public IndexModel(RazorPagesWithEfCore.Data.SchoolContext context, IOptions<MvcOptions> mvcOptions)
        {
            _context = context;
            _mvcOptions = mvcOptions.Value;
        }

        //public IList<Student> Students { get; set; }
        //分页实体列表
        public PaginatedList<Student> Students { get; set; }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public async Task OnGetAsync(string sortOrder, string searchString, string currentFilter, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            //Student = await _context.Students.Take(_mvcOptions.MaxModelBindingCollectionSize).ToListAsync();
            IQueryable<Student> studentsIQ = from s in _context.Students
                                             select s;
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            CurrentFilter = searchString;
            //.ToLower()这里解决Sqllite中区分大小写
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                studentsIQ = studentsIQ.Where(s => s.LastName.ToLower().Contains(searchString) ||
                s.FirstMidName.ToLower().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    studentsIQ = studentsIQ.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    studentsIQ = studentsIQ.OrderBy(s => s.LastName);
                    break;
            }
            int pageSize = 3;
            Students = await PaginatedList<Student>.CreateAsync(studentsIQ, pageIndex ?? 1, pageSize);
        }
    }
}
