using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainStationManagement.Models
{
    public class TrainStation
    {
        [Key]
        public int StationId { get; set; }

        public string StationName { get; set; }

        public string StationAddress { get; set; }

        public string? StationLocation { get; set; }

        public virtual ICollection<Train>? DepartureTrains { get; set; }

        public virtual ICollection<Train>? ArrivalTrains { get; set; }
    }
}
