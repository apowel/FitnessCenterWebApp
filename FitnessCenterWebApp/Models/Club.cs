using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessCenterWebApp.Models
{
    public class Club
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int Id { get; set; }
        public Membership Membership { get; set; }
    }
}
