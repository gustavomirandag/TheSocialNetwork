using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TheSocialNetwork.AzureStorageAccount;
using TheSocialNetwork.DataAccess.Contexts;
using TheSocialNetwork.DataAccess.Repositories;
using TheSocialNetwork.DomainModel.Entities;
using TheSocialNetwork.DomainModel.Interfaces.Repositories;
using TheSocialNetwork.DomainService;

namespace TheSocialNetwork.WebApp.Controllers
{
    public class ProfilesController : Controller
    {
        private static IProfileRepository _profileRepository = new ProfileEntityFrameworkRepository();
        private ProfileService _profileService = new ProfileService(_profileRepository);
        private FriendshipService _friendshipService = new FriendshipService(_profileRepository);
        private SocialNetworkContext db = new SocialNetworkContext();
        private readonly PhotoService _fileService;

        public ProfilesController()
        {
            //Simulando uma injeção de dependência
            _fileService = new PhotoService(new PhotoAzureBlobRepository());
        }

        // GET: Profiles
        public ActionResult Index()
        {
            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("https://thesocialnetworkprofilewebapi20190227095906.azurewebsites.net");
            //client.DefaultRequestHeaders.Accept.Add(
            //    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            ////HTTP Get
            //HttpResponseMessage response = client.GetAsync("api/profiles").Result;
            //string serializedProfilesCollection = response.Content.ReadAsStringAsync().Result;
            //Profile[] profiles = Newtonsoft
            //    .Json.JsonConvert
            //    .DeserializeObject<Profile[]>(serializedProfilesCollection);

            var unknownProfiles = _friendshipService
                .GetUnknownProfiles(Guid.Parse(Session["profileId"].ToString()));

            return View(unknownProfiles);

            //return RedirectToAction("Index", "Home");
            //return View(db.Profiles.ToList());
        }

        // GET: Profiles/Details/5
        public ActionResult Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = _profileService.GetProfile(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        public ActionResult AddFriend(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guid profileId = Guid.Parse(Session["profileId"].ToString());
            _friendshipService.AddFriend(profileId, id);

            return RedirectToAction("Details", new { id = profileId });
        }

        public ActionResult UnFriend(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guid profileId = Guid.Parse(Session["profileId"].ToString());
            _friendshipService.UnFriend(profileId, id);

            return RedirectToAction("Details", new { id = profileId });
        }

        // GET: Profiles/Create
        public ActionResult Create()
        {
            if (Session["profileId"] == null)
                RedirectToAction("Index", "Home");

            //Por segurança, se o usuário tentar chamar a ação de criação de perfil
            //sem estar autenticado (com profile na Session), mando ele de volta pra
            //página principal
            if (Session["profileId"] == null)
                return RedirectToAction("Index", "Home");

            //Se existe um perfil na Session, termino de preencher ele
            return View();
        }

        // POST: Profiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Birthday,PhotoUrl")] Profile profile, HttpPostedFileBase binaryFile)
        {
            if (ModelState.IsValid)
            {
                if (binaryFile != null)
                {
                    var photo = new Photo
                    {
                        ContainerName = "profilepictures",
                        FileName = binaryFile.FileName,
                        BinaryContent = binaryFile.InputStream,
                        ContentType = binaryFile.ContentType
                    };
                    string newPhotoUrl = _fileService.UploadPhoto(photo);
                    profile.PhotoUrl = newPhotoUrl;
                }
                profile.Id = Guid.Parse(Session["profileId"].ToString());
                //db.Profiles.Add(profile);
                //db.SaveChanges();
                _profileService.CreateProfile(profile);
                return RedirectToAction("Details", new {@id = Session["profileId"].ToString()});
            }

            return View(profile);
        }

        // GET: Profiles/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (Session["profileId"] == null)
                RedirectToAction("Index", "Home");

            if (id == null || id.ToString() != Session["profileId"].ToString())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Profile profile = db.Profiles.Find(id);
            Profile profile = _profileService.GetProfile(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Birthday,PhotoUrl")] Profile profile, HttpPostedFileBase binaryFile)
        {
            if (ModelState.IsValid)
            {
                if (binaryFile != null)
                {
                    var photo = new Photo
                    {
                        ContainerName = "profilepictures",
                        FileName = binaryFile.FileName,
                        BinaryContent = binaryFile.InputStream,
                        ContentType = binaryFile.ContentType
                    };
                    string newPhotoUrl = await _fileService.UploadPhotoAsync(photo);
                    profile.PhotoUrl = newPhotoUrl;
                }

                db.Entry(profile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profile);
        }

        // GET: Profiles/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (Session["profileId"] == null)
                RedirectToAction("Index", "Home");

            if (id == null || id.ToString() != Session["profileId"].ToString())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // POST: Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Profile profile = db.Profiles.Find(id);
            db.Profiles.Remove(profile);
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
