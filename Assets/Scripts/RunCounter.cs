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
        runText.text = "run: " + runCounter.ToString();
    }

    void Start()
    {
        runText.text = "run: " + runCounter.ToString();
    }

}
