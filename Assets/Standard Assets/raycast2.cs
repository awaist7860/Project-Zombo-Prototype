using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class raycast2 : MonoBehaviour
{

    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;
    public float fireRate = 15f;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    private float nextTimeToFire = 0f;

    //Reloading variables
    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    public bool isReloading = false;
    public Animator animator;
    private string ammoText;
    public Text curAmmoText;
    public Text backupAmmoText;


    public int curAmmo;
    public int fullAmmo = 30;
    public int backupAmmo;
    public float thereloadTime = 3.0f;
    public float curReloadTime;


    void Start()
    {
        curAmmoText.text = curAmmo.ToString();
        backupAmmoText.text = backupAmmo.ToString();
    }

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("reloading", false);
    }

    // Update is called once per frame
    void Update()
    {

        if (isReloading)
        {
            return;
            
        }

        if (curAmmo <= 0)
        {
            Reload();
            return;
        }

        if (Input.GetKeyDown(KeyCode.R) && curAmmo < fullAmmo)
        {
            Reload();
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

    }

    //Coroutine
    void Reload()
    {
        var shot = fullAmmo - curAmmo;

        if (backupAmmo < shot)
        {
            curAmmo += backupAmmo;
            backupAmmo = 0;
        }
        else
        {
            curAmmo += shot;
            backupAmmo -= shot;
        }
    }

    void Shoot()
    {

        muzzleFlash.Play();

        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

        }

    }
}
