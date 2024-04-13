using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Dto.Airtype_Owns
{
    public class AirOwnSearchDto
    {
        public string keyword { get; set; }
        public int? airTypeId { get; set; } = 0; //不根據分類編號進行搜尋

        public string sortBy { get; set; }
        public string sortType { get; set; } = "asc";

        public int? page { get; set; } = 1; //第一頁
        public int? pageSize { get; set; } = 5; //一頁5筆
    }
}
