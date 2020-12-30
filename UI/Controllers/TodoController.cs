using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    [Route("[controller]/[action]")]
    public class TodoController : Controller
    {
        public IActionResult Todos()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }
    }
}
