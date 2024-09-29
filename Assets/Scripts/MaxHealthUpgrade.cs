using SuperPupSystems.Helper;
using UnityEngine.Events;

public class MaxHealthUpgrade : Pickup
{
    public int increaseHealth;
    public UnityEvent pickup;
    public UnityEvent reset;

    public override void onPickupEvent()
    {
        Health health = GameManager.instance.player.GetComponent<Health>();

        health.maxHealth += increaseHealth;
        health.currentHealth = health.maxHealth;
        pickup.Invoke();
    }

    public void OnEnable()
    {
        reset.Invoke();
    }
}
