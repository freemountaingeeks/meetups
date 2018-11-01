using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Infrastructure;
using System;
using System.Collections.ObjectModel;
using Warehouse.Infrastructure;
using Warehouse.Web.Hubs;

namespace Warehouse.Web
{
    public class FillLevelObserver
    {
        #region Private fields and properties

        private static FillLevelObserver _instance;
        private bool _active;
        private Collection<double> _fillLevels;        
        private readonly IDependencyResolver _dependencyResolver;
        private IHubConnectionContext<dynamic> _clients;

        private IHubConnectionContext<dynamic> Clients
        {
            get
            {
                if (_clients != null)
                {
                    return _clients;
                }

                var connectionManager = _dependencyResolver.GetService(typeof(IConnectionManager)) as IConnectionManager;
                if (connectionManager == null)
                {
                    return null;
                }

                _clients = connectionManager.GetHubContext<FillLevelHub>().Clients;
                return _clients;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FillLevelObserver"/> class.
        /// </summary>
        /// <param name="dependencyResolver">The dependency resolver.</param>
        /// <param name="provider">The fill level provider.</param>
        private FillLevelObserver(IDependencyResolver dependencyResolver, IFillLevelProvider provider)
        {
            _fillLevels = new Collection<double>();
            _active = true;
            _dependencyResolver = dependencyResolver;
            provider.FillLevelUpdatedEvent += NotifyFillLevelUpdated;
        }

        #endregion

        #region Private methods

        private void NotifyFillLevelUpdated(object sender, EventArgs e)
        {
            var provider = sender as IFillLevelProvider;
            if (provider == null)
            {
                return;
            }

            var collection = provider.FillLevels as Collection<double>;
            if (collection == null)
                return;

            if (_fillLevels.Count == 0)
            {
                for (int i = 0; i < collection.Count; i++)
                {
                    _fillLevels.Add(collection[i]);
                }
                UpdateFillLevel(Clients.All);
                return;
            }

            for (int i = 0; i < collection.Count; i++)
            {
                _fillLevels[i] = collection[i];
            }

            UpdateFillLevel(Clients.All);
        }

        #endregion

        #region public methods

        /// <summary>
        /// Initializes the instance.
        /// </summary>
        /// <param name="dependencyResolver">The dependency resolver.</param>
        /// <param name="provider">The fill level provider.</param>
        public static void Init(IDependencyResolver dependencyResolver, IFillLevelProvider provider)
        {
            _instance = new FillLevelObserver(dependencyResolver, provider);
        }

        /// <summary>
        /// Updates the fill level for all callers with the latest fill level values.
        /// </summary>
        /// <param name="caller">The caller.</param>
        public static void UpdateFillLevel(dynamic caller)
        {
            if (_instance == null)
            {
                return;
            }

            if (_instance._active)
            {
                caller.updateFillLevel(_instance._fillLevels);
            }
        }

        

        internal static void Activate(dynamic caller)
        {
            if (_instance == null)
            {
                return;
            }

            _instance._active = !_instance._active;            
        }

        #endregion
    }
}