using System.Runtime.InteropServices;
using UnityEngine;

public class TTAnalyticsIOS
{
#if UNITY_IOS

    public const string LIB_NAME = "__Internal";

    [DllImport(LIB_NAME)]
    private static extern void UnityStartWithAppID(string appId, string channel, string appName, bool isLog, bool isEncrypt, bool isGameMode);

    /// <summary>
    /// appID和channel分别表示app唯⼀一标示(头条数据仓库组分配)和App发布的渠道名(建议内测版⽤用localtest，正式版⽤用App Store)
    /// </summary>
    /// <param name="appId">App identifier.</param>
    /// <param name="channel">Channel.</param>
    /// <param name="appName">App name.</param>
    public static void StartWithAppID(string appId, string channel, string appName, bool isLog, bool isEncrypt, bool isGameMode)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            UnityStartWithAppID(appId, channel, appName, isLog, isEncrypt, isGameMode);
        }
    }

    [DllImport(LIB_NAME)]
    private static extern void UnityPurchaseEventWithContentType(string productname, string productid, int number, string currencytype, long amount, bool isSuccess);

    /// <summary>
    /// Purchases the type of the event with content.
    /// </summary>
    /// <param name="productname">Productname.</param>
    /// <param name="productid">Productid.</param>
    /// <param name="number">Number.</param>
    /// <param name="currencytype">Currencytype.</param>
    /// <param name="amount">Amount.</param>
    /// <param name="isSuccess">If set to <c>true</c> is success.</param>
    public static void PurchaseEventWithContentType(string productname, string productid, int number, string currencytype, long amount, bool isSuccess)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            UnityPurchaseEventWithContentType(productname, productid, number, currencytype, amount, isSuccess);
        }
    }

    [DllImport(LIB_NAME)]
    private static extern void UnityTTEvent(string eventID, string value);

    /// <summary>
    /// /// 第一个参数为在头条数据后台注册的事件名称，第二个参数为本次事件需要上传的参数 
    /// </summary>
    /// <param name="eventID">Event identifier.</param>
    /// <param name="jsonData">Json data.</param>
    public static void TTEvent(string eventID, string jsonData)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            UnityTTEvent(eventID, jsonData);
        }
    }

#endif
}