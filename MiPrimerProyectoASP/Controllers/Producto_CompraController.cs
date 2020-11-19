using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiPrimerProyectoASP.Models;

namespace MiPrimerProyectoASP.Controllers
{
    [Authorize]
    public class Producto_CompraController : Controller
    {
        // GET: Producto_Compra
        public ActionResult Index()
        {
            using (var db = new inventarioEntities())
            {
                return View(db.producto_compra.ToList());
            }
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(producto_compra newproducto_compra)
        {

            if (!ModelState.IsValid)
                return View();
            try
            {

                using (var db = new inventarioEntities())
                {
                    db.producto_compra.Add(newproducto_compra);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error" + ex);
                return View();
                throw;
            }

        }
        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    producto_compra findproducto_compra = db.producto_compra.Where(a => a.id == id).FirstOrDefault();
                    return View(findproducto_compra);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error" + ex);
                return View();
                throw;
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(producto_compra updateproducto_compra)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    producto_compra objproducto_compra = db.producto_compra.Find(updateproducto_compra.id);
                    objproducto_compra.id_compra = updateproducto_compra.id_compra;
                    objproducto_compra.id_producto = updateproducto_compra.id_producto;
                    objproducto_compra.cantidad = updateproducto_compra.cantidad;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error" + ex);
                return View();
                throw;
            }
        }
        public ActionResult Details(int id)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    producto_compra findproducto_compra = db.producto_compra.Where(a => a.id == id).FirstOrDefault();
                    return View(findproducto_compra);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error" + ex);
                return View();
                throw;
            }


        }
        public ActionResult Delete(int id)
        {
            try

            {
                using (var db = new inventarioEntities())
                {
                    producto_compra findproducto_compra = db.producto_compra.Find(id);
                    db.producto_compra.Remove(findproducto_compra);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error" + ex);
                return View();
                throw;
            }
        }
        public static string Nombreproducto(int? idProducto)
        {
            using (var db = new inventarioEntities())
            {
                return db.producto.Find(idProducto).nombre;
            }
        }

        public ActionResult Listaproducto()
        {
            using (var db = new inventarioEntities())
            {
                return PartialView(db.producto.ToList());
            }
        }
        public ActionResult Listacompra()
        {
            using (var db = new inventarioEntities())
            {
                return PartialView(db.compra.ToList());
            }
        }

    }
}