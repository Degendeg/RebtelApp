using System.Linq;
using System.Web.Mvc;
using System.Net;
using System;

namespace RebtelApp.Controllers
{
    [RoutePrefix("subs")]
    public class SubsController : Controller
    {
        #region Init
        public readonly IRebtelDataService _repo;
        public SubsController()
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
        public ActionResult GetAllSubscriptions()
        {
            var subs = _repo.GetAllSubscriptions();
            return Json(subs);
        }

        [AllowJsonGet]
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult GetSubscriptionById(int id)
        {
            var currSub = _repo.GetSubscriptionById(id);
            return Json(currSub);
        }
        #endregion

        #region POST
        [HttpPost]
        [Route("create")]
        public HttpStatusCode CreateSubscription(Subscriptions sub)
        {
            if (ModelState.IsValid)
            {
                var db = new rebtelEntities();

                var _name = Request.QueryString["name"];
                var _price = Request.QueryString["price"];
                var _priceIncVatAmount = Request.QueryString["priceincvatamount"];
                var _callminutes = Request.QueryString["callminutes"];

                db.Subscriptions.Add(new Subscriptions
                {
                    name = _name,
                    price = int.Parse(_price),
                    priceIncVatAmount = int.Parse(_priceIncVatAmount),
                    callminutes = int.Parse(_callminutes)
                });
                db.SaveChanges();

                return HttpStatusCode.OK;
            }
            return HttpStatusCode.InternalServerError;
        }
        #endregion

        #region PUT
        [HttpPut]
        [Route("{subId:int}")]
        public HttpStatusCode UpdateSubscription(int subId)
        {
            if (ModelState.IsValid)
            {
                var db = new rebtelEntities();
                var sub = db.Subscriptions.Where(x => x.id == subId).First();

                var _name = Request.QueryString["name"];
                var _price = Request.QueryString["price"];
                var _priceIncVatAmount = Request.QueryString["priceincvatamount"];
                var _callminutes = Request.QueryString["callminutes"];

                sub.name = _name;
                sub.price = int.Parse(_price);
                sub.priceIncVatAmount = int.Parse(_priceIncVatAmount);
                sub.callminutes = int.Parse(_callminutes);

                db.SaveChanges();

                return HttpStatusCode.OK;
            }
            return HttpStatusCode.InternalServerError;
        }
        #endregion

        #region DELETE
        [HttpDelete]
        [Route("{id:int}")]
        public HttpStatusCode DeleteSubscription(int id)
        {
            if (ModelState.IsValid)
            {
                var db = new rebtelEntities();
                var sub = db.Subscriptions.Where(u => u.id == id).First();
                db.Subscriptions.Remove(sub);
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