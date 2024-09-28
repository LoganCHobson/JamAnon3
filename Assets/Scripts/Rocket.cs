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

    public float splashRadius = 15f;
    public float damage;

    void Start()
    {
        damage = GameManager.instance.player.GetComponentInChildren<Gun>().damagePerShot;
        if (bullet != null)
        {
            bullet.hitTarget.AddListener(OnBulletHitTarget);
        }
        else
        {
            Debug.LogError("Bullet reference is not set in Rocket script");
        }
    }

    public void OnBulletHitTarget()
    {
        Debug.Log("Bullet hit target, playing particle systems");
        sparks.Play();
        flash.Play();
        fire.Play();
        smoke.Play();
        /*
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, splashRadius);
        foreach (Collider hitCollider in hitColliders)
        {
            Health targetHealth = hitCollider.GetComponent<Health>();
            if (targetHealth != null)
            {
                targetHealth.Damage(damage);
            }
        }*/
    }
}
