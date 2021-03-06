﻿using Senparc.Weixin.TenPay.V3;

namespace System.WeChat
{
    public static class TenPayV3Helper
    {
        public static string GenerateOutTradeNo()
        {
            return string.Format("{0}{1}{2}", WeChatInfo.MchID, DateTime.Now.ToString("yyyyMMddHHmmssff"), TenPayV3Util.BuildRandomStr(6)).PadRight(32, '0').Substring(0, 32);
        }

        public static Tuple<string, TenPayV3UnifiedorderRequestData, UnifiedorderResult> Pay(Uri uri, string openid, string outTradeNo, int totalFee, string body, string spbillCreateIp)
        {
            var nonceStr = TenPayV3Util.GetNoncestr();
            var url = uri.Scheme == "http" && uri.Port == 80
                ? uri.Scheme + @"://" + uri.Host + WeChatInfo.TenPayV3NotifyUrl
                : uri.Scheme == "https" && uri.Port == 443
                    ? uri.Scheme + @"://" + uri.Host + WeChatInfo.TenPayV3NotifyUrl
                    : uri.Scheme + @"://" + uri.Host + @":" + uri.Port + WeChatInfo.TenPayV3NotifyUrl;
            var dataInfo = new TenPayV3UnifiedorderRequestData(WeChatInfo.AppID, WeChatInfo.MchID, body, outTradeNo, totalFee, spbillCreateIp, url, Senparc.Weixin.TenPay.TenPayV3Type.JSAPI, openid, WeChatInfo.Key, nonceStr, "WEB", DateTime.Now, DateTime.Now.AddMinutes(5));
            var uresult = TenPayV3.Unifiedorder(dataInfo);
            switch (uresult.return_code)
            {
                case "SUCCESS":
                    switch (uresult.result_code)
                    {
                        case "SUCCESS":
                            return new Tuple<string, TenPayV3UnifiedorderRequestData, UnifiedorderResult>(uresult.prepay_id, dataInfo, uresult);

                        case "FAIL":
                            return new Tuple<string, TenPayV3UnifiedorderRequestData, UnifiedorderResult>(null, dataInfo, uresult);

                        default:
                            return new Tuple<string, TenPayV3UnifiedorderRequestData, UnifiedorderResult>(null, dataInfo, uresult);
                    }

                case "FAIL":
                    return new Tuple<string, TenPayV3UnifiedorderRequestData, UnifiedorderResult>(null, dataInfo, uresult);

                default:
                    return new Tuple<string, TenPayV3UnifiedorderRequestData, UnifiedorderResult>(null, dataInfo, uresult);
            }
        }
    }
}