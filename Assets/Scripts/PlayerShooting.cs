using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerShooting : MonoBehaviour
{
    public GameObject Bullet1;
    public GameObject Bullet2;
    public GameObject shootPoint;

    public float fireRate;
    public int bulletsAmount;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnFire(InputValue value)
    {   
        animator.SetBool("Shooting", value.isPressed);

        if (value.isPressed)
        {
            InvokeRepeating("Shoot1", fireRate, fireRate);
        }
        else
        {
            CancelInvoke();
        }
    }

    public void OnFire2(InputValue value)
    {
        if (value.isPressed)
        {
            InvokeRepeating("Shoot2", fireRate, fireRate);
        }
        else
        {
            CancelInvoke();
        }
    }


    private void Shoot1()
    {
        if (Time.timeScale > 0)
        {
            GameObject clone = Instantiate(Bullet1);

            clone.transform.position = shootPoint.transform.position;
            clone.transform.rotation = shootPoint.transform.rotation;
        }
    }

    private void Shoot2()
    {
        if (Time.timeScale > 0)
        {
            GameObject clone = Instantiate(Bullet2);

            clone.transform.position = shootPoint.transform.position;
            clone.transform.rotation = shootPoint.transform.rotation;
        }
    }

}
