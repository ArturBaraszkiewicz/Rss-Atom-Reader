using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProvider.Models
{   
    [Table("Content")]
    public class ProviderContent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [MaxLength(255)]
        public string Title { get; set; }
        
        [MaxLength(255)]
        public string Content{ get; set; }
        
        [MaxLength(255)]
        public string Author { get; set; }
        
        public DateTime PublicationDate { get; set; }
        
        public DataProvider Provider { get; set; }
    }
}
