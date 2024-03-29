using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MayetteRice.Models.ViewModels
{
    public class OrderVM
    {
        public OrderHeader OrderHeader { get; set; }
        public IEnumerable<OrderDetail> OrderDetail { get; set; }
    }
}
