using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEffect : MonoBehaviour
{
    public AudioSource click;
    public void ButtonClick()
    {
        click.Play();
    }
}
