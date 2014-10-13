using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required(ErrorMessage="You must set category name")]
        [MaxLength(255)]
        public string CategoryName { get; set; }
        
        public bool IsActive { get; set; }

        public DateTime LastSync { get; set; }
        
        public TimeSpan SyncPeriod { get; set; }

        public virtual List<DataProvider> DataProviders { get; set; }
    }
}
