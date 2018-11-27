
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication6.Models;
using MercadoPago;
using MercadoPago.Resources;
using MercadoPago.DataStructures.Preference;

namespace WebApplication6.Controllers
{
    public class ViajesController : Controller
    {
        private TaxiContexto db = new TaxiContexto();

        // GET: Viajes
        public ActionResult Index()
        {
            //var user = db.Usuarios.Find(1);
           // var viajes = db.Viajes.Where(b => b.Usuario.Id == user.Id);
            return View(db.Viajes.ToList());
        }
        public ActionResult Consultar()
        {
            return View();
        }

        // GET: Viajes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Viaje viaje = db.Viajes.Find(id);
            if (viaje == null)
            {
                return HttpNotFound();
            }
            return View(viaje);
        }

        // GET: Viajes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Viajes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Origen,Destino,Costo")] Viaje viaje)
        {
            if (ModelState.IsValid)
            {
                viaje.CalcularCosto();

                MercadoPago.SDK.ClientId = "623228491029736";
                MercadoPago.SDK.ClientSecret = "Ccs4Q7fmPE7qIQK4sDVQk9qsPTZvIg3u";
                Preference preference = new Preference();
                BackUrls backUrls = new BackUrls();
                backUrls.Success = "http://localhost:54555/Viajes/Index";
                preference.BackUrls = backUrls;
                preference.Items.Add(
                  new Item()
                  {
                      Id = "1234",
                      Title = "Taxi desde " + viaje.Origen + "hacia " + viaje.Destino,
                      Quantity = 1,
                      CurrencyId = 0,
                      UnitPrice = (decimal)viaje.Costo
                  }
                );
                // Setting a payer object as value for Payer property

                preference.Payer = new Payer()
                {
                    Email = "pepito@hotmail.com"
                };

                preference.Save();
                db.Viajes.Add(viaje);
                db.SaveChanges();

                return Redirect(preference.SandboxInitPoint);


                
                
            }

            return View(viaje);
           
        }
        

        // GET: Viajes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Viaje viaje = db.Viajes.Find(id);
            if (viaje == null)
            {
                return HttpNotFound();
            }
            return View(viaje);
        }

        // POST: Viajes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Origen,Destino,Costo")] Viaje viaje)
        {
            if (ModelState.IsValid)
            {
                db.Entry(viaje).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viaje);
        }

        // GET: Viajes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Viaje viaje = db.Viajes.Find(id);
            if (viaje == null)
            {
                return HttpNotFound();
            }
            return View(viaje);
        }

        // POST: Viajes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Viaje viaje = db.Viajes.Find(id);
            db.Viajes.Remove(viaje);
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
