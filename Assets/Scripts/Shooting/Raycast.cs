using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Raycast : MonoBehaviour
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
    public int ammoStash;
    public Text ammoStashText;
    public Text CurrentAmmoText;
    public FirstPersonController fpsController;
    private bool isRunning = false;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("reloading", false);
    }

    // Update is called once per frame
    void Update() {

        ammoStashText.text = ammoStash.ToString();
        CurrentAmmoText.text = currentAmmo.ToString();

        if (isReloading)
        {
            return;
        }

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool("melee", true);
            OnTriggerEnter();
        }
        else
        {
            animator.SetBool("melee", false);
        }

    }

    //Coroutine
    IEnumerator Reload()
    {

        if (ammoStash == 0)
        {
            animator.SetBool("reloading", false);
            isReloading = false;
        }
        isReloading = true;
        Debug.Log("Reloading");

        if (ammoStash <= 0 && currentAmmo == 0)
        {
            StopAllCoroutines();
            isReloading = false;
            animator.SetBool("reloading", false);
        }

        animator.SetBool("running", false);
        animator.SetBool("reloading", true);

        yield return new WaitForSeconds(reloadTime - 0.25f);
        animator.SetBool("reloading", false);
        yield return new WaitForSeconds(0.25f);

        var fullcombinedammo = maxAmmo - currentAmmo;

        if (ammoStash < fullcombinedammo)
        {
            currentAmmo += ammoStash;
            ammoStash = 0;
        }
        else
        {
            currentAmmo += fullcombinedammo;
            ammoStash -= fullcombinedammo;
        }

        isReloading = false;
    }


    public void Shoot()
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

    private void OnTriggerEnter()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Melee Attack");
        }
    }
}
