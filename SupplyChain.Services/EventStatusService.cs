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
    public class EventStatusService : IEventStatusService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EventStatusService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> CountNotificationAsync()
        {
            return await _unitOfWork.NotifcationRepository.CountAsync();
        }

        public async Task CreateNotificationAsync(EventStatus notification)
        {
            await _unitOfWork.NotifcationRepository.AddAsync(notification);
            await _unitOfWork.CommitAsync();
            await _unitOfWork.CommitTransaction();
        }

        public async Task DeleteNotificationAsync(EventStatus notification)
        {
            await _unitOfWork.NotifcationRepository.RemoveAsync(notification);
            await _unitOfWork.CommitAsync();
            await _unitOfWork.CommitTransaction();
        }

        public async Task<IEnumerable<EventStatus>> GetAllNotificationAsync()
        {
            return await _unitOfWork.NotifcationRepository.GetAllAsync();
        }

        public async Task<IEnumerable<EventStatus>> GetAllPagedNotificationAsync(int page, int pageSize)
        {
            var result = await _unitOfWork.NotifcationRepository
               .GetPagedAsync(page, pageSize, null, orderBy: q => q.OrderBy(p => p.Id), true);
            return result;
        }

        public async Task<EventStatus> GetNotificationByIdAsync(int id)
        {
            return await _unitOfWork.NotifcationRepository.GetByIdAsync(id);
        }

        public async Task RollbackTransaction()
        {
            await _unitOfWork.RollbackAsync();
        }

        public async Task UpdateNotificationAsync(EventStatus notification)
        {
            await _unitOfWork.NotifcationRepository.UpdateAsync(notification);
            await _unitOfWork.CommitAsync();
            await _unitOfWork.CommitTransaction();
        }
    }
}
