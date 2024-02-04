using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonitorUI : MonoBehaviour
{
    bool Power;
    public Button buttonToChange;
    // Start is called before the first frame update
    void Start()
    {
        buttonToChange = GameObject.Find("ButtonName").GetComponent<Button>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PressPowerButton()
    {
        if (!Power)
        {
            Power = true;
            buttonToChange.GetComponent<Image>().color = new Color(0, 1, 0, 1); // Sets the color to red
            //turn On Monitor
            //turn On Working UI
            
        }
        else
        {
            Power = false;
            buttonToChange.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1); // Sets the color to red
            //turn On Monitor
            //turn On Working UI
        }
    }
}
