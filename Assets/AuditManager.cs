using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuditManager : MonoBehaviour
{
    public CheckSheet checkSheet;

    public List<AuditItem> items; // �洢������˵���    
    private int totalPoint = 0; // �ܷ�
    public int returnPoint = 60;

    // ���һ����˵���
    public void AddItem(AuditItem item)
    {
        items.Add(item);
        CalculateScore(); // ÿ�����ʱ���¼������
    }

    // ����ÿ��������ͺͷ����仯�����ܷ�
    private void CalculateScore()
    {
        totalPoint = 0; // �����ܷ�
        foreach (var item in items)
        {
            totalPoint += item.scoreChange;
        }
        CheckIfReturnNeeded(); // ����Ƿ�ﵽ�˻���׼
    }

    // ����Ƿ���Ҫ�˻�
    private void CheckIfReturnNeeded()
    {
        if (totalPoint >= returnPoint)
        { // someThresholdΪ�ﵽ�˻��ķ�����ֵ
            Debug.Log("��Ҫ�˻�");
            // ��һ�������˻��߼�
        }
        else
        {
            Debug.Log("����Ҫ�˻�");
        }
    }
}
