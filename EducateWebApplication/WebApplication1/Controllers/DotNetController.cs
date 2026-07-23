using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace WebApplication1.Controllers
{
    public class DotNetController : Controller
    {
        public IActionResult CSharp()=> View();


        public IActionResult ASPNetCore()=> View();

        public IActionResult SqlServer() => View();

    }
}
