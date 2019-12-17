using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{

    public GameObject theBullet;
    public Transform barrelEnd;

    public int bulletSpeed;
    public float despawnTime = 3.0f;

    public bool shootAble = true;
    public float waitBeforeNextShot = 0.25f;

    public int curAmmo;
    public int fullAmmo = 30;
    public int backupAmmo;
    public float reloadTime = 3.0f;
    public float curReloadTime;
    public bool isReloading;

    public Text curAmmoText;
    public Text backupAmmoText;

    public void Start()
    {
        curAmmo = 30;
        backupAmmo = 90;
    }

    private void Update()
    {
        curAmmoText.text = curAmmo.ToString();
        backupAmmoText.text = backupAmmo.ToString();

        if (Input.GetKey(KeyCode.Mouse0) && !isReloading && curAmmo > 0)
        {
            if (shootAble)
            {
                curAmmo--;
                shootAble = false;
                Shoot();
                StartCoroutine(ShootingYield());
            }
        }

        if (curAmmo <= 0)
        {
            Reload();
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading && backupAmmo > 0)
        {
            curReloadTime = reloadTime;
            isReloading = true;
            Reload();
        }

        if (isReloading)
        {
            curReloadTime -= Time.fixedDeltaTime;

            if (curReloadTime <= 0)
            {
                isReloading = false;
            }
        }
    }

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

    IEnumerator ShootingYield()
    {
        yield return new WaitForSeconds(waitBeforeNextShot);
        shootAble = true;
    }

    void Shoot()
    {
        var bullet = Instantiate(theBullet, barrelEnd.position, barrelEnd.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

        Destroy(bullet, despawnTime);
    }
}