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
    public class ManufactureService : IManufactureService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ManufactureService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CountManufacturerAsync()
        {
            return await _unitOfWork.ManufactureRepository.CountAsync();
        }

        public async Task CreateManufacturerAsync(Manufacturer manufacturer)
        {
            await _unitOfWork.ManufactureRepository.AddAsync(manufacturer);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteManufacturerAsync(Manufacturer manufacturer)
        {
            await _unitOfWork.ManufactureRepository.RemoveAsync(manufacturer);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Manufacturer>> GetAllManufacturerAsync()
        {
            return await _unitOfWork.ManufactureRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Manufacturer>> GetAllPagedManufacturerAsync(int page, int pageSize)
        {
            var result = await _unitOfWork.ManufactureRepository
               .GetPagedAsync(page, pageSize, null, orderBy: q => q.OrderBy(p => p.Id), true);
            return result;
        }

        public async Task<Manufacturer> GetManufacturerByIdAsync(int id)
        {
            return await _unitOfWork.ManufactureRepository.GetByIdAsync(id);
        }

        public async Task UpdateManufacturerAsync(Manufacturer manufacturer)
        {
            await _unitOfWork.ManufactureRepository.UpdateAsync(manufacturer);
            await _unitOfWork.CommitAsync();
        }
    }
}
