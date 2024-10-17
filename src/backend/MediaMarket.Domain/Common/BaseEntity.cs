using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaMarket.Domain.Common
{
    public abstract class BaseEntity<T>
    {
        [Key]
        public required T Id { get; set; }
    }
}
