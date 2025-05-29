namespace Condorcet.B2.AspnetCore.MVC.Application.Models
{
    public class DashboardViewModel
    {
        public int UserCount { get; set; }
        public int ProductCount { get; set; }
        public int OrderCount { get; set; }
        public IEnumerable<RecentUserDto> RecentUsers { get; set; }
        public IEnumerable<RecentProductDto> RecentProducts { get; set; }
    }

    public class RecentUserDto
    {

        public string Username { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class RecentProductDto
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}