using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;
using UnityEngine.Events;

public class HealthPack : Pickup
{
    public int value;
    public UnityEvent pickup;
        
public override void onPickupEvent()
    {
        Health health = GameManager.instance.player.GetComponent<Health>();
        health.Heal(health.maxHealth / 2);
        pickup.Invoke();
    }
}