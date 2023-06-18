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
    public class programacaoController : Controller
    {
        private CinemaxxContext db = new CinemaxxContext();

        // GET: programacao
        public ActionResult Index()
        {
            var programacao = db.programacao.Include(p => p.filme1).Include(p => p.sala1);
            return View(programacao.ToList());
        }

        // GET: programacao/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            programacao programacao = db.programacao.Find(id);
            if (programacao == null)
            {
                return HttpNotFound();
            }
            return View(programacao);
        }

        // GET: programacao/Create
        public ActionResult Create()
        {
            ViewBag.filme = new SelectList(db.filme, "id", "nome");
            ViewBag.sala = new SelectList(db.sala, "id", "indentificador");
            return View();
        }

        // POST: programacao/Create
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "sala,filme,horario")] programacao programacao)
        {

            var horarios = db.programacao.Where(a => a.sala == programacao.sala);

            foreach (var item in horarios)
            {
                if (item.horario == programacao.horario)
                {
                    ModelState.AddModelError("Horario","o horario é igual a outro");
                }
            }

            foreach (var item in horarios)
            {
                var sleep = item.horario + TimeSpan.Parse("00:30:00");
                if (item.horario < programacao.horario && sleep > programacao.horario)
                {
                    ModelState.AddModelError("Horario", "o horario é menor que o tempo de descanso(deve ser maior que 30 min)");
                }
            }

            int? id = db.programacao
        .OrderByDescending(o => o.id)
        .Select(o => o.id)
        .FirstOrDefault();

            if (ModelState.IsValid)
            {
                programacao.id = id + 1;
                db.programacao.Add(programacao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.filme = new SelectList(db.filme, "id", "nome", programacao.filme);
            ViewBag.sala = new SelectList(db.sala, "id", "indentificador", programacao.sala);
            return View(programacao);
        }

        // GET: programacao/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            programacao programacao = db.programacao.Find(id);
            if (programacao == null)
            {
                return HttpNotFound();
            }
            ViewBag.filme = new SelectList(db.filme, "id", "nome", programacao.filme);
            ViewBag.sala = new SelectList(db.sala, "id", "indentificador", programacao.sala);
            return View(programacao);
        }

        // POST: programacao/Edit/5
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,sala,filme,horario")] programacao programacao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(programacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.filme = new SelectList(db.filme, "id", "nome", programacao.filme);
            ViewBag.sala = new SelectList(db.sala, "id", "indentificador", programacao.sala);
            return View(programacao);
        }

        // GET: programacao/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            programacao programacao = db.programacao.Find(id);
            if (programacao == null)
            {
                return HttpNotFound();
            }
            return View(programacao);
        }

        // POST: programacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            programacao programacao = db.programacao.Find(id);
            db.programacao.Remove(programacao);
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
