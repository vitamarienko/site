//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using site.core;
//using site.core.Models;

//namespace site.Controllers
//{
//    public class ImgSetsController : Controller
//    {
//        private SiteContext db = new SiteContext();

//        // GET: ImgSets
//        public async Task<ActionResult> Index()
//        {
//            return View(await db.ImgSets.ToListAsync());
//        }

//        // GET: ImgSets/Details/5
//        public async Task<ActionResult> Details(string id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            ImgSet imgSet = await db.ImgSets.FindAsync(id);
//            if (imgSet == null)
//            {
//                return HttpNotFound();
//            }
//            return View(imgSet);
//        }

//        // GET: ImgSets/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: ImgSets/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Type,Base64TitleImg")] ImgSet imgSet)
//        {
//            if (ModelState.IsValid)
//            {
//                db.ImgSets.Add(imgSet);
//                await db.SaveChangesAsync();
//                return RedirectToAction("Index");
//            }

//            return View(imgSet);
//        }

//        // GET: ImgSets/Edit/5
//        public async Task<ActionResult> Edit(string id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            ImgSet imgSet = await db.ImgSets.FindAsync(id);
//            if (imgSet == null)
//            {
//                return HttpNotFound();
//            }
//            return View(imgSet);
//        }

//        // POST: ImgSets/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Order,Type,Base64TitleImg")] ImgSet imgSet)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(imgSet).State = EntityState.Modified;
//                await db.SaveChangesAsync();
//                return RedirectToAction("Index");
//            }
//            return View(imgSet);
//        }

//        // GET: ImgSets/Delete/5
//        public async Task<ActionResult> Delete(string id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            ImgSet imgSet = await db.ImgSets.FindAsync(id);
//            if (imgSet == null)
//            {
//                return HttpNotFound();
//            }
//            return View(imgSet);
//        }

//        // POST: ImgSets/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> DeleteConfirmed(string id)
//        {
//            ImgSet imgSet = await db.ImgSets.FindAsync(id);
//            db.ImgSets.Remove(imgSet);
//            await db.SaveChangesAsync();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
