using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using canteen.Data.Models.Domain;

namespace canteen.Data.Repository
{
    public interface IissueRepository
    {
        Task<bool> AddAsync(Issue issue);
        Task<bool> UpdateAsync(Issue issue);
        Task<bool> DeleteAsync(int id);
        Task<Issue> GetByIdAsync(int id);
        Task<IEnumerable<Issue>> GetAllAsync();

        Task<IEnumerable<string>> GetDescriptionsAsync();
        Task<IEnumerable<string>> GetReceiptsDescriptionsAsync();
        Task<List<Issue>> GetReceiptsDataByDescription(string description);
        Task<Issue> GetReceiptParametersByDescription(string description);

        Task<List<string>> GetItemsForDropdownAsync(string category);
    }
}
