using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSheet : MonoBehaviour
{
    public enum SellerRatingType
    {
        OneStar,
        TwoStars,
        ThreeStars,
        FourStars,
        FiveStars
    }

    public enum ReturnReasonType
    {
        OrderedWrongProduct,
        ReceivedWrongProduct,
        DamagedOrDefective,
        ArrivedTooLate,
        NoLongerNeeded,
        ImpulsivePurchase,
        DoesNotMatchDescription,
        LowerThanExpectation,
        FraudulentPurchase
    }

    public enum ReturnRateType
    {
        AlwaysReturn,
        UsuallyReturn,
        FairReturn,
        InRangeAllowableError
    }

    private int totalPoint;
    public int returnPoint = 60;

    private bool isExpect;
    private bool isDescription;
   
    void Start()
    {
        totalPoint = 0;
    }
    public bool ShouldReturn()
    {
        return totalPoint >= returnPoint;
    }
   
    public void SellerRating(SellerRatingType Type)
    {
        switch(Type)
        {
            case SellerRatingType.OneStar: // 1 star
                totalPoint += 20;
                break;
            case SellerRatingType.TwoStars:// 2 star
                totalPoint += 10;
                if (isDescription) { totalPoint += 15; }
                if (isExpect) { totalPoint += 10; }               
                break;
            case SellerRatingType.ThreeStars: // 3 star
                totalPoint += 5;
                if (isDescription || isExpect) { totalPoint += 10; }
                break;
            // 4 & 5 star, do nothing
            case SellerRatingType.FourStars:
                break;
            case SellerRatingType.FiveStars:
                break;
            default: 
                break;
        }
    }

    public void ReturnReson(ReturnReasonType Type)
    {
        switch(Type)
        {
            case ReturnReasonType.OrderedWrongProduct: //ordered wrong product/size
                totalPoint += 15;
                break;
            case ReturnReasonType.ReceivedWrongProduct://received wrong product/size
                totalPoint += 20;
                break;
            case ReturnReasonType.DamagedOrDefective://damaged/defective
                totalPoint += 25;
                break;
            case ReturnReasonType.ArrivedTooLate: //arrived too late
                totalPoint += 5;
                break;
            case ReturnReasonType.NoLongerNeeded: //no longer needed
                totalPoint += 10;
                break;
            case ReturnReasonType.ImpulsivePurchase: //implusive purchase
                totalPoint += 5;
                break;
            case ReturnReasonType.DoesNotMatchDescription: //does not match description
                totalPoint += 10;
                break;
            case ReturnReasonType.LowerThanExpectation: //lower than expectation
                totalPoint += 5;
                break;
            case ReturnReasonType.FraudulentPurchase: //fraudulent purchase
                totalPoint += 15;
                break;
            default:
                break;
        }
    }

    public void ReturnRate(ReturnRateType Type)
    {
        switch (Type)
        {
            case ReturnRateType.AlwaysReturn: // always return
                totalPoint -= 30;
                break;
            case ReturnRateType.UsuallyReturn: // usually return
                totalPoint -= 15;
                break;
            case ReturnRateType.FairReturn: // Fair retun
                totalPoint -= 5;
                break;
            case ReturnRateType.InRangeAllowableError: // In rage of the allowable error, do nothing
                break;
        }
    }


}
