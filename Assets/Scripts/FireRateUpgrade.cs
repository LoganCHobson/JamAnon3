using UnityEngine.Events;

public class FireRateUpgrade : Pickup
{
    public int scoreValue;
    public float increaseFireRate = -0.02f;
    public UnityEvent pickup;
    public UnityEvent reset;

    public override void onPickupEvent()
    {
        GameManager.instance.player.GetComponentInChildren<Gun>().fireRate -= increaseFireRate;
        ScoreManager.instance.AddScore(scoreValue);
        pickup.Invoke();
    }

    public void OnEnable()
    {
        reset.Invoke();
    }
}