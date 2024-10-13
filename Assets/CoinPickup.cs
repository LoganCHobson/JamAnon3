using SuperPupSystems.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinPickup : Pickup
{
    public int value;
    public UnityEvent onPickup;
    public UnityEvent reset;

    private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = ScoreManager.instance;
    }
    public override void onPickupEvent()
    {
        GameManager.instance.player.GetComponent<WalletManager>().AddCoin(value);
       // scoreManager.AddMoney(value);
        //scoreManager.AddScore(value * 10);
        onPickup.Invoke();
    }
    public void OnEnable()
    {
        reset.Invoke();
    }
}
