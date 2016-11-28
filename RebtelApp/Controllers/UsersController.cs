using System.Linq;
using System.Web.Mvc;
using System.Net;
using System;

namespace RebtelApp.Controllers
{
    [RoutePrefix("users")]
    public class UsersController : Controller
    {
        #region Init
        public readonly IRebtelDataService _repo;
        public UsersController()
        {
            var entities = new rebtelEntities();
            var repo = new RebtelDataService(entities);
            _repo = repo;
        }
        #endregion

        #region GET
        [AllowJsonGet]
        [HttpGet]
        [Route("")]
        public ActionResult GetAllUsers()
        {
            var users = _repo.GetAllUsers();
            return Json(users);
        }

        [AllowJsonGet]
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult GetUserById(int id)
        {
            var currUser = _repo.GetUserById(id);
            return Json(currUser);
        }
        #endregion

        #region POST
        [HttpPost]
        [Route("create")]
        public HttpStatusCode CreateUser(Users user)
        {
            if (ModelState.IsValid)
            {
                var db = new rebtelEntities();

                var _email = Request.QueryString["email"];
                var _firstname = Request.QueryString["firstname"];
                var _lastname = Request.QueryString["lastname"];

                db.Users.Add(new Users
                {
                    email = _email,
                    firstname = _firstname,
                    lastname = _lastname
                });
                db.SaveChanges();

                return HttpStatusCode.OK;
            }
            return HttpStatusCode.InternalServerError;
        }
        #endregion

        #region PUT
        [HttpPut]
        [Route("{userId:int}/{subId:int}")]
        public HttpStatusCode AddSubscription(int userId, string subId)
        {
            if (ModelState.IsValid)
            {
                var db = new rebtelEntities();
                var user = db.Users.Where(x => x.id == userId).First();
                user.subId = subId;
                db.SaveChanges();

                return HttpStatusCode.OK;
            }
            return HttpStatusCode.InternalServerError;
        }
        #endregion

        #region DELETE
        [HttpDelete]
        [Route("{id:int}")]
        public HttpStatusCode DeleteUser(int id)
        {
            if (ModelState.IsValid)
            {
                var db = new rebtelEntities();
                var user = db.Users.Where(u => u.id == id).First();
                db.Users.Remove(user);
                db.SaveChanges();

                return HttpStatusCode.NoContent;
            }
            return HttpStatusCode.InternalServerError;
        }
        #endregion

        #region Helpers
        public class AllowJsonGetAttribute : ActionFilterAttribute
        {
            public override void OnResultExecuting(ResultExecutingContext filterContext)
            {
                var jsonResult = filterContext.Result as JsonResult;

                if (jsonResult == null)
                    throw new ArgumentException("Action does not return a JsonResult, attribute AllowJsonGet is not allowed");

                jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

                base.OnResultExecuting(filterContext);
            }
        }
        #endregion
    }
}