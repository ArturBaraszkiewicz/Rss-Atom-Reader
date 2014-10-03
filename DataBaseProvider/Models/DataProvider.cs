using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider.Models
{
    public class DataProvider
    {
        public int Id { get; set; }
        public string ProviderName { get; set; }
        public string ProviderURI { get; set; }
        public bool IsActive { get; set; }
        public DataProviderType ProviderType { get; set; }
        public Category ProviderCategory { get; set; }
    }
}
