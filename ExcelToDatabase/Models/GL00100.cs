using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Numerics;

namespace ExcelToDatabase.Models
{
    public class GL00100
    {
        [Key]
        public int ACTINDX { get; set; } // int in C# corresponds to INT in SQL
        public string ACTDESCR { get; set; }
        public short ACCATNUM { get; set; } // short in C# corresponds to SMALLINT in SQL
        //This is the ID for the parent table[
        [NotMapped]
        public int? AccountId { get; set; }
    }
}
