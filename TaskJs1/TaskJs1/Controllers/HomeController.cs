using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Web.Services;

namespace TaskJs1.Controllers
{
        public class HomeController : Controller
        {
            public List<string> leftList = new List<string>() { "Option1", "Option1" };
            public List<string> rightList = new List<string>() { "Option2", "Option2" };

            // GET: Home
            public ActionResult Index()
            {
                ViewBag.Left = leftList;
                ViewBag.Right = rightList;
                return View();
            }

            [WebMethod]
            public void SaveToLeftList(string leftArray, string rightArray)
            {
                leftList = JsonConvert.DeserializeObject<List<string>>(leftArray);
                rightList = JsonConvert.DeserializeObject<List<string>>(rightArray);
            }
        }
    
}