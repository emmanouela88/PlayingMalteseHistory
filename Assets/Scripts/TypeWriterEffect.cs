using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriterEffect : MonoBehaviour
{
    public float delay = 0.01f;
    public TextMeshProUGUI fullText;
    private string currentText;
    float delayBeforeStart = 0.2f;
    void Start()
    {
        //fullText = GetComponent<TextMeshProUGUI>();
        
        Debug.Log("Start: "+fullText.text+" curtext: "+currentText);
       
        
    }

    // Update is called once per frame
    IEnumerator ShowText()
    {
            yield return new WaitForSeconds(delayBeforeStart);
            foreach (char c in currentText)
            {
                fullText.text = fullText.text.Substring(0, fullText.text.Length);
                fullText.text += c;
                yield return new WaitForSeconds(delay);
            }

       
    }

    private void OnEnable()
    {
        if (fullText != null)
        {
            currentText = fullText.text;
            fullText.text = "";
        }
        StartCoroutine(ShowText());
    }

    private void OnDisable()
    {
        StopCoroutine(ShowText());
        fullText.text = "";
        currentText = fullText.text;
    }
}
