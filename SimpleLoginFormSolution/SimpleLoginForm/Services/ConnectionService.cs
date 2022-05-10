using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleLoginForm.Services
{
    public class ConnectionService
    {
        private SqlConnection connection;
        private string _connectionString = "DBConnection";
        private bool useTransaction = false;
        private SqlTransaction transaction;
        private IConfiguration config;

        public ConnectionService()
        {

        }
        public IConfiguration ConnectionString
        {
            get { return this.config; }
        }

        public ConnectionService(IConfiguration config)
        {
            this.config = config;
        }
        public SqlConnection GetConnection()
        {
            this.connection = new SqlConnection(config.GetConnectionString(_connectionString));
            return this.connection;
        }

        public void CloseConnection()
        {
            if (this.connection.State == ConnectionState.Open)
            {
                this.connection.Close();
            }
        }
        public void StartTransaction()
        {
            if (this.connection.State == ConnectionState.Closed)
            {
                this.connection.Open();
            }

            this.transaction = this.connection.BeginTransaction();
            this.useTransaction = true;
        }
        public void CommitTransaction()
        {
            if ((this.transaction != null) && (this.connection.State == ConnectionState.Open))
            {
                this.transaction.Commit();

                if (this.connection.State == ConnectionState.Open)
                {
                    this.connection.Close();
                }

                this.useTransaction = false;
            }
        }
        public void RollbackTransaction()
        {
            if ((this.transaction != null) && (this.connection.State == ConnectionState.Open))
            {
                this.transaction.Rollback();

                if (this.connection.State == ConnectionState.Open)
                {
                    this.connection.Close();
                }

                this.useTransaction = false;
            }
        }
        protected virtual object GetNull(object obj)
        {
            if (obj == null)
            {
                obj = DBNull.Value;
                return obj;

            }
            // Check that object is "string" and object's value is empty string
            if ((obj is string) && (obj.ToString() == string.Empty))
            {
                // Specify DBNull value
                obj = DBNull.Value;
                return obj;
            }

            // Check that object is "DateTime" and object's value is minimum datetime value
            if ((obj is DateTime) && (((DateTime)obj) == DateTime.MinValue))
            {
                // Specify DBNull value
                obj = DBNull.Value;
                return obj;
            }

            // Check that object is "int" and object's value is minimum integer value
            if ((obj is int) && (((int)obj) == int.MinValue))
            {
                // Specify DBNull value
                obj = DBNull.Value;
                return obj;
            }

            // Check that object is "float" and object's value is minimum float value
            if ((obj is float) && (((float)obj) == float.MinValue))
            {
                // Specify DBNull value
                obj = DBNull.Value;
                return obj;
            }

            // Check that object is "decimal" and object's value is minimum decimal value
            if ((obj is decimal) && (((decimal)obj) == decimal.MinValue))
            {
                // Specify DBNull value
                obj = DBNull.Value;
                return obj;
            }

            // Check that object is "double" and object's value is minimum double value
            if ((obj is double) && (((double)obj) == double.MinValue))
            {
                // Specify DBNull value
                obj = DBNull.Value;
            }

            return obj;
        }
        public T GetValue<T>(object obj)
        {
            T result = default(T);
            Type type = typeof(T);

            if (this.GetNull(obj) != DBNull.Value)
            {
                result = (T)obj;
            }

            return result;
        }
    }
}
