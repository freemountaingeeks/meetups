using Microsoft.AspNet.SignalR;

namespace Warehouse.Web.Hubs
{
    public class FillLevelHub : Hub
    {
        public void RequestFillLevelUpdate()
        {
            FillLevelObserver.UpdateFillLevel(Clients.Caller);
        }

       public void Activate()
       {
            FillLevelObserver.Activate(Clients.Caller);
       }


    }
}