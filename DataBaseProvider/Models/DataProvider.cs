using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider.Models
{   
    [Table("DataProvider")]
    public class DataProvider
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required(ErrorMessage="You must set data provider name")]
        [MaxLength(255)]
        public string ProviderName { get; set; }
        
        [Required(ErrorMessage="You must set proper url to yout data provider")]
        [MaxLength(255)]
        public string ProviderURI { get; set; }
        public bool IsActive { get; set; }

        public DataProviderType ProviderType { get; set; }
        public Category ProviderCategory { get; set; }

        public DateTime LastSync { get; set; }
        public TimeSpan SyncPeriod { get; set; }
    }
}
