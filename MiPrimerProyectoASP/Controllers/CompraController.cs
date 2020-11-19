using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiPrimerProyectoASP.Models;

namespace MiPrimerProyectoASP.Controllers
{
    [Authorize]
    public class CompraController : Controller
    {
        // GET: Compra
        public ActionResult Index()
        {
            using (var db = new inventarioEntities())
            {
                return View(db.compra.ToList());
            }
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(compra newcompra)
        {

            if (!ModelState.IsValid)
                return View();
            try
            {

                using (var db = new inventarioEntities())
                {
                    db.compra.Add(newcompra);
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
                    compra findCompra = db.compra.Where(a => a.id == id).FirstOrDefault();
                    return View(findCompra);
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
        public ActionResult Edit(compra updateCompra)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    compra objcompra = db.compra.Find(updateCompra.id);
                    objcompra.fecha = updateCompra.fecha;
                    objcompra.total = updateCompra.total;
                    objcompra.id_usuario = updateCompra.id_usuario;
                    objcompra.id_cliente = updateCompra.id_usuario;
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
                    compra findCompra = db.compra.Where(a => a.id == id).FirstOrDefault();
                    return View(findCompra);
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
                    compra findCompra = db.compra.Find(id);
                    db.compra.Remove(findCompra);
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
        public static string NombreUsuario(int? idUsuario)
        {
            using (var db = new inventarioEntities())
            {
                return db.usuario.Find(idUsuario).nombre;
            }
        }

        public ActionResult ListaUsuarios()
        {
            using (var db = new inventarioEntities())
            {
                return PartialView(db.usuario.ToList());
            }
        }

        public static string NombreClidente(int? idCliente)
        {
            using (var db = new inventarioEntities())
            {
                return db.cliente.Find(idCliente).nombre;
            }
        }

        public ActionResult ListaClientes()
        {
            using (var db = new inventarioEntities())
            {
                return PartialView(db.cliente.ToList());
            }
        }
        public ActionResult DetalleCompra()
        {
            var db = new inventarioEntities();
            var query = from compra in db.compra
                        join cliente in db.cliente on compra.id_cliente equals cliente.id
                        select new CompraCliente
                        {
                            fechaCompra = compra.fecha,
                            totalCompra = compra.total,
                            nombreCliente = cliente.nombre,
                            documentoCliente = cliente.documento                            
                        };
            return View(query);
        }
    }

}