

#import "KSUnitySDKDelegate.h"

#import "KSUnityUtility.h"


NSString *const KSUnityMessageName_OnInitComplete = @"OnInitComplete";
NSString *const KSUnityMessageName_OnLoginComplete = @"OnLoginComplete";
NSString *const KSUnityMessageName_OnPurchaseComplete = @"OnPurchaseComplete";

static NSMutableArray *g_instances;

@implementation KSUnitySDKDelegate {
  int _requestID;
}

+ (void)initialize
{
  if (self == [KSUnitySDKDelegate class]) {
    g_instances = [NSMutableArray array];
  }
}

+ (instancetype)instanceWithRequestID:(int)requestID
{
  KSUnitySDKDelegate *instance = [[KSUnitySDKDelegate alloc] init];
  instance->_requestID = requestID;
  [g_instances addObject:instance];
  return instance;
}

#pragma mark - Private helpers

- (void)complete
{
  [g_instances removeObject:self];
}


@end
