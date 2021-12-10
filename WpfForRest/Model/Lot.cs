using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfForRest.Model
{
    public class Lot : ILot
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public string ImagePath { get; set; }
    }
}
