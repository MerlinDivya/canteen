using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using canteen.Data.Models.Domain;

namespace canteen.Data.Repository
{
    public interface IreceiptsRepository
    {
        Task<bool> AddAsync(Receipts receipts);
        Task<bool> UpdateAsync(Receipts receipts);
        Task<bool> DeleteAsync(int id);
        Task<Receipts?> GetByIdAsync(int id);
        Task<IEnumerable<Receipts>> GetAllAsync();
        Task<List<string>> GetDescriptionsAsync();
        Task<string?> GetUnitByDescriptionAsync(string description);
        Task<string?> GetItemNumberByDescriptionAsync(string description);
    }
}
