using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RunCounter : MonoBehaviour
{
    public int runCounter = 0;
    public TMP_Text runText;
    public void AddToRun()
    {
        runCounter++;
    }

    void Start()
    {
        if (runText == null)
        {
            Debug.LogError("GameObject " + name + " field text in count down timer null!!!!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(runCounter <= 0)
        {
            runCounter = 0;
        }
        
        runText.text = "run: " + Mathf.Ceil(runCounter);
    }
}
