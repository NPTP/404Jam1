using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shatter : MonoBehaviour
{
    public GameObject particle;

    public float particleSize = 0.1f;
    public int numparticles = 5;

    float particlesPivotDistance;
    Vector3 particlesPivot;

    public float explosionForce = 50f;
    public float explosionRadius = 4f;
    public float explosionUpward = 0.4f;

    // Use this for initialization
    void Start()
    {
        particlesPivotDistance = particleSize * numparticles / 2;
        particlesPivot = new Vector3(particlesPivotDistance, particlesPivotDistance, particlesPivotDistance);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Floor")
        {
            explode();
        }
    }

    public void explode()
    {
        gameObject.SetActive(false);
        System.Random rnd = new System.Random();

        for (int p = 0; p < numparticles; p++)
        {
            Instantiate(particle, transform.position, Quaternion.identity);
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);
                rb.angularVelocity = new Vector3(rnd.Next(0, 15), rnd.Next(0, 15), rnd.Next(0, 15));
            }
        }
    }
}