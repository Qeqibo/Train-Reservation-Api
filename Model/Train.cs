using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainReservation.Model
{
    public class Train
    {
        public Train()
        {
            this.Wagons = new List<Wagon>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        
        public List<Wagon> Wagons { get; set; }
    }
}
