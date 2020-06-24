using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CasbinACLvsRBAC.Entity
{
    public class CasbinRule //<TKey> : ICasbinRule where TKey : IEquatable<TKey>
    {
      //  public virtual TKey Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string PType { get; set; }
        public string V0 { get; set; }
        public string V1 { get; set; }
        public string V2 { get; set; }
        public string V3 { get; set; }
        public string V4 { get; set; }
        public string V5 { get; set; }
    }

    public interface ICasbinRule
    {
        string PType { get; set; }
        string V0 { get; set; }
        string V1 { get; set; }
        string V2 { get; set; }
        string V3 { get; set; }
        string V4 { get; set; }
        string V5 { get; set; }
    }
}
