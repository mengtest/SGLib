

extern const char* const KS_OBJECT_NAME;

@interface KSUnityUtility : NSObject
+ (NSDictionary *)dictionaryFromKeys:(const char **)keys values:(const char **)vals length:(int)length;
+ (void)sendCancelToUnity:(NSString *)unityMessage requestId:(int)requestId;
+ (void)sendErrorToUnity:(NSString *)unityMessage error:(NSError *)error requestId:(int)requestId;
+ (void)sendErrorToUnity:(NSString *)unityMessage errorMessage:(NSString *)errorMessage requestId:(int)requestId;
+ (void)sendMessageToUnity:(NSString *)unityMessage userData:(NSDictionary *)userData requestId:(int)requestId;
+ (NSString *)stringFromCString:(const char *)string;
@end
