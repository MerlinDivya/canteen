using canteen.Data.DataAccess;
using canteen.Data.Models.Domain;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace canteen.Data.Repository
{
    public class issueRepository : IissueRepository
    {
        private readonly ISqlDataAccess _db;

        public issueRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<bool> AddAsync(Issue issue)
        {
            try
            {
                await _db.SaveData("sp_create_Issue", new
                {
                    issue.item_number,
                    issue.description,
                    issue.rect_date,
                    issue.rate,
                    issue.quantity,
                    issue.issd_date,
                    issue.category,
                    issue.menuitem
                });
                return true;
            }
            catch (Exception ex)
            {
                // Handle exceptions or return false to indicate failure
                return false;
            }
        }

        public async Task<bool> UpdateAsync(Issue issue)
        {
            try
            {
                await _db.SaveData("sp_update_Issue", issue);
                return true;
            }
            catch (Exception ex)
            {
                // Handle exceptions or return false to indicate failure
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _db.SaveData("sp_delete_Issue", new { Id = id });
                return true;
            }
            catch (Exception ex)
            {
                // Handle exceptions or return false to indicate failure
                return false;
            }
        }

        public async Task<Issue?> GetByIdAsync(int id)
        {
            IEnumerable<Issue> result = await _db.GetData<Issue, dynamic>("sp_get_Issue", new { Id = id });
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<Issue>> GetAllAsync()
        {
            string query = "sp_get_Iss";
            return await _db.GetData<Issue, dynamic>(query, new { });
        }

        public async Task<IEnumerable<string>> GetDescriptionsAsync()
        {
            try
            {
                // Implement a method to retrieve unique descriptions from the "Issues" table
                string query = "SELECT DISTINCT description FROM Issue";
                var descriptions = await _db.LoadData<string, dynamic>(query, new { });

                return descriptions.ToList();
            }
            catch (Exception ex)
            {
                // Handle exceptions or return an empty list to indicate failure
                return new List<string>();
            }
        }

        public async Task<IEnumerable<string>> GetReceiptsDescriptionsAsync()
        {
            try
            {
                // Implement a method to retrieve unique descriptions from the "Receipts" table
                string query = "SELECT DISTINCT description FROM Receipts";
                var descriptions = await _db.LoadData<string, dynamic>(query, new { });

                return descriptions;
            }
            catch (Exception ex)
            {
                // Handle exceptions or return an empty list to indicate failure
                return new List<string>();
            }
        }
        public async Task<List<Issue>> GetReceiptsDataByDescription(string description)
        {
            try
            {
                // Implement code to fetch receipt data based on the description
                // Example: SELECT * FROM Receipts WHERE description = @description
                string query = "SELECT item_number, description, rect_date, rate, quantity FROM Receipts WHERE description = @Description";
                var parameters = new { Description = description };
                var data = await _db.LoadData<Issue, dynamic>(query, parameters);

                return data.ToList();
            }
            catch (Exception ex)
            {
                // Handle exceptions or return an empty list to indicate failure
                return new List<Issue>();
            }
        }




        public async Task<Issue> GetReceiptParametersByDescription(string description)
        {
            try
            {
                string query = "SELECT item_number, rect_date, rate, quantity FROM Receipts WHERE description = @Description";
                var parameters = new { Description = description };
                var data = await _db.LoadData<Issue, dynamic>(query, parameters);
                return data.FirstOrDefault(); // Assuming it returns a single record
            }
            catch (Exception ex)
            {
                // Handle exceptions or return null to indicate failure
                return null;
            }
        }
        public async Task<List<string>> GetItemsForDropdownAsync(string category)
        {
            string query = "SELECT DISTINCT item FROM menu_items WHERE category = @Category";
            var parameters = new { Category = category };

            var items = await _db.LoadData<string, dynamic>(query, parameters);
            return items.ToList();
        }





    }

}
