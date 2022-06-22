using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GunSystem gunSystem = other.GetComponentInChildren<GunSystem>();
            if (!gunSystem.Maxbullets())
            {
                gunSystem.AddAmmo();
                Destroy(gameObject);
            }
        }
    }
}


