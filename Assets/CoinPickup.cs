using SuperPupSystems.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinPickup : Pickup
{
    public int value;
    public UnityEvent onPickup;
    
    public override void onPickupEvent()
    {
        GameManager.instance.player.GetComponent<WalletManager>().AddCoin(value);
        onPickup.Invoke();
    }
}
