using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiPrimerProyectoASP.Models;

namespace MiPrimerProyectoASP.Controllers
{

    [Authorize]
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            using (var db = new inventarioEntities())
            {
                return View(db.producto.ToList());
            }

        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(producto newproducto)
        {

            if (!ModelState.IsValid)
                return View();
            try
            {

                using (var db = new inventarioEntities())
                {
                    db.producto.Add(newproducto);
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
                    producto findProducto = db.producto.Where(a => a.id == id).FirstOrDefault();
                    return View(findProducto);
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
        public ActionResult Edit(producto updateproducto)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    producto objproducto = db.producto.Find(updateproducto.id);
                    objproducto.nombre = updateproducto.nombre;
                    objproducto.percio_unitario = updateproducto.percio_unitario;
                    objproducto.descripcion = updateproducto.descripcion;
                    objproducto.cantidad = updateproducto.cantidad;
                    objproducto.id_proveedor = updateproducto.id_proveedor;
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
        public static string NombreProveedor(int? idProveedor) 
        {
        using (var db = new inventarioEntities()) 
            {
                return db.proveedor.Find(idProveedor).nombre;
            }
        }
        
        public ActionResult ListaProveedores() 
        {
        using (var db = new inventarioEntities()) 
            {
                return PartialView(db.proveedor.ToList());
            }
        }
        public ActionResult Details(int id)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    producto findProducto = db.producto.Where(a => a.id == id).FirstOrDefault();
                    return View(findProducto);
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
                    producto findProducto = db.producto.Find(id);
                    db.producto.Remove(findProducto);
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


    }
    

}