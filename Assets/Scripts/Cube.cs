using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    private float _minExplodeDelay = 1f;
    private float _maxExplodeDelay = 2f;

    public event Action<Cube> Destroyed;

    public IEnumerator Segmentation()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(_minExplodeDelay, _maxExplodeDelay));

        Destroyed?.Invoke(this);
    }

    public void Explode()
    {
        foreach (Rigidbody explodableObject in GetExplodableObjects())
            explodableObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
    }

    private List<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        List<Rigidbody> cubes = new();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                cubes.Add(hit.attachedRigidbody);

        return cubes;
    }
}
