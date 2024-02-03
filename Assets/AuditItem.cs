using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuditItem : MonoBehaviour
{
    public string name; // 审核单项的名称
    public int type; // 审核单项的类型
    public int scoreChange; // 该项的分数变化，可以为正数也可以为负数
}
