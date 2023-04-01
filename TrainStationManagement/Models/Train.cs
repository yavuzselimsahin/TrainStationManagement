using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainStationManagement.Models
{
    public class Train
    {
        [Key]
        public int TrainId { get; set; }
        
        public int DepartureStationId { get; set; }
        
        public int ArrivalStationId { get; set; }
        
        public DateTime DepartureTime { get; set; }
        
        public DateTime ArrivalTime { get; set; }

        
        public virtual TrainStation? DepartureStation { get; set; }

        
        public virtual TrainStation? ArrivalStation { get; set; }
    }
}
