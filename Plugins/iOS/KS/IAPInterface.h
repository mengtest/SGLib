//
//  UJSInterface.h
//  Unity-iPhone
//
//  Created by MacMini on 14-5-15.
//
//

#import <Foundation/Foundation.h>

@interface IAPInterface : NSObject
+(void) InitIAPManager;
+(bool) IsProductAvailable;
+(void) Requst:(NSString *) ProductInfoS;
+(void) Buy:(NSString *) Product;
+(void)restore;
+(void)destroy;
@end
