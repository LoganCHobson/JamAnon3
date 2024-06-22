using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;

public class AcidicWater : MonoBehaviour
{
    //Needs to change but is alright for now
    public Health playerHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Has Taken Damage");
            playerHealth.Damage(1);
        }
    }
}
