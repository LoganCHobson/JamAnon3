using UnityEngine;

public class Gun : MonoBehaviour
{
    public PauseMenu pausedGame;

    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;

    public float fireRate = 0.2f;
    public float weaponRange = 50f;
    public int damagePerShot = 20;

    public int ammo = int.MaxValue;
    public int limitedAmmo = 10;

    private float nextFireTime;

    public int fireMode = 0;

    public bool infintAmmo = true;

    public LayerMask layerMask;

    GameObject gunHolder;
    
    private AudioSource shoot;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();    
        shoot = GetComponent<AudioSource>();
        gunHolder = GetComponentInParent<WeaponSway>().gameObject;
        UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
        if (pausedGame == null)
        {
            pausedGame = new PauseMenu();
        }
    }
    // Update is called once per frame
    void Update()
    {
        AutoAim();

        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= nextFireTime && ammo != 0 && fireMode == 0 && pausedGame.isGamePaused == false)
        {
            ShootPrefab();
            //ShootRaycast();
       
            ammo--;

        }

        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextFireTime && ammo != 0 && fireMode == 1 && pausedGame.isGamePaused == false)
        {
            ShootPrefab();
            ammo--;
        }

        if (Input.GetKeyDown(KeyCode.R) && pausedGame.isGamePaused == false)
        {
            ammo = limitedAmmo;
        }

        if (ammo <= 0 && infintAmmo == true)
        {
            ammo = int.MaxValue;
        }
    }

    void ShootPrefab()
    {
        nextFireTime = Time.time + fireRate;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        shoot.Play();
        anim.Play("Recoil");

        Destroy(bullet, 3f);

        //Invoke("Delay", .08f);
    }

    void ShootRaycast()
    {
        nextFireTime = Time.time + fireRate;

        RaycastHit hit;

        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, weaponRange))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                Debug.Log("Hit!");
            }
        }
    }

    public void DestroyShooting()
    {
        Destroy(this);
    }


    void AutoAim()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            gunHolder.transform.LookAt(hit.transform.position);       
        }

    }
}
