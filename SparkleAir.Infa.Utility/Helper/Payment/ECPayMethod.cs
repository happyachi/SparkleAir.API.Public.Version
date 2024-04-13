using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SparkleAir.Infa.Utility.Helper.Payment
{
    public class ECPayMethod
    {
        private readonly string _hashKey = "5294y06JbISpM5x9";
        private readonly string _HashIV = "v77hoKGq4kWxNNIS";


        public ECPayRequestDto CreateECPayResponseData(ECPayCreateResponseDto createDto)
        {
            // 要建立一個controller接收
            string returnURL = createDto.ReturnURL; //付款完成通知回傳網址
            int totalAmount = createDto.TotalAmount; //交易金額
            string tradeDesc = createDto.TradeDesc; //交易描述
            string itemName = createDto.ItemName; //商品名稱最多400字

            // 前端要倒回的網址，之後要存在appsettings.json
            string orderResultURL = createDto.OrderResultURL;

            // 這些是固定的
            string merchantID = "2000132";//ECPay提供的特店編號
            string merchantTradeNo = "Sparkle" + new Random().Next(0, 99999).ToString();//廠商的交易編號
            string merchantTradeDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");//廠商的交易時間
            string paymentType = "aio"; // 交易類型 請固定填入 aio
            int encryptType = 1; // CheckMacValue加密類型 請固定填入1
            string choosePayment = "Credit"; // 選擇預設付款方式


            string parameters = 
                $"ChoosePayment={choosePayment}&" +
                $"EncryptType={encryptType}&" +
                $"ItemName={itemName}&" +
                $"MerchantID={merchantID}&" +
                $"MerchantTradeDate={merchantTradeDate}&" +
                $"MerchantTradeNo={merchantTradeNo}&" +
                $"OrderResultURL={orderResultURL}&"+
                $"PaymentType={paymentType}&" +
                $"ReturnURL={returnURL}&" +
                $"TotalAmount={totalAmount}&" +
                $"TradeDesc={tradeDesc}";

            string checkMacValue = BuildCheckMacValue(parameters);

            ECPayRequestDto ecpayDto = new ECPayRequestDto()
            {
                MerchantID=merchantID,
                MerchantTradeNo=merchantTradeNo,
                MerchantTradeDate=merchantTradeDate,
                PaymentType=paymentType,
                TotalAmount=totalAmount,
                TradeDesc=tradeDesc,
                ItemName=itemName,
                ReturnURL=returnURL,
                ChoosePayment=choosePayment,
                EncryptType=encryptType,
                OrderResultURL=orderResultURL,
                CheckMacValue=checkMacValue,
            };

            return ecpayDto;
        }

        private string BuildCheckMacValue(string parameters, int encryptType = 1)
        {
            string szCheckMacValue = String.Empty;
            // 產生檢查碼。
            szCheckMacValue = String.Format("HashKey={0}&{1}&HashIV={2}", _hashKey, parameters, _HashIV);
            szCheckMacValue = HttpUtility.UrlEncode(szCheckMacValue).ToLower();
            if (encryptType == 1)
            {
                //szCheckMacValue = SHA256Encoder.Encrypt(szCheckMacValue);
                byte[] bytes = Encoding.UTF8.GetBytes(szCheckMacValue);
                byte[] hash = SHA256.Create().ComputeHash(bytes);
                //byte[] hash = SHA256Managed.Create().ComputeHash(bytes);


                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("X2"));
                }
                szCheckMacValue =  builder.ToString();
            }
            else
            {
                //szCheckMacValue = MD5Encoder.Encrypt(szCheckMacValue);
            }

            return szCheckMacValue.ToUpper();
        }


    }
}
