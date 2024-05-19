using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    private float _minExplodeDelay = 1f;
    private float _maxExplodeDelay = 2f;
    private Renderer _renderer;

    public float SplitChance { get; set; } = 100f;

    public event Action<Cube> Destroyed;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public IEnumerator Segmentation()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(_minExplodeDelay, _maxExplodeDelay));

        Destroyed?.Invoke(this);
    }

    public void Explode(List<Cube> explodableCubes)
    {
        foreach (Cube cube in explodableCubes)
            cube.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
    }

    public void SetRandomColor()
    {
        float channelRed = UnityEngine.Random.Range(0f, 1f);
        float channelGreen = UnityEngine.Random.Range(0f, 1f);
        float channelBlue = UnityEngine.Random.Range(0f, 1f);

        _renderer.material.color = new Color(channelRed, channelGreen, channelBlue);
    }
}
