using canteen.Data.Models.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace canteen.Data.Repository
{
    public interface IcustomerRepository
    {
        Task<bool> AddAsync(Customer customer);
        Task<bool> UpdateAsync(Customer customer);
        Task<bool> DeleteAsync(int id);
        Task<Customer?> GetByIdAsync(int id);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<List<string>> GetDescriptionsAsync();
        Task<string?> GetUnitByDescriptionAsync(string description);
        Task<string?> GetItemNumberByDescriptionAsync(string description); // New method to retrieve item_number by description
    }
}
