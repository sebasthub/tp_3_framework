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
    public class pagamentoController : Controller
    {
        private CinemaxxContext db = new CinemaxxContext();

        // GET: pagamento
        public ActionResult Index()
        {
            var pagamento = db.pagamento.Include(p => p.ingresso1);
            return View(pagamento.ToList());
        }

        // GET: pagamento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pagamento pagamento = db.pagamento.Find(id);
            if (pagamento == null)
            {
                return HttpNotFound();
            }
            return View(pagamento);
        }

        // GET: pagamento/Create
        public ActionResult Create()
        {
            ViewBag.ingresso = new SelectList(db.ingresso, "id", "id");
            return View();
        }

        // POST: pagamento/Create
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,ingresso,nome_filme,filme,sala,fileira,metodo_pagamento,valor")] pagamento pagamento)
        {

            int id = db.sala
                .OrderByDescending(o => o.id)
                .Select(o => o.id)
                .FirstOrDefault();

            pagamento.id = id + 1;

            if (ModelState.IsValid)
            {
                db.pagamento.Add(pagamento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ingresso = new SelectList(db.ingresso, "id", "id", pagamento.ingresso);
            return View(pagamento);
        }

        // GET: pagamento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pagamento pagamento = db.pagamento.Find(id);
            if (pagamento == null)
            {
                return HttpNotFound();
            }
            ViewBag.ingresso = new SelectList(db.ingresso, "id", "id", pagamento.ingresso);
            return View(pagamento);
        }

        // POST: pagamento/Edit/5
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,ingresso,nome_filme,filme,sala,fileira,metodo_pagamento,valor")] pagamento pagamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pagamento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ingresso = new SelectList(db.ingresso, "id", "id", pagamento.ingresso);
            return View(pagamento);
        }

        // GET: pagamento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pagamento pagamento = db.pagamento.Find(id);
            if (pagamento == null)
            {
                return HttpNotFound();
            }
            return View(pagamento);
        }

        // POST: pagamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            pagamento pagamento = db.pagamento.Find(id);
            db.pagamento.Remove(pagamento);
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
