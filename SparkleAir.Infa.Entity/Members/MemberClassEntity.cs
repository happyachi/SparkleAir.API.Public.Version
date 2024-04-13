using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Entity.Members
{
    public class MemberClassEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ClassOrder { get; set; }

        public int MileageStart { get; set; }

        public int MileageEnd { get; set; }
    }
}
