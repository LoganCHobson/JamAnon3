using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageUpgrade : Pickup
{
    public int increaseDamage = 10;
    public UnityEvent pickup;
    public UnityEvent reset;

    public override void onPickupEvent()
    {
        GameManager.instance.player.GetComponentInChildren<Gun>().damagePerShot += increaseDamage;
        pickup.Invoke();
    }

    public void OnEnable()
    {
        reset.Invoke();
    }
}
