using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuditManager : MonoBehaviour
{
    public CheckSheet checkSheet;

    public List<AuditItem> items; // 存储所有审核单项    
    private int totalPoint = 0; // 总分
    public int returnPoint = 60;

    // 添加一个审核单项
    public void AddItem(AuditItem item)
    {
        items.Add(item);
        CalculateScore(); // 每次添加时重新计算分数
    }

    // 根据每个项的类型和分数变化计算总分
    private void CalculateScore()
    {
        totalPoint = 0; // 重置总分
        foreach (var item in items)
        {
            totalPoint += item.scoreChange;
        }
        CheckIfReturnNeeded(); // 检查是否达到退货标准
    }

    // 检查是否需要退货
    private void CheckIfReturnNeeded()
    {
        if (totalPoint >= returnPoint)
        { // someThreshold为达到退货的分数阈值
            Debug.Log("需要退货");
            // 进一步处理退货逻辑
        }
        else
        {
            Debug.Log("不需要退货");
        }
    }
}
