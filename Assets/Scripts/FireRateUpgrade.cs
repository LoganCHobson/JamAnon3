using UnityEngine.Events;

public class FireRateUpgrade : Pickup
{
    public float increaseFireRate = -0.02f;
    public UnityEvent pickup;

    public override void onPickupEvent()
    {
        GameManager.instance.player.GetComponentInChildren<Gun>().fireRate -= increaseFireRate;
        pickup.Invoke();
    }
}