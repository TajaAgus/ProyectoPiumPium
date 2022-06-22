using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3;

    float countdown;

    public float radius = 5;

    public float explosionForce = 70;

    bool exploded = false;

    public GameObject explosionEffect;


    void Start()
    {
        countdown = delay;
    }


    void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0 && !exploded)
        {
            Explode();
            exploded = true;
        }
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius); // Crea un radio tipo esfera alrededor del objeto.

        foreach (var rangeObjects in colliders) // Recorre todos los objectos dentro del radio de colliders
        {
            Rigidbody rb = rangeObjects.GetComponent<Rigidbody>(); // trae los elementos que tengan un rigid body dentro de colliders

            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce * 10, transform.position, radius); // fuerza de explosion
            }
        }

        Destroy(gameObject);

        

    }
}
