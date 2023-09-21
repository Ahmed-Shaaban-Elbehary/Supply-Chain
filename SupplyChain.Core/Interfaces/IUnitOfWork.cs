using SupplyChain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }
        IProductCategoryRepository ProductCategoryRepository { get; }
        IManufacturerRepository ManufacturerRepository { get; }
        ICartRepository CartRepository { get; }
        IUserRepository UserRepository { get; }
        IPermissionRepository PermissionRepository { get; }
        IRoleRepository RoleRepository { get; }
        IRolePermissionRepository RolePermissionRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }
        IEventStatusRepository EventStatusRepository { get; }
        IEventRepository EventRepository { get; }
        IProductEventRepository ProductEventRepository { get; }
        Task<int> CommitAsync();
        Task Detach<T>(T entity) where T : class;
        Task RollbackAsync();
        Task BeginTransaction();
        Task CommitTransaction();
    }
}
