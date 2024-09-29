using UnityEngine.Events;

public class FireRateUpgrade : Pickup
{
    public float increaseFireRate = -0.02f;
    public UnityEvent pickup;
    public UnityEvent reset;

    public override void onPickupEvent()
    {
        GameManager.instance.player.GetComponentInChildren<Gun>().fireRate -= increaseFireRate;
        pickup.Invoke();
    }

    public void OnEnable()
    {
        reset.Invoke();
    }
}