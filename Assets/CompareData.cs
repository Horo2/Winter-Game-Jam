using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class CompareData : MonoBehaviour
{
    [Header("Check Sheet")]
    public TextAsset itemDataFile;   
    public TMP_Text itemTypeText;
    public TMP_Text statusOfItemText;
    public TMP_Text returnRateText;
    public TMP_Text reviewScoreText;
    public TMP_Text returnReasonText;
    public TMP_Text deliveryInfoPackingText;
    public TMP_Text deliveryTimeText;
    public TMP_Text arraivalPictureText;

    [Header("Summary")]
    public TMP_Text basePay;
    public TMP_Text bonus;
    public TMP_Text deduction;
    public TMP_Text extralBonus;
    public TMP_Text totalToday;
    public TMP_Text dayNumber;

    [Header("NewsPage")]
    public List<GameObject> daysNews = new List<GameObject>();

    [Header("Ending")]
    public List<bool> isCorrectCheck = new List<bool>(); //Check specific order check correctly.
    public List<GameObject> EndingNumber = new List<GameObject>();
    public TMP_Text lastEarning;

    [Space(20)]
    public GameObject checkSheetPerfab;
    public GameObject summaryPagePerfab;
    public GameObject newsPagePerfab;
    public GameObject EndingPerfab;



    private string[] dataRows;
    private int currentIndex = 0;

    
    private int totalPoint;   
    public int returnPoint = 60;
    
    private int todayCheckedSheet;
    public int correctCheck;
    public int faildCheck;
    public int nextEnd = 0;
    private bool GoldFinger;
    private bool isMainNews;
    private int MainNewsNum;

    private GameStateManager gameState;
    private void Start()
    {
        //初始化所需设定

        totalPoint = 0;
        todayCheckedSheet = 0;
        correctCheck = 0;
        nextEnd = 0;
        GoldFinger = false;
        isMainNews = false;
        MainNewsNum = 0;

        for (int i =0; i < isCorrectCheck.Count; i++)
        {
            isCorrectCheck[i] = false;
        }

       //gameState = GameObject.Find("GameStateManager").GetComponent<GameStateManager>();
        gameState = GameObject.Find("TestingGameStateManager").GetComponent<GameStateManager>();
        gameState.Days = 1;
        



        ReadText(itemDataFile);
        ShowNextRow();

    }

    private void Update()
    {
        
    }
    

    public void ReadText(TextAsset _textAsset)
    {
        dataRows = _textAsset.text.Split('\n');
    }

    public void UpdateCheckSheetText(string _itemTypeText, string _statusOfItemText, string _returnRateText, string _reviewScore,
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
                UpdateCheckSheetText(cells[0], cells[1], cells[2], cells[3], cells[4], cells[5], cells[6], cells[7]);

                //  Evaluate Item to give total Point
                EvaluateItemStatus(cells);
                EvaluateReturnRate(cells);
                EvaluateSellerRating(cells); 
                EvaluateReturnReson(cells);

                //do something after evaluare this item
                // example: Output this item total point and is need to return.
                Debug.Log("当前订单总分: " + totalPoint);
                Debug.Log("是否应该返回: " + ShouldReturn());

                if (cells[9].Trim() == "KeyNews")
                {
                    Debug.Log("重要新闻！！！！");
                    isMainNews = true;
                    MainNewsNum += 1;

                }
            }
            currentIndex++; // go to next row.
        }
        else
        {
            Debug.Log("已经到达数据行的末尾");
        }
    }

    public void UpdateSummaryText(string _basePay, string _bonus, string _deduction, string _extralBonus, string _totalToday, string _dayNumber)
    {
        basePay.text = _basePay;
        bonus.text = _bonus;
        deduction.text = _deduction;
        extralBonus.text = _extralBonus;
        totalToday.text = _totalToday;
        dayNumber.text = _dayNumber;
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

    
    //check if this order should return
    public bool ShouldReturn()
    {
        return totalPoint >= returnPoint;
    }

    //Summary Today's reward
    public void SummaryToday()
    {
        if (GoldFinger) { faildCheck -= 3; GoldFinger = false; } //spend 250$ to use goldFinger

        gameState.currentBonus = gameState.bonus * correctCheck;
        gameState.currentDeduction = gameState.deduction * faildCheck;

        //Checking is has extra bonus
        if (faildCheck == 0)
        {
            gameState.totalToday = gameState.BasePaid + gameState.currentBonus + gameState.currentDeduction + gameState.extraBonus;
        }
        else if (faildCheck != 0)
        {
            gameState.totalToday = gameState.BasePaid + gameState.currentBonus + gameState.currentDeduction;
        }
        gameState.PlayerCurrence += gameState.totalToday; //add Today amount to Player currence


        //SHOWING INFO
        UpdateSummaryText(gameState.BasePaid.ToString(), gameState.currentBonus.ToString(), gameState.currentDeduction.ToString(),
                            gameState.extraBonus.ToString(), gameState.totalToday.ToString(),gameState.Days.ToString());

        //todo if use bossbutton, whet effect?

    }

    public void NewsPages()
    {
        newsPagePerfab.SetActive(true);
        foreach (GameObject days in daysNews)
        {
            days.SetActive(false);
        }
        switch(gameState.Days)
        {
            case 1:
                daysNews[0].gameObject.SetActive(true);
                break;
            case 2:
                daysNews[1].gameObject.SetActive(true);
                break;
            case 3:
                daysNews[2].gameObject.SetActive(true);
                break;
            case 4:
                daysNews[3].gameObject.SetActive(true);
                break;
            case 5:
                daysNews[4].gameObject.SetActive(true);
                break;

        }
    }

    public void Ending()
    {  
            if (isCorrectCheck[nextEnd-1])
            {
                EndingNumber[nextEnd].transform.Find("Pass").gameObject.SetActive(true);
                EndingNumber[nextEnd].transform.Find("Fail").gameObject.SetActive(false);
            }
            else if (!isCorrectCheck[nextEnd-1])
            {
                EndingNumber[nextEnd].transform.Find("Pass").gameObject.SetActive(false);
                EndingNumber[nextEnd].transform.Find("Fail").gameObject.SetActive(true);
            }
 
       
    }

    public void PassButton()
    {       
        todayCheckedSheet += 1;
        if (ShouldReturn())
        {
            correctCheck += 1;
            //判定这条订单是否处理正确，随后判定是否是重要订单，如果是则将对应序号的结局设为真。
            if(isMainNews)
            {
                if (!isCorrectCheck[MainNewsNum - 1])
                {
                    isCorrectCheck[MainNewsNum - 1] = true;
                    isMainNews = false;
                }
                
            }          
        }
        else 
        { 
            faildCheck += 1;
            isMainNews = false;
            //todo punish Player

        }

        ShowNextRow();
        if (todayCheckedSheet == 8)
        {
            checkSheetPerfab.SetActive(false);
            summaryPagePerfab.SetActive(true);
            SummaryToday();
        }
    }
    public void FaildButton()
    {
        
        todayCheckedSheet += 1;
        if (!ShouldReturn())
        {
            correctCheck += 1;
            //判定这条订单是否处理正确，随后判定是否是重要订单，如果是则将对应序号的结局设为真。
            if (isMainNews)
            {
                if (!isCorrectCheck[MainNewsNum - 1])
                {
                    isCorrectCheck[MainNewsNum - 1] = true;
                    isMainNews = false;                   
                }
                
            }
        }
        else
        {
            faildCheck += 1;
            isMainNews = false;
            //todo punish Player
        }

        ShowNextRow();
        if (todayCheckedSheet == 8)
        {
            checkSheetPerfab.SetActive(false);
            summaryPagePerfab.SetActive(true);
            SummaryToday();
        }
    }

    public void CompleteSummaryButton()
    {
        //display News Pages
        NewsPages();
    }

    public void NextDayButton()
    {
        gameState.currentBonus = 0;
        gameState.currentDeduction = 0;
        correctCheck = 0;
        faildCheck = 0;
        todayCheckedSheet = 0;
        gameState.Days += 1;

        newsPagePerfab.SetActive(false);
        summaryPagePerfab.SetActive(false);
        if (gameState.Days <= 6)
        {
            checkSheetPerfab.SetActive(true);
        }
       else if(gameState.Days > 6) 
        {
            checkSheetPerfab.SetActive(false);
            EndingPerfab.SetActive(true);

            //Last Summary Page
            Transform lastSummary = EndingNumber[0].transform.Find("Earning");
            lastEarning.text = gameState.PlayerCurrence.ToString();
            if(gameState.PlayerCurrence >= 25000)
            {
                EndingNumber[nextEnd].transform.Find("Passing").gameObject.SetActive(true);
                EndingNumber[nextEnd].transform.Find("Failed").gameObject.SetActive(false);
            }
            else if(gameState.PlayerCurrence <= 25000)
            {
                EndingNumber[nextEnd].transform.Find("Passing").gameObject.SetActive(false);
                EndingNumber[nextEnd].transform.Find("Failed").gameObject.SetActive(true);
            }


        }

        
    }

    public void NextEndingButton()
    {        
        if(nextEnd < 5) //判定不超过结局数
        {
            nextEnd += 1;
            if (nextEnd == 2)//当轮到结局2的时候，跳过结局2
            {
                EndingNumber[nextEnd + 1].SetActive(true);
                if (isCorrectCheck[nextEnd])
                {
                    EndingNumber[nextEnd + 1].transform.Find("Pass").gameObject.SetActive(true);
                    EndingNumber[nextEnd + 1].transform.Find("Fail").gameObject.SetActive(false);
                }
                else if (!isCorrectCheck[nextEnd + 1])
                {
                    EndingNumber[nextEnd + 1].transform.Find("Pass").gameObject.SetActive(false);
                    EndingNumber[nextEnd + 1].transform.Find("Fail").gameObject.SetActive(true);
                }
                nextEnd += 1;
            }
            else
            {               
                EndingNumber[nextEnd].SetActive(true);
                Ending();
            }                                                     
        }
       else if(nextEnd == 5)//最后再显示结局2
        {
            foreach(GameObject ending in EndingNumber)
            {
                ending.SetActive(false);
            }
            EndingNumber[2].SetActive(true);
            if (isCorrectCheck[1])
            {
                EndingNumber[2].transform.Find("Pass").gameObject.SetActive(true);
                EndingNumber[2].transform.Find("Fail").gameObject.SetActive(false);
            }
            else if (!isCorrectCheck[1])
            {
                EndingNumber[2].transform.Find("Pass").gameObject.SetActive(false);
                EndingNumber[2].transform.Find("Fail").gameObject.SetActive(true);
            }

            nextEnd += 1;
        }
        else
        {
            //GameOver
            Debug.Log("游戏结束！！！！！！");
        }
        
    }


    public void GoldFingerButton()
    {
        Debug.Log("金手指使用！！！");
        if (gameState.PlayerCurrence >= 250)
        {
            gameState.PlayerCurrence -= 250;
            GoldFinger = true;           
        }
        else { GoldFinger = false; }
    }
}
