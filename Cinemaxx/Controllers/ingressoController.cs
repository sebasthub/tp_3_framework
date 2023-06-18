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
    public class ingressoController : Controller
    {
        private CinemaxxContext db = new CinemaxxContext();

        // GET: ingresso
        public ActionResult Index()
        {
            var ingresso = db.ingresso.Include(i => i.fileira1).Include(i => i.programacao1).Include(i => i.usuario1);
            return View(ingresso.ToList());
        }

        // GET: ingresso/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ingresso ingresso = db.ingresso.Find(id);
            if (ingresso == null)
            {
                return HttpNotFound();
            }
            return View(ingresso);
        }

        // GET: ingresso/Create
        public ActionResult Create()
        {
            ViewBag.fileira = new SelectList(db.fileira, "id", "indentificador");
            ViewBag.programacao = new SelectList(db.programacao, "id", "id");
            ViewBag.usuario = new SelectList(db.usuario, "id", "nome");
            return View();
        }

        // POST: ingresso/Create
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,cadeira,fileira,programacao,usuario")] ingresso ingresso)
        {

            int id = db.sala
                .OrderByDescending(o => o.id)
                .Select(o => o.id)
                .FirstOrDefault();

            ingresso.id = id + 1;

            if (ModelState.IsValid)
            {
                db.ingresso.Add(ingresso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fileira = new SelectList(db.fileira, "id", "indentificador", ingresso.fileira);
            ViewBag.programacao = new SelectList(db.programacao, "id", "id", ingresso.programacao);
            ViewBag.usuario = new SelectList(db.usuario, "id", "nome", ingresso.usuario);
            return View(ingresso);
        }

        // GET: ingresso/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ingresso ingresso = db.ingresso.Find(id);
            if (ingresso == null)
            {
                return HttpNotFound();
            }
            ViewBag.fileira = new SelectList(db.fileira, "id", "indentificador", ingresso.fileira);
            ViewBag.programacao = new SelectList(db.programacao, "id", "id", ingresso.programacao);
            ViewBag.usuario = new SelectList(db.usuario, "id", "nome", ingresso.usuario);
            return View(ingresso);
        }

        // POST: ingresso/Edit/5
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,cadeira,fileira,programacao,usuario")] ingresso ingresso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ingresso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fileira = new SelectList(db.fileira, "id", "indentificador", ingresso.fileira);
            ViewBag.programacao = new SelectList(db.programacao, "id", "id", ingresso.programacao);
            ViewBag.usuario = new SelectList(db.usuario, "id", "nome", ingresso.usuario);
            return View(ingresso);
        }

        // GET: ingresso/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ingresso ingresso = db.ingresso.Find(id);
            if (ingresso == null)
            {
                return HttpNotFound();
            }
            return View(ingresso);
        }

        // POST: ingresso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ingresso ingresso = db.ingresso.Find(id);
            db.ingresso.Remove(ingresso);
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
