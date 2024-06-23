using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseRunCounter : MonoBehaviour
{
    public RunCounter counter;

    public TMP_Text runText; 

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
        if(counter.runCounter <= 0)
        {
            counter.runCounter = 0;
        }
        
        runText.text = "" + Mathf.Ceil(counter.runCounter);
    }
}
