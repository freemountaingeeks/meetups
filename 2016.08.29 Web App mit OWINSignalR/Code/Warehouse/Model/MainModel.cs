using Microsoft.Owin.Hosting;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Warehouse.Infrastructure;
using Warehouse.Web;

namespace Warehouse.Model
{
    public class MainModel : ModelBase
    {
        #region private members
        private bool _providerIsRunning;
        private readonly Dispatcher _currentDispatcher;
        private ObservableCollection<double> _fillLevels;
        #endregion

        #region properties

        public ObservableCollection<double> FillLevels
        {
            get { return _fillLevels; }
            set
            {
                _fillLevels = value;
                RaisePropertyChanged(() => FillLevels);
            }
        }

        #endregion

        #region Constructur
        public MainModel()
        {
            _currentDispatcher = Dispatcher.CurrentDispatcher;
            FillLevels = new ObservableCollection<double>();
            // Start the fill level provider in a separate thread.
            Task.Run(() => Start());
        }
        #endregion

        private void Start()
        {
            _providerIsRunning = true;
            using (var provider = new RandomFillLevelProvider(5))
            {
                // Subscribe for the update event.
                provider.FillLevelUpdatedEvent += NotifyFillLevelUpdated;

                // Invoke the first event manually, thus the values are immediately updated.
                NotifyFillLevelUpdated(provider, new EventArgs());

                const string url = "http://localhost:8080";
                using (WebApp.Start(url, builder => Startup.Configure(builder, provider)))
                {
                    while (_providerIsRunning)
                    {
                        Thread.Sleep(5000);
                    }
                }
            }
        }

        private void NotifyFillLevelUpdated(object sender, EventArgs args)
        {
            var provider = sender as IFillLevelProvider;
            if (provider == null)
            {
                return;
            }

            _currentDispatcher.Invoke(() =>
            {
                var collection = provider.FillLevels as Collection<double>;
                if (collection == null)
                    return;

                if(FillLevels.Count == 0)
                {
                    for (int i = 0; i < collection.Count; i++)
                    {
                        FillLevels.Add(collection[i]);
                    }
                    RaisePropertyChanged(() => FillLevels);
                    return;
                }

                for (int i = 0; i < collection.Count; i++)
                {
                    FillLevels[i] = collection[i];
                }
                RaisePropertyChanged(() => FillLevels);
            });
        }
    }
}
