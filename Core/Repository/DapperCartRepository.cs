using Condorcet.B2.AspnetCore.MVC.Application.Core.Domain;
using Condorcet.B2.AspnetCore.MVC.Application.Core.Infrastructure;
using Dapper;

namespace Condorcet.B2.AspnetCore.MVC.Application.Core.Repository;


public class DapperCartRepository : ICartRepository
{
    private readonly IDbConnectionProvider _dbConnectionProvider;

        public DapperCartRepository(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public async Task<List<CartItem>> GetAll(int userId)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();
            var sql = @"
                SELECT ci.user_id, ci.product_id, ci.quantity,
                       p.id, p.name, p.description, p.type, p.prix, p.isactive
                FROM cart_items ci
                JOIN products p ON p.id = ci.product_id
                WHERE ci.user_id = @userId AND p.isactive = true";
            var result = await connection.QueryAsync<CartItem, Product, CartItem>(
                sql,
                (ci, product) =>
                {
                    ci.Product = product;
                    return ci;
                },
                new { userId },
                splitOn: "id"
            );

            return result.ToList();
        }

        public async Task Add(int userId, int productId, int quantity)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();
            var sql = @"
                INSERT INTO cart_items (user_id, product_id, quantity)
                VALUES (@userId, @productId, @quantity)
                ON CONFLICT (user_id, product_id)
                DO UPDATE SET quantity = cart_items.quantity + @quantity";
            await connection.ExecuteAsync(sql, new { userId, productId, quantity });
        }

        public async Task Remove(int userId, int productId)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();
            var sql = "DELETE FROM cart_items WHERE user_id = @userId AND product_id = @productId";
            await connection.ExecuteAsync(sql, new { userId, productId });
        }

        public async Task Clear(int userId)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();
            var sql = "DELETE FROM cart_items WHERE user_id = @userId";
            await connection.ExecuteAsync(sql, new { userId });
        }

        public async Task UpdateQuantity(int userId, int productId, int quantity)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();
            var sql = "UPDATE cart_items SET quantity = @quantity WHERE user_id = @userId AND product_id = @productId";
            await connection.ExecuteAsync(sql, new { userId, productId, quantity });
        }
}