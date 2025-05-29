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
            var sql = """
                      SELECT id, name, description, type, prix, isactive 
                      FROM products
                      ORDER BY id
                      """;
            var result = await connection.QueryAsync<Product>(sql);
            return result.ToList();
        }

        public async Task<Product?> GetById(int id)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();
            var sql = """
                      SELECT id, name, description, type, prix 
                      FROM products 
                      WHERE id = @id
                      """;
            return await connection.QueryFirstOrDefaultAsync<Product>(sql, new { id });
        }

        public async Task<int> Insert(Product product)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();
            var sql = """
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
            return await connection.ExecuteScalarAsync<int>(sql, product);
        }

        public async Task<int> Update(int id, Product product)
        {
            product.Id = id;
            using var connection = await _dbConnectionProvider.CreateConnection();
            var sql = """
                      UPDATE products 
                      SET 
                          name = @name, 
                          description = @description,
                          type = @type,
                          prix = @prix
                      WHERE id = @id
                      """;
            return await connection.ExecuteAsync(sql, product);
        }

        public async Task<int> DisableAsync(int id)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();
            var sql = """
                      UPDATE products
                      SET isactive = FALSE
                      WHERE id = @id
                      """;
            return await connection.ExecuteAsync(sql, new { id });
        }
    }
}