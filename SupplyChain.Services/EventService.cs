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

        public async Task<int> CountEventAsync()
        {
            return await _unitOfWork.EventRepository.CountAsync();
        }

        public async Task<int> CreateEventAsync(Event _event)
        {
            try
            {
                await _unitOfWork.EventRepository.AddAsync(_event);
                var result = await _unitOfWork.CommitAsync();
                await _unitOfWork.CommitTransaction();
                return result;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }

        }

        public async Task<int> DeleteEventAsync(Event _event)
        {
            try
            {
                await _unitOfWork.EventRepository.RemoveAsync(_event);
                var result = await _unitOfWork.CommitAsync();
                await _unitOfWork.CommitTransaction();
                return result;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }

        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _unitOfWork.EventRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Event>> GetAllPagedEventsAsync(int page, int pageSize)
        {
            var result = await _unitOfWork.EventRepository
              .GetPagedAsync(page, pageSize, null, orderBy: q => q.OrderBy(p => p.Id), true);
            return result;
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            return await _unitOfWork.EventRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Event>> GetIntervalEvent(DateTime start, DateTime end)
        {
            return await _unitOfWork.EventRepository
                .GetWhereAsync(e => e.StartIn >= start && e.EndIn <= end);
        }

        public async Task RollbackTransaction()
        {
            await _unitOfWork.RollbackAsync();
        }

        public async Task<int> UpdateEventAsync(Event _event)
        {
            try
            {
                await _unitOfWork.EventRepository.UpdateAsync(_event);
                var result = await _unitOfWork.CommitAsync();
                await _unitOfWork.CommitTransaction();
                return result;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }

        }
    }
}
