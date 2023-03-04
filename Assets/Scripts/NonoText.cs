using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NonoText : MonoBehaviour
{
    public int i;

    public void SetText(string text)
    {
        this.GetComponent<TMP_Text>().text = text;
    }
}
