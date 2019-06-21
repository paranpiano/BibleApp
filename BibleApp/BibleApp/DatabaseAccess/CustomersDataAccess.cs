using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace BibleApp.DatabaseAccess
{
    public class CustomersDataAccess
    {
        private SQLiteConnection database;
        public static object collisionLock = new object();

        public ObservableCollection<Customer> Customers { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public CustomersDataAccess()
        {
            database =
                DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<Customer>();

            this.Customers =
                new ObservableCollection<Customer>(database.Table<Customer>());

            //if the table is empty, initialize the collction
            if (!database.Table<Customer>().Any())
            {
                AddNewCustomer();
            }
        }


        #region CRUD
        //Use Linq to query and filter data
        private void AddNewCustomer()
        {
            this.Customers.
                Add(new Customer
                {
                    CompanyName = "Continental",
                    PhysicalAddress = "Seoul PanGyo",
                    Country = "Korea"
                });

            SaveAllCustoemrs();
        }
        public IEnumerable<Customer> GetFilteredCustomers(string countryName)
        {
            lock (collisionLock)
            {
                var query = from cust in database.Table<Customer>()
                            where cust.Country == countryName
                            select cust;
                return query.AsEnumerable();
            }

        }

        //Use SQL queryies against data
        public IEnumerable<Customer> GetFilteredCustomers()
        {
            lock (collisionLock)
            {
                return database.Query<Customer>
                    ("SELECT * FROM Item WHERE Country = 'Korea' ").AsEnumerable();
            }

        }

        public Customer GetCustomers(int id)
        {
            lock (collisionLock)
            {
                return database.Table<Customer>().FirstOrDefault(c => c.Id == id);

            }

        }

        public int SaveCustomers(Customer customer)
        {
            lock (collisionLock)
            {
                if (customer.Id != 0)
                {
                    database.Update(customer);
                    return customer.Id;
                }
                else
                {
                    database.Insert(customer);
                    return customer.Id;
                }

            }
        }

        public void SaveAllCustoemrs()
        {
            lock (collisionLock)
            {
                foreach (var customer in this.Customers)
                {
                    if (customer.Id != 0)
                    {
                        database.Update(customer);
                    }
                    else
                    {
                        database.Insert(customer);
                    }
                }
            }
        }

        public int DeleteCustomer(Customer customer)
        {
            var id = customer.Id;
            if (id != 0)
            {
                lock (collisionLock)
                {
                    database.Delete<Customer>(id);
                }
            }

            this.Customers.Remove(customer);
            return id;
        }

        public void DeleteAllCustomers()
        {
            lock (collisionLock)
            {
                database.DropTable<Customer>();
                database.CreateTable<Customer>();
            }

            this.Customers = null;
            this.Customers = new ObservableCollection<Customer>(database.Table<Customer>());
        }
        #endregion




    }
}
