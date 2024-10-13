using SuperPupSystems.Helper;
using UnityEngine.Events;

public class HealthPack : Pickup
{
    public int scoreValue;
    public UnityEvent pickup;
    public UnityEvent reset;

    public override void onPickupEvent()
    {
        Health health = GameManager.instance.player.GetComponent<Health>();
        health.Heal(health.maxHealth / 2);
        ScoreManager.instance.AddScore(scoreValue);
        pickup.Invoke();
    }

    public void OnEnable()
    {
        reset.Invoke();
    }

}