using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CompareData : MonoBehaviour
{
    
    public TextAsset itemDataFile;

    public TMP_Text itemTypeText;
    public TMP_Text statusOfItemText;
    public TMP_Text returnRateText;
    public TMP_Text reviewScoreText;
    public TMP_Text returnReasonText;
    public TMP_Text deliveryInfoPackingText;
    public TMP_Text deliveryTimeText;
    public TMP_Text arraivalPictureText;

    private string[] dataRows;
    private int currentIndex = 0; 


    private int totalPoint;
    public int returnPoint = 60;


    private void Start()
    {
        totalPoint = 0;
        ReadText(itemDataFile);
        ShowNextRow();

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 检测鼠标左键点击
        {
            ShowNextRow();
        }
    }
    public bool ShouldReturn()
    {
        return totalPoint >= returnPoint;
    }

    public void ReadText(TextAsset _textAsset)
    {
        dataRows = _textAsset.text.Split('\n');
    }

    public void UpdateText(string _itemTypeText, string _statusOfItemText, string _returnRateText, string _reviewScore,
                            string _returnReson, string _deliveryInfoPacking, string _deliveryTime, string _arrivalPicture)
    {
        itemTypeText.text = _itemTypeText;
        statusOfItemText.text = _statusOfItemText; // 设置status of item文本组件的内容
        returnRateText.text = _returnRateText;
        reviewScoreText.text = _reviewScore;
        returnReasonText.text = _returnReson;
        deliveryInfoPackingText.text = _deliveryInfoPacking;
        deliveryTimeText.text = _deliveryTime;
        arraivalPictureText.text = _arrivalPicture;
    }

    public void ShowNextRow()
    {
        if (currentIndex < dataRows.Length) // makesure in range
        {
            // resettotalPoint
            totalPoint = 0;

            string[] cells = dataRows[currentIndex].Split(',');
            if (cells.Length > 7) // makesure has enough cells
            {
                // updateText
                UpdateText(cells[0], cells[1], cells[2], cells[3], cells[4], cells[5], cells[6], cells[7]);

                //  Evaluate Item to give total Point
                EvaluateItemStatus(cells);
                EvaluateReturnRate(cells);
                EvaluateSellerRating(cells); 
                EvaluateReturnReson(cells);

                //do something after evaluare this item
                // example: Output this item total point and is need to return.
                Debug.Log("当前总分: " + totalPoint);
                Debug.Log("是否应该返回: " + ShouldReturn());
            }
            currentIndex++; // go to next row.
        }
        else
        {
            Debug.Log("已经到达数据行的末尾");
        }
    }

    public void EvaluateItemStatus(string[] cells)
    {
        string itemStatusText = cells[1].Trim();
        switch (itemStatusText)
        {
            case "new":
                totalPoint += 70;
                break;
            case "packing needed":
                totalPoint += 70;
                break;
            case "need fixing by merchant":
                totalPoint -= 50;
                break;
            case "damaged":
                totalPoint -= 25;
                break;
            default: break;
        }
    }

    public void EvaluateReturnRate(string[] cells)
    {
        string returnRateText = cells[2].Trim();
        switch (returnRateText)
        {
            case ">10":
                totalPoint -= 5;
                break;
            case ">20":
                totalPoint -= 15;
                break;
            case ">50":
                totalPoint -= 30;
                break;
            case "<=50":
                totalPoint -= 15;
                break;
        }
    }
    public void EvaluateSellerRating(string[] cells)
    {
        
        string sellerRatingText = cells[3]; 
        int rating;
        if (int.TryParse(sellerRatingText.Trim(), out rating)) // text to int
        {
            switch (rating)
            {
                case 1: // 1 star
                    totalPoint += 20;
                    break;
                case 2: // 2 stars
                    totalPoint += 10;
                    break;
                case 3: // 3 stars
                    totalPoint += 5;
                    break;
                case 4: // 4 stars, do nothing
                case 5: // 5 stars, do nothing
                    break;
                default:
                    Debug.Log("未知的评分值: " + rating);
                    break;
            }
        }
        else
        {
            Debug.Log("评分解析失败: " + sellerRatingText);
        }
    }
    public void EvaluateReturnReson(string[] cells) 
    {
        string returnResonText = cells[4].Trim();
        switch(returnResonText)
        {
            case "ordered wrong product/size":
                totalPoint += 15;
                break;
            case "received wrong product/size":
                totalPoint += 20;
                break;
            case "damaged/defective":
                totalPoint += 20;
                break;
            case "arrived too late":
                totalPoint += 5;
                break;
            case "no longer needed":
                totalPoint += 15;
                break;
            case "implusive purchase":
                totalPoint += 5;
                break;
            case "does not match description":
                totalPoint += 10;
                break;
            case "lower than expectation":
                totalPoint += 5;
                break;
            case "fraudulent purchase":
                totalPoint += 15;
                break;
                default : break;
        }
        
    }


}
