//
//  IAPManager.h
//  Unity-iPhone
//
//  Created by MacMini on 14-5-16.
//
//

extern NSString *const IAPErrorCode_Successed;
extern NSString *const IAPErrorCode_Cancle;
extern NSString *const IAPErrorCode_CannotFindPid;
extern NSString *const IAPErrorCode_NotSupportBuy;
extern NSString *const IAPErrorCode_Restore;
extern NSString *const IAPErrorCode_OtherError;
extern NSString *const IAP_PID_TAG;
extern NSString *const IAPErrorCode_TAG;

#import <Foundation/Foundation.h>
#import <StoreKit/StoreKit.h>

@interface IAPManager : NSObject<SKProductsRequestDelegate, SKPaymentTransactionObserver>{
    SKProduct *proUpgradeProduct;
    SKProductsRequest *productsRequest;
}

-(void)attachObserver;
-(BOOL)CanMakePayment;
-(void)buy:(NSString *)productId;
-(void)restore;
-(void)destroy;
@end
