using SuperPupSystems.Helper;
using UnityEngine.Events;
using UnityEngine.UI;

public class MaxHealthUpgrade : Pickup
{
    public int scoreValue;
    public int increaseHealth;
    public UnityEvent pickup;
    public UnityEvent reset;

    public override void onPickupEvent()
    {
        Health health = GameManager.instance.player.GetComponent<Health>();
        Slider slider = GameManager.instance.player.GetComponentInChildren<Slider>();
        slider.maxValue += increaseHealth;
        ScoreManager.instance.AddScore(scoreValue);
        health.maxHealth += increaseHealth;
        health.currentHealth = health.maxHealth;
        slider.value = health.currentHealth;
        pickup.Invoke();
    }

    public void OnEnable()
    {
        reset.Invoke();
    }
}
