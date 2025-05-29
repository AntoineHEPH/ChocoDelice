using Condorcet.B2.AspnetCore.MVC.Application.Core.Domain;
using Condorcet.B2.AspnetCore.MVC.Application.Core.Infrastructure;
using Dapper;
// Loris
namespace Condorcet.B2.AspnetCore.MVC.Application.Core.Repository
{
    public class DapperOrderRepository : IOrderRepository
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        public DapperOrderRepository(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public async Task AddAsync(Order order)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();
            var sql = """
                      INSERT INTO orders
                          (user_id, username, product_id, product_name, quantity, unit_price, order_date)
                      VALUES 
                          (@UserId, @Username, @ProductId, @ProductName, @Quantity, @UnitPrice, @OrderDate)
                      """;
            await connection.ExecuteAsync(sql, order);
        }

        public async Task<List<Order>> GetOrdersByUserAsync(int userId)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();
            var sql = """
                      SELECT id, user_id AS UserId, username AS Username, 
                         product_id AS ProductId, product_name AS ProductName,
                      quantity AS Quantity, unit_price AS UnitPrice, 
                      order_date AS OrderDate
                      FROM orders
                      WHERE user_id = @UserId
                      ORDER BY order_date DESC
                      """;

            var result = await connection.QueryAsync<Order>(sql, new { UserId = userId });
            return result.ToList();
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            using var connection = await _dbConnectionProvider.CreateConnection();
            var sql = """
                      SELECT id,
                             user_id AS UserId,
                             username AS Username,
                             product_id AS ProductId,
                             product_name AS ProductName,
                             quantity AS Quantity,
                             unit_price AS UnitPrice,
                             order_date AS OrderDate
                      FROM orders
                      ORDER BY OrderDate DESC
                      """;

            var result = await connection.QueryAsync<Order>(sql);
            return result.ToList();
        }
    }
}