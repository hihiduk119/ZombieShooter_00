using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    private float radius = 2.0F;
    private float power = 50.0F;

    public void Pow()
    {
        Debug.Log("Pow");
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(power, explosionPos, radius, 1.0F,ForceMode.VelocityChange);
            }
        }
    }
}
