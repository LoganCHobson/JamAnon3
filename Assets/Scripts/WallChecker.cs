using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChecker : MonoBehaviour
{

    public bool wall;

    public void OnTriggerEnter(Collider other)
    {
      if (other.gameObject.layer == 7)
        {
            wall = true;
      
        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            wall = false;
        }
    }
}
