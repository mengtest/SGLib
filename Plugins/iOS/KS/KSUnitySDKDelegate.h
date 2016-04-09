


extern NSString *const KSUnityMessageName_OnInitComplete;
extern NSString *const KSUnityMessageName_OnLoginComplete;
extern NSString *const KSUnityMessageName_OnPurchaseComplete;

/*!
 @abstract A helper class that implements various FBSDK delegates in order to send
 messages back to Unity.
 */
@interface KSUnitySDKDelegate : NSObject
/*
 @abstract returns a self retaining instance that is released once it receives a
 delegate message from FBSDK.
 */
+ (instancetype)instanceWithRequestID:(int)requestID;

@end
