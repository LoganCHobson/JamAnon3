using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SuperPupSystems.Manager;

public class ShowMoney : MonoBehaviour
{
    public TMP_Text money;
    // Start is called before the first frame update
    void Start()
    {
        if (money == null)
        {
            Debug.LogError("GameObject " + name + " field text in count down timer null!!!!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(WalletManager.instance.coin <= 0)
        {
            WalletManager.instance.coin = 0;
        }
        
        money.text = "$" + Mathf.Ceil(WalletManager.instance.coin);
    }

}
