﻿using SupplyChain.Core.Interfaces;
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
        public async Task<int> CountEventStatusAsync()
        {
            return await _unitOfWork.EventStatusRepository.CountAsync();
        }

        public async Task CreateEventStatusAsync(EventStatus eventStatus)
        {
            var isEventAlreadyExist = await _unitOfWork.EventStatusRepository
                .GetWhereAsync(e => e.UserId == eventStatus.UserId && e.EventId == eventStatus.EventId);

            if (!isEventAlreadyExist.Any())
            {
                await _unitOfWork.EventStatusRepository.AddAsync(eventStatus);
                await _unitOfWork.CommitAsync();
                await _unitOfWork.CommitTransaction();
            }
        }

        public async Task DeleteEventStatusAsync(EventStatus eventStatus)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(eventStatus.UserId);
            var _event = await _unitOfWork.EventRepository.GetByIdAsync(eventStatus.EventId);
            eventStatus.User = user;
            eventStatus.Event = _event;
            await _unitOfWork.EventStatusRepository.RemoveAsync(eventStatus);
            await _unitOfWork.CommitAsync();
            await _unitOfWork.CommitTransaction();
        }

        public async Task<IEnumerable<EventStatus>> GetAllEventStatusAsync()
        {
            return await _unitOfWork.EventStatusRepository.GetAllAsync();
        }

        public async Task<IEnumerable<EventStatus>> GetAllPagedEventStatusAsync(int page, int pageSize)
        {
            var result = await _unitOfWork.EventStatusRepository
               .GetPagedAsync(page, pageSize, null, orderBy: q => q.OrderBy(p => p.Id), true);
            return result;
        }

        public IEnumerable<EventStatus>? GetEventStatusByEventIdAndUserIdAsync(int eventId, int userId)
        {
            var result = _unitOfWork.EventStatusRepository
                .GetWhereAsync(e => e.EventId == eventId && e.UserId == userId);
            return result.Result.Count() <= 0 ? null : result.Result;
        }

        public async Task<EventStatus> GetEventStatusByIdAsync(int id)
        {
            return await _unitOfWork.EventStatusRepository.GetByIdAsync(id);
        }

        public async Task RollbackTransaction()
        {
            await _unitOfWork.RollbackAsync();
        }

        public async Task UpdateEventStatusAsync(EventStatus EventStatus)
        {
            await _unitOfWork.EventStatusRepository.UpdateAsync(EventStatus);
            await _unitOfWork.CommitAsync();
            await _unitOfWork.CommitTransaction();
        }
    }
}
