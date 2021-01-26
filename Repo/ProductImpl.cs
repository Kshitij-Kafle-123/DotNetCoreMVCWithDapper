using Dapper;
using MVCCore.Data;
using MVCCore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCore.Repo
{
    public class ProductImpl : IProducts
    {
        public void DeleteProduct(int productId)
        {
            //Using SQLClient ("SqlConnection" Which i installed)
            using var connection = new SqlConnection(ConnectionStringFetch.ConnectionValue);
            //Using Dapper (DynamicParameters)
            var param = new DynamicParameters();
            param.Add("@ProductId", productId);
            var result = connection.Execute("Usp_Delete", param, null, 0, CommandType.StoredProcedure);
            //throw new NotImplementedException();
        }

        public Product GetProductByProductId(int productId)
        {
            using var connection = new SqlConnection(ConnectionStringFetch.ConnectionValue);
            var param = new DynamicParameters();
            param.Add("@ProductId", productId);

            //---------------------------------------------
            return connection.Query<Product>("Usp_GetProductById", param, null, true, 0,
                CommandType.StoredProcedure).FirstOrDefault();
            // throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProducts()
        {
            using var connection = new SqlConnection(ConnectionStringFetch.ConnectionValue);

            //---------------------------------------------
            return connection.Query<Product>("Usp_GetAllProducts", null, null, true, 0,
                CommandType.StoredProcedure).ToList();
            //throw new NotImplementedException();
        }

        public void InsertProduct(ProductViewModel product)
        {
            try
            {
                using var connection = new SqlConnection(ConnectionStringFetch.ConnectionValue);
                connection.Open();
                var trans = connection.BeginTransaction();
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@Name", product.Name);
                    param.Add("@Quantity", product.Quantity);
                    param.Add("@Color", product.Color);
                    param.Add("@Price", product.Price);
                    param.Add("@ProductCode", product.ProductCode);
                    var result = connection.Execute("Usp_Insert", param, trans, 0, CommandType.StoredProcedure);

                
                        trans.Commit();
                    
                }
                catch (Exception)
                {
                    trans.Rollback();
                }
            }
            catch (Exception)
            {
                throw;
            }


            //throw new NotImplementedException();
        }

        public void UpdateProduct(Product product)
        {
            using var connection = new SqlConnection(ConnectionStringFetch.ConnectionValue);
            connection.Open();
            var trans = connection.BeginTransaction();
            try
            {
                var param = new DynamicParameters();
                param.Add("@Name", product.Name);
                param.Add("@Quantity", product.Quantity);
                param.Add("@Price", product.Price);
                param.Add("@Color", product.Color);
                param.Add("@ProductCode", product.ProductCode);
                param.Add("@ProductId", product.ProductId);

                var result = connection.Execute("Usp_Update", param, trans, 0, CommandType.StoredProcedure);
                if (result > 0)
                {
                    trans.Commit();
                }
            }
            catch
            {
                trans.Rollback();
            }
            //throw new NotImplementedException();
        }

        public bool ProductExist(int productId)
        {
            using var connection = new SqlConnection(ConnectionStringFetch.ConnectionValue);
            var param = new DynamicParameters();
            param.Add("@ProductId", productId);
            var result = connection.Query<bool>("Usp_CheckProduct", param, null, false, 0, 
                CommandType.StoredProcedure).FirstOrDefault();
            return result;

        }
    }
}
