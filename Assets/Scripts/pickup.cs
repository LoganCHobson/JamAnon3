using SolarStudios;
using SuperPupSystems.Helper;
using SuperPupSystems.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class pickup : MonoBehaviour
{
    public int value;
    public float rotationSpeed = 25f;
    private AudioSource audio;

    public UnityEvent pickupCoin;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    void Update()
    {
        gameObject.transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
            pickupCoin.Invoke();
            WalletManager.instance.AddCoin(value);
            gameObject.SetActive(false);
        }
    }
}
