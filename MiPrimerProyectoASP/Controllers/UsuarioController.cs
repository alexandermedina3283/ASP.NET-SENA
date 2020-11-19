using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.WebSockets;
using MiPrimerProyectoASP.Models;

namespace MiPrimerProyectoASP.Controllers
{
   
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            using (var db = new inventarioEntities()) 
            {
                return View(db.usuario.ToList());
            }
                
        }
        public ActionResult Create() 
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(usuario newuser) 
        {
            if (!ModelState.IsValid)
                return View();
            try
            {

                using(var db = new inventarioEntities()) 
                {
                    db.usuario.Add(newuser);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex) 
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
                    usuario findUser = db.usuario.Where(a => a.id == id).FirstOrDefault();
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
        public ActionResult Edit (usuario updateUser) 
        {
            try 
            {
            using (var db = new inventarioEntities()) 
                {
                    usuario objUser = db.usuario.Find(updateUser.id);
                    objUser.nombre = updateUser.nombre;
                    objUser.apellido = updateUser.apellido;
                    objUser.email = updateUser.email;
                    objUser.fecha_nacimiento = updateUser.fecha_nacimiento;
                    objUser.password = updateUser.password;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
            }
            catch(Exception ex) 
            {
                ModelState.AddModelError("", "Error" + ex);
                return View();
                throw;
            }
        }
        public ActionResult Details (int id) 
        {
            try 
            {
            using (var db = new inventarioEntities()) 
                {
                    usuario findUser = db.usuario.Where(a => a.id == id).FirstOrDefault();
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
        public ActionResult Delete (int id) 
        {
            try 
            
            {
            using(var db = new inventarioEntities()) 
                {
                    usuario findUser = db.usuario.Find(id);
                    db.usuario.Remove(findUser);
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
        public ActionResult Login (string message = "") 
        {
            ViewBag.Message = message;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Login(string user, string password) 
        {
        using (var db = new inventarioEntities()) 
            {
                var userLogin = db.usuario.FirstOrDefault(e => e.email == user && e.password == password);
                if(userLogin != null) 
                {
                    FormsAuthentication.SetAuthCookie(userLogin.email, true);
                    return RedirectToAction("Index", "producto");
                }
                else 
                {
                    return Login("Verifique sus datos");
                }
            }
        }
        [Authorize]
        public ActionResult Logout() 
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        

       

    }
}