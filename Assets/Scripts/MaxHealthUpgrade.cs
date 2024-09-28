using SuperPupSystems.Helper;
using UnityEngine.Events;

public class MaxHealthUpgrade : Pickup
{
    public int increaseHealth;
    public UnityEvent pickup;

    public override void onPickupEvent()
    {
        Health health = GameManager.instance.player.GetComponent<Health>();

        health.maxHealth += increaseHealth;
        health.currentHealth = health.maxHealth;
        pickup.Invoke();
    }
}
