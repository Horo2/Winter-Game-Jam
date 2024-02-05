using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MoneyFlyAway : MonoBehaviour
{
    public GameObject myself;
    public TMP_Text Money;
    private float Duration = 3f; // each tips showing times
    private bool goingon = false;
    public void FadesAway()
    {
        StartCoroutine(FadingAway());
        
    }
    IEnumerator FadingAway()
    {
        if (goingon == true)
        {
            while (myself == true)
            {
                Money.text += "\n-250$";
                Money.CrossFadeAlpha(1f, 0.5f, false);
                yield return new WaitForSeconds(Duration);
                Money.CrossFadeAlpha(0f, 0.5f, false);
                yield return new WaitForSeconds(0.5f);
                myself.SetActive(false);
                yield break;
            }
        }
        else
        {
            while (myself == true)
            {
                goingon = true;
                Money.text = "-250$";
                Money.CrossFadeAlpha(1f, 0.5f, false);
                yield return new WaitForSeconds(Duration);
                Money.CrossFadeAlpha(0f, 0.5f, false);
                yield return new WaitForSeconds(0.5f);
                myself.SetActive(false);
                goingon = false;
                yield break;
                
            }
        }
    }
}
