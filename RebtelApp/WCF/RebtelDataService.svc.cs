using System.Collections.Generic;
using System.Linq;

namespace RebtelApp
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RebtelDataService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select RebtelDataService.svc or RebtelDataService.svc.cs at the Solution Explorer and start debugging.
    public class RebtelDataService : IRebtelDataService
    {
        private readonly rebtelEntities _entities;

        public RebtelDataService()
        {
        }

        public RebtelDataService(rebtelEntities entities)
        {
            _entities = entities;
        }

        public List<Users> GetAllUsers()
        {
            var users = _entities.Users.ToList();
            return users;
        }

        public Users GetUserById(int id)
        {
            var user = _entities.Users.First(x => x.id == id);
            return user;
        }

        public List<Subscriptions> GetAllSubscriptions()
        {
            var subs = _entities.Subscriptions.ToList();
            return subs;
        }

        public Subscriptions GetSubscriptionById(int id)
        {
            var sub = _entities.Subscriptions.First(x => x.id == id);
            return sub;
        }
    }
}
