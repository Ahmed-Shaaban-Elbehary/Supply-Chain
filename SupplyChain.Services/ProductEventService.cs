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
    public class ProductEventService : IProductEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductEventService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<int> CountProductEventAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Event> GetProductEventByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Event>> GetAllPagedProductEventsAsync(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Event>> GetAllProductEventsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<int>> AddProductEventAsync(Event _event, List<int> productIds)
        {
            List<int> result = new List<int>();

            foreach (var productId in productIds)
            {
                //get role
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
                if (product == null)
                    throw new ArgumentException("Invalid Role Id");

                ProductEvent pe = new ProductEvent()
                {
                    Event = _event,
                    Product = product,
                    EventId = _event.Id,
                    ProductId = product.Id
                };

                await _unitOfWork.ProductEventRepository.AddAsync(pe);
                result.Add(await _unitOfWork.CommitAsync());
            }
            return result;
        }

        public async Task<int> UpdateProductEventAsync(Event _event, List<int> productIds)
        {
            // Remove the existing product event roles for the event.
            var existingProductEvent = await _unitOfWork.ProductEventRepository.GetWhereAsync(ur => ur.EventId == _event.Id);
            await _unitOfWork.ProductEventRepository.RemoveRangeAsync(existingProductEvent);

            // Add the new product event for the event.
            var addedProductEventCount = await AddProductEventAsync(_event, productIds);
            return await _unitOfWork.CommitAsync(); ;
        }

        public async Task<IEnumerable<ProductEvent>> GetProductEventByProductIdAsync(int productId)
        {
            return await _unitOfWork.ProductEventRepository.GetWhereAsync(ev => ev.ProductId == productId);
        }

        public async Task<IEnumerable<ProductEvent>> GetProductEventByEventIdAsync(int eventId)
        {
            return await _unitOfWork.ProductEventRepository.GetWhereAsync(ev => ev.EventId == eventId);
        }

    }
}
