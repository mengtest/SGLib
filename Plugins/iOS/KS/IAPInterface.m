//
//  UJSInterface.m
//  Unity-iPhone
//
//  Created by MacMini on 14-5-15.
//
//

#import "IAPInterface.h"
#import "IAPManager.h"

@implementation IAPInterface


static IAPManager *iapManager = nil;

+(void) InitIAPManager{
    NSLog(@"InitIAPManager");
    iapManager = [[IAPManager alloc] init];
    [iapManager attachObserver];
    
}

+(bool) IsProductAvailable{
    return [iapManager CanMakePayment];
}

+(void) Requst:(NSString *) ProductInfoS{
   // NSString *list = [NSString stringWithUTF8String:p];
    NSLog(@"productKey:%@",ProductInfoS);
  //  [iapManager requestProductData:ProductInfoS];
}

+(void) Buy:(NSString *) Product{
    [iapManager buy:Product];
}

+(void)restore{
    [iapManager restore];
}
+(void)destroy{
    [iapManager destroy];
    iapManager = nil;
}

@end
