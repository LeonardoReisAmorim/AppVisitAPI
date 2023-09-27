using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppVisitAPI.Controllers
{
    public class EstadoController : Controller
    {
        // GET: EstadoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: EstadoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EstadoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstadoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EstadoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EstadoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EstadoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EstadoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
