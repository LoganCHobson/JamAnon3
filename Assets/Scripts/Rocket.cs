using SuperPupSystems.Helper;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public ParticleSystem sparks;
    public ParticleSystem flash;
    public ParticleSystem fire;
    public ParticleSystem smoke;

    public Bullet bullet;

    public float splashRadius = 15f;
    public int splashDamage;
    public int damage;

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
        Debug.Log("Ran");
        Debug.Log("Bullet hit target, playing particle systems");
        sparks.Play();
        flash.Play();
        fire.Play();
        smoke.Play();

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, splashRadius);
        
        List<Health> healthComponents = hitColliders //We gotta do this because theres too many colliders on one obj.
            .Select(collider => collider.GetComponent<Health>())
            .Where(health => health != null)  
            .Distinct()                      
            .ToList();                       

        foreach (Health healthComponent in healthComponents)
        {
            GameObject hitObject = healthComponent.gameObject; 
            Debug.Log("Hit: " + hitObject.name);
            healthComponent.Damage(splashDamage);
        }
    }
}
