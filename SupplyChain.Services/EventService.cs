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
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EventService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CountManufacturerAsync()
        {
            return await _unitOfWork.EventRepository.CountAsync();
        }

        public async Task CreateManufacturerAsync(Event _event)
        {
            await _unitOfWork.EventRepository.AddAsync(_event);
            await _unitOfWork.CommitAsync();
            await _unitOfWork.CommitTransaction();
        }

        public async Task DeleteManufacturerAsync(Event _event)
        {
            await _unitOfWork.EventRepository.RemoveAsync(_event);
            await _unitOfWork.CommitAsync();
            await _unitOfWork.CommitTransaction();
        }

        public async Task<IEnumerable<Event>> GetAllManufacturerAsync()
        {
            return await _unitOfWork.EventRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Event>> GetAllPagedManufacturerAsync(int page, int pageSize)
        {
            var result = await _unitOfWork.EventRepository
              .GetPagedAsync(page, pageSize, null, orderBy: q => q.OrderBy(p => p.Id), true);
            return result;
        }

        public async Task<Event> GetManufacturerByIdAsync(int id)
        {
            return await _unitOfWork.EventRepository.GetByIdAsync(id);
        }

        public async Task RollbackTransaction()
        {
            await _unitOfWork.RollbackAsync();
        }

        public async Task UpdateManufacturerAsync(Event _event)
        {
            await _unitOfWork.EventRepository.UpdateAsync(_event);
            await _unitOfWork.CommitAsync();
            await _unitOfWork.CommitTransaction();
        }
    }
}
