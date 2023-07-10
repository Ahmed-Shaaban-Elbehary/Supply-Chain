using SupplyChain.Core.Interfaces;
using SupplyChain.Core.Models;
using SupplyChain.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public NotificationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> CountManufacturerAsync()
        {
            return await _unitOfWork.NotifcationRepository.CountAsync();
        }

        public async Task CreateManufacturerAsync(Notification notification)
        {
            await _unitOfWork.NotifcationRepository.AddAsync(notification);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteManufacturerAsync(Notification notification)
        {
            await _unitOfWork.NotifcationRepository.RemoveAsync(notification);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Notification>> GetAllManufacturerAsync()
        {
            return await _unitOfWork.NotifcationRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Notification>> GetAllPagedManufacturerAsync(int page, int pageSize)
        {
            var result = await _unitOfWork.NotifcationRepository
               .GetPagedAsync(page, pageSize, null, orderBy: q => q.OrderBy(p => p.Id), true);
            return result;
        }

        public async Task<Notification> GetManufacturerByIdAsync(int id)
        {
            return await _unitOfWork.NotifcationRepository.GetByIdAsync(id);
        }

        public async Task UpdateManufacturerAsync(Notification notification)
        {
            await _unitOfWork.NotifcationRepository.UpdateAsync(notification);
            await _unitOfWork.CommitAsync();
        }
    }
}
