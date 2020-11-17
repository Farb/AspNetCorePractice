using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWeb01.Controllers
{
    public class FirstController : Controller
    {
        private readonly ILogger<FirstController> _logger;
        private readonly ILoggerFactory _loggerFactory;
        public FirstController(ILogger<FirstController> logger,ILoggerFactory loggerFactory)
        {
            _logger = logger;
            _loggerFactory = loggerFactory;
        }
        public IActionResult Index()
        {
            _logger.LogInformation($"记录日志：this is FirstController-Index");
            ViewBag.UserName1 = "周星驰01_viewBag-dynamic";
            ViewData["UserName"] = "周星驰02_viewData-ViewDataDictionary";
            TempData["UserName"] = "周星驰03_tempData-ITempDataDict";
            object model = "周星驰04_model_object";
            //session 使用时要添加中间件,Session的定义是一个抽象接口，遵循最小声明原则
            string sessionName= HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(sessionName))
            {
                HttpContext.Session.SetString("UserName", "周星驰05_session");
                _loggerFactory.CreateLogger(typeof(FirstController)).LogInformation("获取session的值");
            }
            return View(model);
        }
    }
}
