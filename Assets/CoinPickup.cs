using SuperPupSystems.Manager;
using UnityEngine.Events;

public class CoinPickup : Pickup
{
    public int scoreValue;
    public int value;
    public UnityEvent onPickup;
    public UnityEvent reset;

    private void Start()
    {
        
    }
    public override void onPickupEvent()
    {
        GameManager.instance.player.GetComponent<WalletManager>().AddCoin(value);
        ScoreManager.instance.AddMoney(value);
        ScoreManager.instance.AddScore(scoreValue);
        onPickup.Invoke();
    }
    public void OnEnable()
    {
        reset.Invoke();
    }
}
