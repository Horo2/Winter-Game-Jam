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
    private int currentIndex = 0; // 当前显示的行索引

    private void Start()
    {
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
        if (currentIndex < dataRows.Length) // 确保索引在范围内
        {
            string[] cells = dataRows[currentIndex].Split(',');
            if (cells.Length > 1) // 假设至少有两个单元格：一个用于item type，另一个用于status of item
            {
                UpdateText(cells[0], cells[1], cells[2], cells[3], cells[4], cells[5], cells[6], cells[7]); // 更新两个文本显示
            }
            currentIndex++; // 移动到下一行
        }
    }
}
