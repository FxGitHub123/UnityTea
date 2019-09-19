
using System.Collections.Generic;

public class TTAnalytics
{
    /// <summary>
    /// Starts the with app identifier.
    /// </summary>
    /// <param name="appId">App identifier.</param>
    /// <param name="channel">Channel.</param>
    /// <param name="appName">App name.</param>
    public static void StartWithAppID(string appId, string channel, string appName, bool isLog, bool isEncrypt, bool isGameMode)
    {
#if UNITY_IOS
        TTAnalyticsIOS.StartWithAppID(appId, channel, appName, isLog, isEncrypt, isGameMode);
#else
        TTAnalyticsAndroid.StartWithAppID(appId, appName, channel, isLog, isEncrypt, isGameMode);
#endif
    }

    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="mothed">注册⽅方式 mobile、weixin、qq等(必须上传)</param>
    /// <param name="isSuccess">是否成功(必须上传)</param>
    public static void SetRegister(string mothed, bool isSuccess)
    {
#if UNITY_ANDROID
        TTAnalyticsAndroid.SetRegister(mothed, isSuccess);
#endif
    }

    public static void SetLogin(string mothed, bool isSuccess)
    {
#if UNITY_ANDROID
        TTAnalyticsAndroid.SetLogin(mothed, isSuccess);
#endif
    }

    public static void SetAccessAcount(string mothed, bool isSuccess)
    {
#if UNITY_ANDROID
        TTAnalyticsAndroid.SetAccessAcount(mothed, isSuccess);
#endif
    }

    /// <summary>
    /// 支付
    /// </summary>
    /// <param name="content_type">内容类型.</param>
    /// <param name="content_name">商品/内容名.</param>
    /// <param name="content_id">商品ID/内容ID.</param>
    /// <param name="content_num">商品数量量.</param>
    /// <param name="payment_channel">支付渠道名 如⽀支付宝、微信等.</param>
    /// <param name="currency">真实货币类型</param>
    /// <param name="is_success">是否成功(必须上传)</param>
    /// <param name="currency_amount">本次⽀支付的真实货币的⾦金金额(必须上传，单位:元).</param>
    public static void SetPurchase(string content_type, string content_name, string content_id, int content_num,
                                   string payment_channel, string currency, bool is_success, int currency_amount)
    {
#if UNITY_ANDROID
        TTAnalyticsAndroid.SetPurchase(content_type, content_name, content_id, content_num, payment_channel, currency, is_success, currency_amount);
#elif UNITY_IOS
        TTAnalyticsIOS.PurchaseEventWithContentType(content_name, content_id, content_num, currency, currency_amount, is_success);
#endif

    }


    public static void Event(string eventName, Dictionary<string, object> dictory)
    {
        string jsonData = TTTracker.JsonObjectUtil.Serialize(dictory);
        Event(eventName, jsonData);
    }

    /// <summary>
    /// Event the specified eventId and jsonData.
    /// </summary>
    /// <param name="eventId">Event identifier.</param>
    /// <param name="jsonData">Json data.</param>
    public static void Event(string eventId, string jsonData)
    {
#if UNITY_ANDROID
        TTAnalyticsAndroid.Event(eventId, jsonData);
#elif UNITY_IOS
        TTAnalyticsIOS.TTEvent(eventId, jsonData);
#endif
    }


}
