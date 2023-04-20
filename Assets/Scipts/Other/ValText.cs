using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValText : MonoBehaviour
{
    [SerializeField] private Text textObj;
    [SerializeField] private string textDisplay;
    private void Awake()
    {
        textObj = GetComponent<Text>();
    }
    private void Update()
    {
        SetText();
    }
    public void SetText()
    {
        float volumeVal = PlayerPrefs.GetFloat("soundVolume", 1) * 100;
        textObj.text = textDisplay + ": " + volumeVal.ToString();
    }
}
