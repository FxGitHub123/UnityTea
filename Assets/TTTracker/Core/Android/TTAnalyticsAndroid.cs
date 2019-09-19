using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTAnalyticsAndroid
{
    static AndroidJavaClass androidJavaClass;

    static TTAnalyticsAndroid()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            try
            {
                androidJavaClass = new AndroidJavaClass("com.yodo1.ttanalytics.TTAnalytics");
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("com.yodo1.ttanalytics.TTAnalytics");
            }
        }
    }

    public static void StartWithAppID(string appId,string appName, string channel,bool debug, bool encrypt,bool isGame )
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidJavaClass)
            {
                androidJavaClass.CallStatic("initialize", appId, appName, channel, debug, encrypt, isGame);
            }
        }
    }

    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="mothed">注册⽅方式 mobile、weixin、qq等(必须上传)</param>
    /// <param name="isSuccess">是否成功(必须上传)</param>
    public static void SetRegister(string mothed, bool isSuccess)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidJavaClass)
            {
                androidJavaClass.CallStatic("setRegister", mothed, isSuccess);
            }
        }
    }

    public static void SetLogin(string mothed, bool isSuccess)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidJavaClass)
            {
                androidJavaClass.CallStatic("setLogin", mothed, isSuccess);
            }
        }
    }

    public static void SetAccessAcount(string mothed, bool isSuccess)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidJavaClass)
            {
                androidJavaClass.CallStatic("setAccessAcount", mothed, isSuccess);
            }
        }
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
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidJavaClass)
            {
                androidJavaClass.CallStatic("setPurchase", content_type, content_name, content_id, content_num,
                                            payment_channel, currency, is_success, currency_amount);
            }
        }
    }

    /// <summary>
    /// Event the specified eventId and jsonData.
    /// </summary>
    /// <param name="eventId">Event identifier.</param>
    /// <param name="jsonData">Json data.</param>
    public static void Event(string eventId, string jsonData)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidJavaClass)
            {
                androidJavaClass.CallStatic("onEventV3", eventId, jsonData);
            }
        }
    }

    /// <summary>
    /// Gets the ab test configs.
    /// </summary>
    /// <returns>The ab test configs.</returns>
    public static string GetAbTestConfigs()
    {
        string config = "";
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidJavaClass)
            {
                config = androidJavaClass.CallStatic<string>("getAbTestConfigs");
            }
        }
        return config;
    }

    public static string GetDid()
    {
        string did = "";
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidJavaClass)
            {
                did = androidJavaClass.CallStatic<string>("getDid");
            }
        }

        return did;
    }

    public static string GetUdid()
    {
        string did = "";
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidJavaClass)
            {
                did = androidJavaClass.CallStatic<string>("getUdid");
            }
        }

        return did;
    }

    public static string GetSsid()
    {
        string did = "";
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidJavaClass)
            {
                did = androidJavaClass.CallStatic<string>("getSsid");
            }
        }

        return did;
    }

    public static string GetUserUniqueID()
    {
        string did = "";
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidJavaClass)
            {
                did = androidJavaClass.CallStatic<string>("getUserUniqueID");
            }
        }

        return did;
    }

    public static void SetUserUniqueID(string id)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (null != androidJavaClass)
            {
                androidJavaClass.CallStatic("setUserUniqueID",id);
            }
        }
    }

}
