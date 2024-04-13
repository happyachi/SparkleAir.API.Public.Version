using SparkleAir.Infa.Dto.AirFlights;
using SparkleAir.Infa.Dto.TaxFree;
using SparkleAir.Infa.EFModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Dto.Campaigns
{
    public class DiscountDto
    {
        public string RuleName { get; set; }
        
        public List<DiscountProductDto> Products { get; set; }
        public decimal Amount { get; set; }
        public int DiscountId { get; set; }
        public string DiscountName { get; set; }

    }
}
