using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MoneyTracker : MonoBehaviour
{
    public GameObject FindGameManager;
    public int Money = 0;
    public TMP_Text Currency;
    public string Currence;
    private void Start()
    {
        FindGameManager = GameObject.Find("GameStateManager");
        
    }
    private void Update()
    {
        Money = FindGameManager.GetComponent<GameStateManager>().PlayerCurrence;
        Currence = Money.ToString();
        Currency.text = Currence +"$";
    }

}
