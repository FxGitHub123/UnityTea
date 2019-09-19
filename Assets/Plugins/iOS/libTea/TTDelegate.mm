
#import <RangersAppLog/BDAutoTrack.h>
#import <RangersAppLog/BDAutoTrackConfig.h>
#import <RangersAppLog/BDAutoTrack+Game.h>


@interface UnityTea : NSObject

@end

@implementation UnityTea

//初始化appID和自定义参数
//[TTTracker startWithAppID:@"10008" channel:@"local_test" appName:@"_test"];
//appID和channel分别表示app唯⼀一标示(头条数据仓库组分配)和App发布的渠道名(建议内测版⽤用loc altest，正式版⽤用App Store，灰度版⽤用发布的渠道名，如pp)
// 是否在控制台输出⽇日志，仅调试使⽤用。release版本请设置为
// 是否加密⽇日志，默认加密。release版本请设置为 YES
// 游戏模式，YES会开始 playSession 上报，每隔⼀一分钟上报⼼心跳⽇日志
+(void)TTStartWithAppID:(NSString*) appID appChannel: (NSString*) appChannel appName: (NSString*) appName log: (BOOL) log encrypt: (BOOL) encrypt gameModeEnable: (BOOL) gameModeEnable {
    NSAssert(appID != nil||appName != nil, @"appID or appName is not set!");
    NSLog(appID,appChannel,appName,log,encrypt,gameModeEnable);
    BDAutoTrackConfig * config=[BDAutoTrackConfig new];
    config.serviceVendor = BDAutoTrackServiceVendorCN; // 域名默认国内，新加坡:BDAutoTrackServiceVendorSG
    config.appID=appID;
    config.appName=appName;
    config.channel=appChannel;
    config.showDebugLog = log;
    config.logNeedEncrypt = encrypt;
    config.gameModeEnable = gameModeEnable;
    [BDAutoTrack startTrackWithConfig:config];
    NSLog(@"Init Finished");
}
    

// isSuccess: 是否成功付费(必须上报)
+(void)TTPurchaseEventWithContentType:(NSString*) productName productID: (NSString*) productID num: (NSUInteger) num currencyType: (NSString*) currencyType currencyAmount: (unsigned long long) currencyAmount state: (BOOL) state {
    NSAssert(productID != nil, @"productID is not set!");
    [BDAutoTrack purchaseEventWithContentType:@"mingwen"
                                  contentName:productName
                                    contentID:productID
                                contentNumber:num
                               paymentChannel:@"AppStore"
                                     currency:currencyType
                              currency_amount:currencyAmount
                                    isSuccess:state];
}

//第一个参数为在头条数据后台注册的事件名称，第二个参数为本次事件需要上传的参数
+(void)TTEvent:(NSString*) eventID eventValue: (NSString*) eventValue {
    NSAssert(eventID != nil, @"eventKey is not set!");
    NSData* jsonData = [eventValue dataUsingEncoding:NSUTF8StringEncoding];
    NSDictionary* dic = [NSJSONSerialization JSONObjectWithData:jsonData options:NSJSONReadingMutableLeaves error:nil];
    if (dic==nil) {
        return;
    }
    NSLog(@"eventid:%@",eventID);
    NSLog(@"eventvalue:%@",eventValue);
    [BDAutoTrack eventV3:eventID params:dic];
}


#ifdef __cplusplus

extern "C" {
    
    void UnityStartWithAppID(const char* appid, const char* channel, const char* name, bool isLog,bool isEncrypt,bool isGameMode) {
        NSString* _appID  = [[NSString alloc] initWithUTF8String:appid];
        NSString* _appChannel = [[NSString alloc] initWithUTF8String:channel];
        NSString* _appName = [[NSString alloc] initWithUTF8String:name];
        BOOL _log = isLog;
        BOOL _encrypt = isEncrypt;
        BOOL _game = isGameMode;
        
        printf("extern c Unity Start");
        
        [UnityTea TTStartWithAppID:_appID appChannel:_appChannel appName:_appName log:_log encrypt:_encrypt gameModeEnable:_game];
        
        
    }
    
    void UnityPurchaseEventWithContentType(const char* productname, const char* productid, int number,const char* currencytype,long amount,bool isSuccess) {
        NSString* _productName = [[NSString alloc] initWithUTF8String:productname];
        NSString* _productID = [[NSString alloc] initWithUTF8String:productid];
        NSString* _currencyType=[[NSString alloc] initWithUTF8String:currencytype];
        //NSUInteger* _number = number;
        [UnityTea TTPurchaseEventWithContentType:_productName productID:_productID num:number currencyType:_currencyType currencyAmount:amount state:isSuccess];
    }
    
    void UnityTTEvent(const char* eventId, const char* value) {
        NSString* _eventId = [[NSString alloc] initWithUTF8String:eventId];
        NSString* _eventvalue = [[NSString alloc] initWithUTF8String:value];
        printf("unity event");
        printf("%s",eventId);
        printf("%s",value);
        [UnityTea TTEvent:_eventId eventValue:_eventvalue];
    }
    
}

#endif

@end
