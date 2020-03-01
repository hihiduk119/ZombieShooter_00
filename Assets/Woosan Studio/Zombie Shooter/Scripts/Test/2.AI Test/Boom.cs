using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom
{
    private float _radius = 1.0F;
    private float _power = 20.0F;

    public Boom(Vector3 position)
    {
        Pow(position, _radius, _power);
    }

    public Boom(Vector3 position, float radius, float power)
    {
        Pow(position, radius, power);
    }

    public void Pow(Vector3 position,float radius, float power)
    {
        //Debug.Log("Pow");
        Vector3 explosionPos = position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(power, explosionPos, radius, 0.5F,ForceMode.VelocityChange);
            }
        }
    }
}
