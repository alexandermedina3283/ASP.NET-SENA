using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiPrimerProyectoASP.Models;

namespace MiPrimerProyectoASP.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            using (var db = new inventarioEntities())
            {
                return View(db.cliente.ToList());
            }
        }
        public ActionResult Create()
        {
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create (cliente newcliente) 
        {
            if (!ModelState.IsValid)
                return View();
            try
            {

                using (var db = new inventarioEntities())
                {
                    db.cliente.Add(newcliente);
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
                    cliente findUser = db.cliente.Where(a => a.id == id).FirstOrDefault();
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
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(cliente updatecliente) 
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    cliente objcliente = db.cliente.Find(updatecliente.id);
                    objcliente.nombre = updatecliente.nombre;
                    objcliente.documento = updatecliente.documento;
                    objcliente.email = updatecliente.email;
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
                    cliente findUser = db.cliente.Where(a => a.id == id).FirstOrDefault();
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
                    cliente findUser = db.cliente.Find(id);
                    db.cliente.Remove(findUser);
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