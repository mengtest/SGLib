

#include "KSUnityInterface.h"

#import <Foundation/NSJSONSerialization.h>

#include "KSUnitySDKDelegate.h"
#include "KSUnityUtility.h"
#include "KSSDK+Internal.h"
#include "IAPInterface.h"

static KSUnityInterface *_instance = [KSUnityInterface sharedInstance];

@interface KSUnityInterface()
@property (nonatomic, copy) NSString *openURLString;
@end

@implementation KSUnityInterface

#pragma mark Object Initialization

+ (KSUnityInterface *)sharedInstance
{
  return _instance;
}

+ (void)initialize {
  if(!_instance) {
    _instance = [[KSUnityInterface alloc] init];
  }
}

- (id)init
{
  if(_instance != nil) {
    return _instance;
  }

  if ((self = [super init])) {
    _instance = self;
    self.shareDialogMode = ShareDialogMode::AUTOMATIC;

    UnityRegisterAppDelegateListener(self);
  }
  return self;
}

#pragma mark - App (Delegate) Lifecycle

// didBecomeActive: and onOpenURL: are called by Unity's AppController
// because we implement <AppDelegateListener> and registered via UnityRegisterAppDelegateListener(...) above.

- (void)didFinishLaunching:(NSNotification *)notification
{
   NSLog(@"didFinishLaunching");
}

- (void)didBecomeActive:(NSNotification *)notification
{
   NSLog(@"didBecomeActive");
}

- (void)onOpenURL:(NSNotification *)notification
{
    NSLog(@"onOpenURL");
}

#pragma mark - Implementation

- (void)configureAppId
{
    NSLog(@"configureAppId");

}

- (void)logInWithPublishPermissions:(int) requestId
                             scope:(const char *)scope
{
  [self startLogin:requestId scope:scope isPublishPermLogin:YES];
}

- (void)logInWithReadPermissions:(int) requestId
                           scope:(const char *)scope
{
  [self startLogin:requestId scope:scope isPublishPermLogin:NO];
}

- (void)startLogin:(int) requestId
             scope:(const char *)scope
isPublishPermLogin:(BOOL)isPublishPermLogin
{
    NSLog(@"startLogin");
}

- (void)logOut
{
    NSLog(@"logOut");

}



- (BOOL)tryCompleteLoginWithRequestId:(int) requestId
{
    
    NSLog(@"tryCompleteLoginWithRequestId");

     return NO;
  
}

@end

#pragma mark - Actual Unity C# interface (extern C)

extern "C" {

  void KSIOSInit()
  {
    NSLog(@"IOSInit");
       [IAPInterface InitIAPManager];
    [[KSUnityInterface sharedInstance] configureAppId];
     
      
      [KSUnityUtility sendMessageToUnity:KSUnityMessageName_OnInitComplete userData:@{} requestId:0];
  }
    
    void KSLogIn(int requestId,const char * scope){
        NSLog(@"KSLogIn");
        [KSUnityUtility sendMessageToUnity:KSUnityMessageName_OnLoginComplete userData:@{} requestId:requestId];
    }

  
    void KSIOSPurchase(int requestId,
                                   int numParams,
                                   const char **paramKeys,
                                   const char **paramVals)
    {
        NSDictionary *params =  [KSUnityUtility dictionaryFromKeys:paramKeys values:paramVals length:numParams];
        NSString *pid = [params objectForKey:@"pid"];
      //  [KSUnityUtility sendMessageToUnity:KSUnityMessageName_OnPurchaseComplete userData:params requestId:requestId];
        [IAPInterface Buy:pid];
    }
 

 
}
