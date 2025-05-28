using Condorcet.B2.AspnetCore.MVC.Application.Core.Domain;
using Condorcet.B2.AspnetCore.MVC.Application.Core.Infrastructure;
using Dapper;

namespace Condorcet.B2.AspnetCore.MVC.Application.Core.Repository
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        public DapperProductRepository(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public async Task<List<Product>> GetAll()
        {
            using var connection = await _dbConnectionProvider.CreateConnection();
            var result =
                await connection.QueryAsync<Product>(
                    "SELECT id, name, description, type, prix, isactive FROM products WHERE isactive = true ORDER BY id");
            return result.ToList();
        }

        public async Task<Product?> GetById(int id)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Product>(
                "SELECT id, name, description, type, prix FROM products WHERE id = @id",
                new { id });
        }

        public async Task<int> Insert(Product product)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();
            const string sql = """
                               INSERT INTO products 
                               (
                                   name, 
                                   description, 
                                   type, 
                                   prix
                               )
                               VALUES 
                               (
                                   @Name, 
                                   @Description, 
                                   @Type, 
                                   @Prix
                               )
                               RETURNING id
                               """;
            var id = await connection.ExecuteScalarAsync<int>(sql, product);

            return id;
        }

        public async Task<int> Update(int id, Product product)
        {
            product.Id = id;
            using var connection = await _dbConnectionProvider.CreateConnection();
            return await connection.ExecuteAsync("""
                                                 UPDATE products 
                                                 SET 
                                                     name = @name, 
                                                     description = @description,
                                                     type = @type,
                                                     prix = @prix
                                                 WHERE id = @id;
                                                 """, product);
        }

        public async Task<bool> Exists(string? name)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();
            return await connection.ExecuteScalarAsync<bool>("""
                                                             SELECT EXISTS (SELECT 1 FROM products WHERE name = @name)
                                                             """, new {name});
        }

        public async Task<int> DisableAsync(int id)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();
            return await connection.ExecuteAsync("""
                                                 UPDATE Products
                                                 SET IsActive = FALSE
                                                 WHERE id = @id;
                                                 """, new { Id = id });
        }
    }
}