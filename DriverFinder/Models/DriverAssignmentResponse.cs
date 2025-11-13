namespace DriverFinder.Models
{
    public class DriverAssignmentResponse
    {
        public int DriverId { get; set; }
        public int DriverX { get; set; }
        public int DriverY { get; set; }
        public int RouteLength { get; set; }
        public List<RoutePoint> Route { get; set; } = new List<RoutePoint>();
    }
}
