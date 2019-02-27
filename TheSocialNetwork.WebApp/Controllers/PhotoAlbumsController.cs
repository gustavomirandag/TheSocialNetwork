 using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TheSocialNetwork.DataAccess.Contexts;
using TheSocialNetwork.DataAccess.Repositories;
using TheSocialNetwork.DomainModel.Entities;
using TheSocialNetwork.DomainService;

namespace TheSocialNetwork.WebApp.Controllers
{
    public class PhotoAlbumsController : Controller
    {
        private SocialNetworkContext db = new SocialNetworkContext();
        private readonly PhotoAlbumService _photoAlbumService;

        public PhotoAlbumsController()
        {
            //Simulando uma injeção de dependência
            _photoAlbumService = new PhotoAlbumService(
                new PhotoAlbumEntityFrameworkRepository(
                    new SocialNetworkContext()),
                    new PhotoAzureBlobRepository());
        }

        // GET: PhotoAlbums
        public ActionResult Index()
        {
            //var photoAlbums = _photoAlbumService
            //    .GetAllPhotoAlbums(
            //        Guid.Parse(Session["profileId"].ToString()
            //    )
            //);
            /*var photoAlbums = db.PhotoAlbums;
            var profilePhotoAlbums = photoAlbums.Where(p => p.Profile.Id == Guid.Parse(Session["profileId"].ToString()));*/

            var profilePhotoAlbums = db.PhotoAlbums;

            return View(profilePhotoAlbums);
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
                photoAlbum.Profile = db.Profiles.Find(Guid.Parse(Session["profileId"].ToString()));
                db.PhotoAlbums.Add(photoAlbum);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(photoAlbum);
        }

        [HttpPost]
        public async Task<ActionResult> UploadPhotoAsync()
        {
            if (Session["photoAlbumId"] == null)
                return HttpNotFound();

            var files = Request.Files;
            for (int i=0; i<files.Count; i++)
            {
                var photo = new Photo
                {
                    ContainerName = "profilepictures",
                    FileName = files[i].FileName,
                    BinaryContent = files[i].InputStream,
                    ContentType = files[i].ContentType
                };

                await _photoAlbumService.AddPhotoToAlbumAsync(photo, Guid.Parse(Session["photoAlbumId"].ToString()));

            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        // GET: PhotoAlbums/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhotoAlbum photoAlbum = db.PhotoAlbums.Find(id);

            //If it's not my album, show error
            //if (photoAlbum.Profile.Id != Guid.Parse(Session["profileId"].ToString()))
            //    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

            Session["photoAlbumId"] = photoAlbum.Id;
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
