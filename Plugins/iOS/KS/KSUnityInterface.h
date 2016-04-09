

#import <UIKit/UIKit.h>

#import "AppDelegateListener.h"
#include "RegisterMonoModules.h"

//if we are on a version of unity that has the version number defined use it, otherwise we have added it ourselves in the post build step
#if HAS_UNITY_VERSION_DEF
#include "UnityTrampolineConfigure.h"
#endif

@interface KSUnityInterface : NSObject <AppDelegateListener>
{
  //If you make changes in here make the same changes in Assets/Facebook/Scripts/NativeDialogModes.cs
  enum ShareDialogMode
  {
    AUTOMATIC = 0,
    NATIVE = 1,
    WEB = 2,
    FEED = 3,
  };
}

@property (assign, nonatomic) BOOL useFrictionlessRequests;
@property (nonatomic) ShareDialogMode shareDialogMode;

+ (KSUnityInterface *)sharedInstance;
@end
