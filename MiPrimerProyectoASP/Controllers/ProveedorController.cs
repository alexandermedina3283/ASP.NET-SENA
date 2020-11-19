using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiPrimerProyectoASP.Models;

namespace MiPrimerProyectoASP.Controllers
{
    [Authorize]
    public class ProveedorController : Controller
    {
        // GET: Proveedor
        public ActionResult Index()
        {
            using (var db = new inventarioEntities())
            {
                return View(db.proveedor.ToList());
            }
        }
        public ActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(proveedor newproveedor) 
        {

            if (!ModelState.IsValid)
                return View();
            try
            {

                using (var db = new inventarioEntities())
                {
                    db.proveedor.Add(newproveedor);
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
                    proveedor findProveedor = db.proveedor.Where(a => a.id == id).FirstOrDefault();
                    return View(findProveedor);
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
        public ActionResult Edit(proveedor updateproveedor) 
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    proveedor objproveedor = db.proveedor.Find(updateproveedor.id);
                    objproveedor.nombre = updateproveedor.nombre;
                    objproveedor.direccion = updateproveedor.direccion;
                    objproveedor.telefono = updateproveedor.telefono;
                    objproveedor.nombre_contacto = updateproveedor.nombre_contacto;
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
                    proveedor findUser = db.proveedor.Where(a => a.id == id).FirstOrDefault();
                    return View(findUser);
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
                    proveedor findUser = db.proveedor.Find(id);
                    db.proveedor.Remove(findUser);
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
        public ActionResult DetalleProveedorProducto() 
        {
            var db = new inventarioEntities();
            var query = from proveedor in db.proveedor
                        join producto in db.producto on proveedor.id equals producto.id_proveedor
                        select new ProductoProveedor
                        {
                            nombreProveedor = proveedor.nombre,
                            nombreProducto = producto.nombre,
                            telefonoProveedor = proveedor.telefono,
                            descripcion = producto.descripcion,
                            precioUnitario = producto.percio_unitario
                        };
            return View(query);
        }
    }
}