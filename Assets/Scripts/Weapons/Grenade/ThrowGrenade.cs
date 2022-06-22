using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowGrenade : MonoBehaviour
{

    public float throwForce = 500;

    public float timeBetweenGrenades = 3f;

    private float elapsed = 0f;

    public GameObject grenadePreFab;


    void Update()
    {
        elapsed += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.G) && elapsed >= timeBetweenGrenades)
        {
            Throw();
        }
    }

    public void Throw()
    {
        GameObject newGrenade = Instantiate(grenadePreFab, transform.position, transform.rotation);

        newGrenade.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce);
        
        elapsed = 0f;
    }
}
