using canteen.Data.DataAccess;
using canteen.Data.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace canteen.Data.Repository
{
    public class receiptsRepository : IreceiptsRepository // Corrected the class name to follow C# naming conventions
    {
        private readonly ISqlDataAccess _db;

        public receiptsRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<bool> AddAsync(Receipts receipt)
        {
            try
            {
                await _db.SaveData("sp_create_Receipts", new
                {
                    receipt.item_number,
                    receipt.description,
                    receipt.rect_date,
                    receipt.rate,
                    receipt.quantity,
                    receipt.unit
                });
                return true;
            }
            catch (Exception ex)
            {
                // Handle exceptions or return false to indicate failure
                return false;
            }
        }

        public async Task<bool> UpdateAsync(Receipts receipts)
        {
            try
            {
                await _db.SaveData("sp_update_Receipts", receipts);
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
                await _db.SaveData("sp_delete_Receipts", new { Id = id });
                return true;
            }
            catch (Exception ex)
            {
                // Handle exceptions or return false to indicate failure
                return false;
            }
        }

        public async Task<Receipts?> GetByIdAsync(int id)
        {
            IEnumerable<Receipts> result = await _db.GetData<Receipts, dynamic>("sp_get_Receipts", new { Id = id });
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<Receipts>> GetAllAsync()
        {
            string query = "sp_get_rept";
            return await _db.GetData<Receipts, dynamic>(query, new { });
        }

        public async Task<List<string>> GetDescriptionsAsync()
        {
            try
            {
                // Implement a method to retrieve unique descriptions from the "Receipts" table
                string query = "SELECT DISTINCT description FROM Receipts";
                var descriptions = await _db.LoadData<string, dynamic>(query, new { });

                return descriptions.ToList();
            }
            catch (Exception ex)
            {
                // Handle exceptions or return an empty list to indicate failure
                return new List<string>();
            }
        }

        public async Task<string?> GetUnitByDescriptionAsync(string description)
        {
            try
            {
                // Implement a method to retrieve the unit by description from the "Customer" table
                string query = "SELECT TOP 1 unit FROM Receipts WHERE description = @Description";
                var parameters = new { Description = description };
                var unit = await _db.LoadData<string, dynamic>(query, parameters);

                return unit.FirstOrDefault();
            }
            catch (Exception ex)
            {
                // Handle exceptions or return null to indicate failure
                return null;
            }
        }

        public async Task<string?> GetItemNumberByDescriptionAsync(string description)
        {
            try
            {
                // Implement the logic to retrieve item_number by description from the "Customer" table
                string query = "SELECT TOP 1 item_number FROM Receipts WHERE description = @Description";
                var parameters = new { Description = description };
                var itemNumber = await _db.LoadData<string, dynamic>(query, parameters);

                return itemNumber.FirstOrDefault();
            }
            catch (Exception ex)
            {
                // Handle exceptions or return null to indicate failure
                return null;
            }
        }
    }
}
