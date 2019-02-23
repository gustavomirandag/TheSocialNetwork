 using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheSocialNetwork.DataAccess.Contexts;
using TheSocialNetwork.DomainModel.Entities;

namespace TheSocialNetwork.WebApp.Controllers
{
    public class PhotoAlbumsController : Controller
    {
        private SocialNetworkContext db = new SocialNetworkContext();

        // GET: PhotoAlbums
        public ActionResult Index()
        {
            return View(db.PhotoAlbums.ToList());
        }

        // GET: PhotoAlbums/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhotoAlbum photoAlbum = db.PhotoAlbums.Find(id);
            if (photoAlbum == null)
            {
                return HttpNotFound();
            }
            return View(photoAlbum);
        }

        // GET: PhotoAlbums/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhotoAlbums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,CreationDate")] PhotoAlbum photoAlbum)
        {
            if (ModelState.IsValid)
            {
                photoAlbum.Id = Guid.NewGuid();
                db.PhotoAlbums.Add(photoAlbum);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(photoAlbum);
        }

        // GET: PhotoAlbums/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhotoAlbum photoAlbum = db.PhotoAlbums.Find(id);
            if (photoAlbum == null)
            {
                return HttpNotFound();
            }
            return View(photoAlbum);
        }

        // POST: PhotoAlbums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,CreationDate")] PhotoAlbum photoAlbum)
        {
            if (ModelState.IsValid)
            {
                db.Entry(photoAlbum).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(photoAlbum);
        }

        // GET: PhotoAlbums/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhotoAlbum photoAlbum = db.PhotoAlbums.Find(id);
            if (photoAlbum == null)
            {
                return HttpNotFound();
            }
            return View(photoAlbum);
        }

        // POST: PhotoAlbums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            PhotoAlbum photoAlbum = db.PhotoAlbums.Find(id);
            db.PhotoAlbums.Remove(photoAlbum);
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
