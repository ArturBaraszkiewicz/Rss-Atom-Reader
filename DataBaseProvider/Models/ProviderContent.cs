using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider.Models
{
    class ProviderContent
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content{ get; set; }
        public string Author { get; set; }
        public DateTime PublicationDate { get; set; }
        public DataProvider Provider { get; set; }
    }
}
