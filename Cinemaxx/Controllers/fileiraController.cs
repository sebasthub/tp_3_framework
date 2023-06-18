using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cinemaxx;

namespace Cinemaxx.Controllers
{
    public class fileiraController : Controller
    {
        private CinemaxxContext db = new CinemaxxContext();

        // GET: fileira
        public ActionResult Index()
        {
            var fileira = db.fileira.Include(f => f.sala1);
            return View(fileira.ToList());
        }

        // GET: fileira/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fileira fileira = db.fileira.Find(id);
            if (fileira == null)
            {
                return HttpNotFound();
            }
            return View(fileira);
        }

        // GET: fileira/Create
        public ActionResult Create()
        {
            ViewBag.sala = new SelectList(db.sala, "id", "indentificador");
            return View();
        }

        // POST: fileira/Create
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,sala,indentificador,cadeiras_de,cadeiras_ate,pne,pne_de,pne_ate")] fileira fileira)
        {
            if (ModelState.IsValid)
            {
                int id = db.fileira
                .OrderByDescending(o => o.id)
                .Select(o => o.id)
                .FirstOrDefault();

                fileira.id = id + 1;

                db.fileira.Add(fileira);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.sala = new SelectList(db.sala, "id", "indentificador", fileira.sala);
            return View(fileira);
        }

        // GET: fileira/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fileira fileira = db.fileira.Find(id);
            if (fileira == null)
            {
                return HttpNotFound();
            }
            ViewBag.sala = new SelectList(db.sala, "id", "indentificador", fileira.sala);
            return View(fileira);
        }

        // POST: fileira/Edit/5
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,sala,indentificador,cadeiras_de,cadeiras_ate,pne,pne_de,pne_ate")] fileira fileira)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fileira).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.sala = new SelectList(db.sala, "id", "indentificador", fileira.sala);
            return View(fileira);
        }

        // GET: fileira/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fileira fileira = db.fileira.Find(id);
            if (fileira == null)
            {
                return HttpNotFound();
            }
            return View(fileira);
        }

        // POST: fileira/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            fileira fileira = db.fileira.Find(id);
            db.fileira.Remove(fileira);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
