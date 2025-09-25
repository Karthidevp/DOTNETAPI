using System;
using System.Collections.Generic;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductServices: IProductServices
    {
        private readonly IDbConnection _dbConnection;

        public ProductService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var sql = "SELECT * FROM Product WHERE IsDeleted = 0";
            return await _dbConnection.QueryAsync<Product>(sql);
        }

        public async Task<ProductResponse> CreateOrUpdateProductAsync(Product product)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ProductId", product.ProductId, DbType.Int32);
                parameters.Add("@ProductName", product.ProductName, DbType.String);
                parameters.Add("@Price", product.Price, DbType.Decimal);
                parameters.Add("@Quantity", product.Quantity, DbType.Int32);
                parameters.Add("@Description", product.Description, DbType.String);
                parameters.Add("@ProductImage", product.ProductImage, DbType.String);

                await _dbConnection.ExecuteAsync(
                    "USPInsertUpdateProduct",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return new ProductResponse
                {
                    Success = true,
                    Message = product.ProductId.HasValue
                        ? "Product updated successfully."
                        : "Product created successfully."
                };
            }
            catch (SqlException ex) when (ex.Number == 50000) // RAISERROR in SP
            {
                return new ProductResponse
                {
                    Success = false,
                    Message = ex.Message // e.g., "A product with the same name already exists."
                };
            }
            catch (Exception ex)
            {
                return new ProductResponse
                {
                    Success = false,
                    Message = $"Error: {ex.Message}"
                };
            }
        }
    }
}
