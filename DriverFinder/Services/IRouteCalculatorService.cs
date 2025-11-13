using DriverFinder.Models;

namespace DriverFinder.Services
{
    public interface IRouteCalculatorService
    {
        List<RoutePoint> CalculateRoute(int startX, int startY, int endX, int endY);
        int CalculateRouteLength(List<RoutePoint> route);
    }

    public class RouteCalculatorService : IRouteCalculatorService
    {
        public List<RoutePoint> CalculateRoute(int startX, int startY, int endX, int endY)
        {
            var route = new List<RoutePoint>();
            int currentX = startX;
            int currentY = startY;

            // Добавляем начальную точку
            route.Add(new RoutePoint(currentX, currentY));

            while (currentX != endX || currentY != endY)
            {
                if (currentX < endX)
                {
                    currentX++;
                }
                else if (currentX > endX)
                {
                    currentX--;
                }

                if (currentY < endY)
                {
                    currentY++;
                }
                else if (currentY > endY)
                {
                    currentY--;
                }

                route.Add(new RoutePoint(currentX, currentY));
            }

            return route;
        }

        public int CalculateRouteLength(List<RoutePoint> route)
        {
            // Длину маршрута
            return route.Count > 0 ? route.Count - 1 : 0;
        }
    }
}
