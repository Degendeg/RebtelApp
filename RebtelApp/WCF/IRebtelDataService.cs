using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RebtelApp
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRebtelDataService" in both code and config file together.
    [ServiceContract]
    public interface IRebtelDataService
    {
        [OperationContract]
        List<Users> GetAllUsers();
        Users GetUserById(int id);
        List<Subscriptions> GetAllSubscriptions();
        Subscriptions GetSubscriptionById(int id);
    }
}
