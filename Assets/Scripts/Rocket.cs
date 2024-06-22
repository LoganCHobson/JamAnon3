using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;

public class Rocket : MonoBehaviour
{
    public ParticleSystem sparks;
    public ParticleSystem flash;
    public ParticleSystem fire;
    public ParticleSystem smoke;

    public Bullet bullet;
    // Start is called before the first frame update
    void Start()
    {
        if (bullet != null)
        {
            bullet.hitTarget.AddListener(OnBulletHitTarget);
        }
        else
        {
            Debug.LogError("Bullet reference is not set in Rocket script");
        }
    }

    // Update is called once per frame
    public void OnBulletHitTarget()
    {
        Debug.Log("Bullet hit target, playing particle systems");
        sparks.Play();
        flash.Play();
        fire.Play();
        smoke.Play();
    }
}
