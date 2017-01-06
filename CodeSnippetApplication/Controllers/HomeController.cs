using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CodeSnippetApplication.Models;

namespace CodeSnippetApplication.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // retrieve code snippet based on dropdown list selection
        [HttpPost]
        public JsonResult GetCodeData(int snippetID)
        {
            
            CodeSnippet returnedsnippet = db.CodeSnippets.FirstOrDefault(d => d.Id == snippetID);
            if (returnedsnippet != null)
            {
                return Json(new { success = true, snippetCode = returnedsnippet.SnippetCode });
            }
            return Json(new { success = false });

        }

        // GET: /Home/
        public ActionResult Index()
        {            
            var snippetlist = db.CodeSnippets.Select(
                s => new
                {
                    Text = s.SnippetName,
                    Value = s.Id
                }
                ).ToList();
            ViewBag.SnippetList = new SelectList(snippetlist, "Value", "Text");
            return View();
        }

        // GET: /Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodeSnippet codesnippet = db.CodeSnippets.Find(id);
            if (codesnippet == null)
            {
                return HttpNotFound();
            }
            return View(codesnippet);
        }

        // GET: /Home/Create
        [Authorize(Users = "mattdailey")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,SnippetName,SnippetCode")] CodeSnippet codesnippet)
        {
            if (ModelState.IsValid)
            {
                db.CodeSnippets.Add(codesnippet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(codesnippet);
        }

        // GET: /Home/Edit/5        
        [Authorize(Users="mattdailey")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodeSnippet codesnippet = db.CodeSnippets.Find(id);
            if (codesnippet == null)
            {
                return HttpNotFound();
            }
            return View(codesnippet);
        }

        // POST: /Home/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,SnippetName,SnippetCode")] CodeSnippet codesnippet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(codesnippet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(codesnippet);
        }
        [Authorize(Users = "mattdailey")]
        public ActionResult EditList() 
        {
            return View(db.CodeSnippets.ToList());
        }

        // GET: /Home/Delete/5
        [Authorize(Users = "mattdailey")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodeSnippet codesnippet = db.CodeSnippets.Find(id);
            if (codesnippet == null)
            {
                return HttpNotFound();
            }
            return View(codesnippet);
        }

        // POST: /Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CodeSnippet codesnippet = db.CodeSnippets.Find(id);
            db.CodeSnippets.Remove(codesnippet);
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
