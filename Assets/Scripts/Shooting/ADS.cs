using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADS : MonoBehaviour
{
    public Animator animator;
    public Camera fpsCamera;
    public int ADSzoom;
    public int defaultZoom;

    public GameObject gun1;



    private bool isScoped = false;

    void Update()
    {
        gun1.GetComponent<Raycast>();

        if (Input.GetButtonDown("Fire2"))
        {
            fpsCamera.fieldOfView = ADSzoom;
            //animator.SetBool("ads");
            if(gun1.gameObject.GetComponent<Raycast>().isReloading == true)
            {
                fpsCamera.fieldOfView = defaultZoom;
            }
            if(gun1.GetComponent<Raycast>().isReloading == false)
            {
                fpsCamera.fieldOfView = ADSzoom;
            }
        }
    }
}
