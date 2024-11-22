using NUnit.Compatibility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionOnDeath : MonoBehaviour
{
    public GameObject particlePrefab;

    void Awake()
    {
        var life = GetComponent<Life>();
        if (life != null )
        {
            life.onDeath.AddListener(OnDeath);
        }
    }

    void OnDeath()
    {
        Instantiate(particlePrefab, transform.position, transform.rotation);
    }
}
