using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainReservation.Model
{
    public class Wagon
    {
     
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int Occupancy { get; set; }
        public int TrainId { get; set; }
        //public Train Train { get; set; }
    }
}
