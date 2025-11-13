namespace DriverFinder.Models
{
    public class RoutePoint
    {
        public int X { get; set; }
        public int Y { get; set; }

        public RoutePoint(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
