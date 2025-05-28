using System.Data;

namespace Condorcet.B2.AspnetCore.MVC.Application.Core.Infrastructure
{
    public interface IDbConnectionProvider
    {
        public Task<IDbConnection> CreateConnection();
    }
}
