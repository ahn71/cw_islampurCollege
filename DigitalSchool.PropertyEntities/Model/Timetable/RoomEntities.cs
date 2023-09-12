using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.Timetable
{
    public class RoomEntities : IDisposable
    {
        public int RoomId { get; set; }
        public string RoomCode { get; set; }
        public string RoomName { get; set; }
        public int RoomCapacity { get; set; }
        public int BuildingId { get; set; }
        public string BuildingName { get; set; }

        bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
            disposed = true;
        }
    }
}
