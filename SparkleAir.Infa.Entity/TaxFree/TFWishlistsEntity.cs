using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Entity.TaxFree
{
    public class TFWishlistsEntity
    {
        public int Id { get; set; }

        public int MemberId { get; set; }

        public string MemberChineseLastName { get; set; }


        public string MemberChineseFirstName { get; set; }


        public string MemberEnglishLastName { get; set; }


        public string MemberEnglishFirstName { get; set; }


        public string MemberPhone { get; set; }


        public string MemberEmail { get; set; }


        public string MemberPassportNumber { get; set; }

        public int TFItemsId { get; set; }

        public string TFItemsName { get; set; }

        public string TFItemsSerialNumber { get; set; }

        public string TFItemsImage { get; set; }

        public int TFItemsQuantity { get; set; }

        public int TFItemsUnitPrice { get; set; }
    }
}
