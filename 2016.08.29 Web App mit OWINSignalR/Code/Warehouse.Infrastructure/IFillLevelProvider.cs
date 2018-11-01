using System;
using System.Collections.Generic;

namespace Warehouse.Infrastructure
{
    public interface IFillLevelProvider : IDisposable
    {
        IEnumerable<double> FillLevels { get; }

        event EventHandler FillLevelUpdatedEvent;
    }
}

