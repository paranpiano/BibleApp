using SQLite;
using System.ComponentModel;

namespace BibleApp.DatabaseAccess
{
    [Table("Customers")]
    public class Customer
    {
        private int _id;
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get { return _id; }
            set { this._id = value; }
        }

        private string _companyName;
        [NotNull]
        public string CompanyName
        {
            get { return _companyName; }
            set { this._companyName = value; }
        }

        private string _physicalAddress;
        [MaxLength(50)]
        public string PhysicalAddress
        {
            get { return _physicalAddress; }
            set { this._physicalAddress = value; }
        }

        private string _country;
        public string Country
        {
            get { return _country; }
            set { this._country = value; }
        }
    }
}
