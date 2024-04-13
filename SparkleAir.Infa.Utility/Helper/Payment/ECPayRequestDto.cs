using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Utility.Helper.Payment
{
    public class ECPayRequestDto
    {
        /// <summary>
        /// 特店編號
        /// </summary>
        public string MerchantID { get; set; }

        /// <summary>
        /// 特店訂單編號
        /// </summary>
        public string MerchantTradeNo { get; set; }

        /// <summary>
        /// 特店交易時間 String(20) 格式為：yyyy/MM/dd HH:mm:ss
        /// </summary>
        public string MerchantTradeDate { get; set; }

        /// <summary>
        /// 交易類型 請固定填入 aio
        /// </summary>
        public string PaymentType { get; set; }

        /// <summary>
        /// 交易金額 
        /// </summary>
        public int TotalAmount { get; set; }

        /// <summary>
        /// 交易描述
        /// </summary>
        public string TradeDesc { get; set; }

        /// <summary>
        /// 商品名稱 如果商品名稱有多筆，需在金流選擇頁一行一行顯示商品名稱的話，商品名稱請以符號#分隔  400字內
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 付款完成通知回傳網址 ReturnURL為付款結果通知回傳網址，為特店server或主機的URL，用來接收綠界後端回傳的付款結果通知
        /// </summary>
        public string ReturnURL { get; set; }

        /// <summary>
        /// 選擇預設付款方式 Credit：信用卡及銀聯卡(需申請開通)
        /// </summary>
        public string ChoosePayment { get; set; }

        /// <summary>
        /// CheckMacValue加密類型  請固定填入1，使用SHA256加密。
        /// </summary>
        public int EncryptType { get; set; }

        /// <summary>
        /// Client端回傳付款結果網址
        /// </summary>
        public string OrderResultURL { get; set; }

        /// <summary>
        /// 檢查碼
        /// </summary>
        public string CheckMacValue { get; set; }

    }

    public class ECPayCreateResponseDto
    {
        public string ReturnURL { get; set; }
        public int TotalAmount { get; set; }
        public string TradeDesc { get; set; }
        public string ItemName { get; set; }
        public string OrderResultURL { get; set; }
    }

    public class ECPayReturnDto
    {
        public string MerchantID { get; set; } // 特店編號
        public string MerchantTradeNo { get; set; } // 特店交易編號
        public string StoreID { get; set; } // 特店旗下店舖代號
        public int RtnCode { get; set; } // 交易狀態,若回傳值為1時，為付款成功
        public string RtnMsg { get; set; } // 交易訊息
        public string TradeNo { get; set; } // 綠界的交易編號
        public int TradeAmt { get; set; } // 交易金額
        public string PaymentDate { get; set; } // 付款時間 格式為yyyy/MM/dd HH:mm:ss
        public string PaymentType { get; set; } // 特店選擇的付款方式
        public int PaymentTypeChargeFee { get; set; } // 交易手續費金額
        public string TradeDate { get; set; } // 訂單成立時間 格式為yyyy/MM/dd HH:mm:ss
        public int SimulatePaid { get; set; } // 是否為模擬付款  1：代表此交易為模擬付款
        public string CheckMacValue { get; set; } // 檢查碼

    }
}
