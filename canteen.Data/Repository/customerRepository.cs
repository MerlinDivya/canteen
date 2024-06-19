using canteen.Data.DataAccess;
using canteen.Data.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace canteen.Data.Repository
{
    public class customerRepository : IcustomerRepository
    {
        private readonly ISqlDataAccess _db;

        public customerRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<bool> AddAsync(Customer customer)
        {
            try
            {
                await _db.SaveData("sp_create_customer", new { customer.item_number, customer.description, customer.unit });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(Customer customer)
        {
            try
            {
                await _db.SaveData("sp_update_customer", customer);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _db.SaveData("sp_delete_customer", new { Id = id });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            IEnumerable<Customer> result = await _db.GetData<Customer, dynamic>("sp_get_customer", new { Id = id });
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            string query = "sp_get_cust";
            return await _db.GetData<Customer, dynamic>(query, new { });
        }

        public async Task<List<string>> GetDescriptionsAsync()
        {
            try
            {
                // Implement a method to retrieve unique descriptions from the "Customer" table
                string query = "SELECT DISTINCT description FROM Customer";
                var descriptions = await _db.LoadData<string, dynamic>(query, new { });

                return descriptions.ToList();
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }

        public async Task<string?> GetUnitByDescriptionAsync(string description)
        {
            try
            {
                // Implement a method to retrieve the unit by description from the "Customer" table
                string query = "SELECT TOP 1 unit FROM Customer WHERE description = @Description";
                var parameters = new { Description = description };
                var unit = await _db.LoadData<string, dynamic>(query, parameters);

                return unit.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null; // Handle exceptions or return a default value as needed
            }
        }

        public async Task<string?> GetItemNumberByDescriptionAsync(string description)
        {
            try
            {
                // Implement the logic to retrieve item_number by description from the "Customer" table
                string query = "SELECT TOP 1 item_number FROM Customer WHERE description = @Description";
                var parameters = new { Description = description };
                var itemNumber = await _db.LoadData<string, dynamic>(query, parameters);

                return itemNumber.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null; // Handle exceptions or return a default value as needed
            }
        }
    }
}
