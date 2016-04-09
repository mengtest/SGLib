//
//  IAPManager.m
//  Unity-iPhone
//
//  Created by MacMini on 14-5-16.
//
//

#import "IAPManager.h"
#include "KSUnityUtility.h"
#include "KSUnitySDKDelegate.h"

 NSString *const IAPErrorCode_Successed = @"1";
 NSString *const IAPErrorCode_Cancle= @"2";
 NSString *const IAPErrorCode_CannotFindPid= @"3";
 NSString *const IAPErrorCode_NotSupportBuy= @"4";
 NSString *const IAPErrorCode_Restore= @"5";
 NSString *const IAPErrorCode_OtherError= @"6";
 NSString *const IAP_PID_TAG= @"pid";
 NSString *const IAPErrorCode_TAG= @"errorcode";

@implementation IAPManager

-(void) attachObserver{
    NSLog(@"AttachObserver");
    [[SKPaymentQueue defaultQueue] addTransactionObserver:self];
}

-(void)destroy
{
    //解除监听
    [[SKPaymentQueue defaultQueue] removeTransactionObserver:self];
    NSLog(@"RMIAPHelper 注销交易监听");
    
}

-(BOOL)CanMakePayment
{
    return [SKPaymentQueue canMakePayments];
}

-(void)buy:(NSString*)productId
{
    if([self CanMakePayment])
    {
        [self requestProduct:productId];
    }else
    {
        NSLog(@"不支持内购");
       NSDictionary *params = [NSDictionary dictionaryWithObjectsAndKeys:productId,IAP_PID_TAG,IAPErrorCode_NotSupportBuy,IAPErrorCode_TAG, nil];
        [KSUnityUtility sendMessageToUnity:KSUnityMessageName_OnPurchaseComplete userData:params requestId:0];
    }
}

-(void)requestProduct:(NSString*)productId
{
    NSArray *product = [[NSArray alloc] initWithObjects:productId,nil];
    NSSet *nsset = [NSSet setWithArray:product];
    SKProductsRequest *request=[[SKProductsRequest alloc] initWithProductIdentifiers: nsset];
    request.delegate=self;
    [request start];
}

#pragma mark SKProductsRequestDelegate
- (void)productsRequest:(SKProductsRequest *)request didReceiveResponse:(SKProductsResponse *)response
{
    //[self.delegate requestProduct:self received:request];
    
    NSLog(@"didReceiveResponse called:");
    NSLog(@"prodocuId:%@",response.products);
    NSLog(@"=======================================================");
    
    NSArray *productArray = response.products;
    if(productArray != nil && productArray.count>0)
    {
        SKProduct *product = [productArray objectAtIndex:0];
        NSLog(@"SKProduct 描述信息%@", [product description]);
        NSLog(@"产品标题 %@" , product.localizedTitle);
        NSLog(@"产品描述信息: %@" , product.localizedDescription);
        NSLog(@"价格: %@" , product.price);
        NSLog(@"Product id: %@" , product.productIdentifier);
        
        SKPayment* payment = [SKPayment paymentWithProduct:product];
        [[SKPaymentQueue defaultQueue] addPayment:payment];
    }else{
         NSLog(@"===================rrrrrrrr===========================");
        NSDictionary *params = [NSDictionary dictionaryWithObjectsAndKeys:@"-1",IAP_PID_TAG,IAPErrorCode_CannotFindPid,IAPErrorCode_TAG, nil];
        [KSUnityUtility sendMessageToUnity:KSUnityMessageName_OnPurchaseComplete userData:params requestId:0];
    }
}

-(NSString *)productInfo:(SKProduct *)product{
    NSArray *info = [NSArray arrayWithObjects:product.localizedTitle,product.localizedDescription,product.price,product.productIdentifier, nil];
    
    return [info componentsJoinedByString:@"\t"];
}

-(NSString *)transactionInfo:(SKPaymentTransaction *)transaction{
    
    return [self encode:(uint8_t *)transaction.transactionReceipt.bytes length:transaction.transactionReceipt.length];
    
    //return [[NSString alloc] initWithData:transaction.transactionReceipt encoding:NSASCIIStringEncoding];
}

-(NSString *)encode:(const uint8_t *)input length:(NSInteger) length{
    static char table[] = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
    
    NSMutableData *data = [NSMutableData dataWithLength:((length+2)/3)*4];
    uint8_t *output = (uint8_t *)data.mutableBytes;
    
    for(NSInteger i=0; i<length; i+=3){
        NSInteger value = 0;
        for (NSInteger j= i; j<(i+3); j++) {
            value<<=8;
            
            if(j<length){
                value |=(0xff & input[j]);
            }
        }
        
        NSInteger index = (i/3)*4;
        output[index + 0] = table[(value>>18) & 0x3f];
        output[index + 1] = table[(value>>12) & 0x3f];
        output[index + 2] = (i+1)<length ? table[(value>>6) & 0x3f] : '=';
        output[index + 3] = (i+2)<length ? table[(value>>0) & 0x3f] : '=';
    }
    
    return [[NSString alloc] initWithData:data encoding:NSASCIIStringEncoding];
}


-(void)paymentQueue:(SKPaymentQueue *)queue updatedTransactions:(NSArray *)transactions{
    for (SKPaymentTransaction *transaction in transactions) {
        switch (transaction.transactionState) {
            case SKPaymentTransactionStatePurchased:
                [self completeTransaction:transaction];
                break;
            case SKPaymentTransactionStateFailed:
                [self failedTransaction:transaction];
                break;
            case SKPaymentTransactionStateRestored:
                [self restoreTransaction:transaction];
                break;
            default:
                break;
        }
    }
}

-(void) completeTransaction:(SKPaymentTransaction *)transaction{
    NSLog(@"Comblete transaction : %@",transaction.transactionIdentifier);
    NSDictionary *params = [NSDictionary dictionaryWithObjectsAndKeys:transaction.payment.productIdentifier,IAP_PID_TAG,IAPErrorCode_Successed,IAPErrorCode_TAG, nil];
    [KSUnityUtility sendMessageToUnity:KSUnityMessageName_OnPurchaseComplete userData:params requestId:0];

    [[SKPaymentQueue defaultQueue] finishTransaction:transaction];
}

-(void) failedTransaction:(SKPaymentTransaction *)transaction{
    NSLog(@"Failed transaction : %@",transaction.transactionIdentifier);
    NSString * errorcode = IAPErrorCode_Cancle;
    if (transaction.error.code != SKErrorPaymentCancelled) {
        NSLog(@"!Cancelled");
      errorcode = IAPErrorCode_OtherError;
 
    }
     NSDictionary *params = [NSDictionary dictionaryWithObjectsAndKeys:transaction.payment.productIdentifier,IAP_PID_TAG,errorcode,IAPErrorCode_TAG, nil];
        [KSUnityUtility sendMessageToUnity:KSUnityMessageName_OnPurchaseComplete userData:params requestId:0];
        [[SKPaymentQueue defaultQueue] finishTransaction:transaction];

   }

-(void) restoreTransaction:(SKPaymentTransaction *)transaction{
    NSLog(@"Restore transaction : %@",transaction.transactionIdentifier);
    NSDictionary *params = [NSDictionary dictionaryWithObjectsAndKeys:transaction.payment.productIdentifier,IAP_PID_TAG,IAPErrorCode_Restore,IAPErrorCode_TAG, nil];
    [KSUnityUtility sendMessageToUnity:KSUnityMessageName_OnPurchaseComplete userData:params requestId:0];
    [[SKPaymentQueue defaultQueue] finishTransaction:transaction];
}


#pragma 恢复流程
//发起恢复
-(void)restore
{
    [[SKPaymentQueue defaultQueue] restoreCompletedTransactions];
}

@end
