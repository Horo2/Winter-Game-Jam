using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TipsManager : MonoBehaviour
{
    public TMP_Text tipsText;

    private string[] tips = new string[] {
        "If a product is marked as arriving with picture proof, that is confirmed by the admin from the delivery company, meaning the package arrives at a safety spot and is not damaged on the delivery packing. ",
        "Amwalapet focuses on user experience and satisfaction from both the merchant and customer side. A lower return rate is what we are approaching! ",
        "Store ratings are made based on the customer feedback & comment! Most people would like to purchase from a store with a high rating, meaning to have high quality products. ",
        "Big companies may purchase a lot of items in one single shopping, we will ship them in large boxes with wrapping!",
        "Remember to keep your customer feedback positive looking!",
        "Have you ever checked the newspaper? Understanding the current trends can help you become a rich person here!",
        "Never challenge your boss¡­"
    };

    private float tipDuration = 20f; // each tips showing times
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowTipsRoutine());
    }

    private IEnumerator ShowTipsRoutine()
    {
        while (true)
        {
            string currentTip = tips[Random.Range(0, tips.Length)];
            tipsText.text = currentTip;
            tipsText.CrossFadeAlpha(1f, 0.5f, false);
            yield return new WaitForSeconds(tipDuration);
            tipsText.CrossFadeAlpha(0f, 0.5f, false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
