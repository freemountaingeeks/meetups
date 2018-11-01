using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace Warehouse.Infrastructure
{
    public class RandomFillLevelProvider : IFillLevelProvider
    {
        #region private members
        private Collection<double> _fillLevels;
        private readonly TimeSpan _updateInterval = TimeSpan.FromSeconds(1);
        private Timer _timer;
        private readonly object _updateLock = new object();
        private bool _isUpdating = false;
        private Random _random;
        private int _Count;
        #endregion

        #region Constructor
        public RandomFillLevelProvider(int count)
        {
            _Count = count;
            _fillLevels = new Collection<double>();
            for (int i = 0; i < _Count; i++)
            {
                _fillLevels.Add(0.0);
            }            
            _random = new Random();

            _timer = new Timer(UpdateLevels, null, _updateInterval, _updateInterval);
        }
        #endregion

        #region Private methods          

        private void UpdateLevels(object state)
        {
            lock (_updateLock)
            {
                if (_isUpdating)
                {
                    return;
                }

                _isUpdating = true;

                _fillLevels.Clear();
                for (int i = 0; i < _Count; i++)
                {
                    _fillLevels.Add(_random.NextDouble() * 100);
                }                

                var handler = FillLevelUpdatedEvent;
                if (handler != null)
                {
                    handler(this, new EventArgs());
                }

                _isUpdating = false;
            }
        }

        #endregion

        #region Implementation IFillLevelProvider
        IEnumerable<double> IFillLevelProvider.FillLevels
        {
            get
            {
                return _fillLevels;
            }
        }

        public event EventHandler FillLevelUpdatedEvent;
        #endregion

        #region IDisposable members
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_timer != null)
                {
                    _timer.Dispose();
                    _timer = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
