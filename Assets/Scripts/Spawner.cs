using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube[] _prefabs;
    [SerializeField] private int _minSpawnCount = 2;
    [SerializeField] private int _maxSpawnCount = 6;

    private ObjectPool<Cube> _pool;
    private int _segmentationСhance = 0;

    private void Start()
    {
        _pool = new ObjectPool<Cube>(_prefabs, transform, 10);

        Cube cube = SpawnCube();
        cube.transform.position = new Vector3(0, 5, 0);
    }

    private void OnDestroyed(Cube cube)
    {
        if (Random.Range(0, _segmentationСhance) == 0)
        {
            int randomCount = Random.Range(_minSpawnCount, _maxSpawnCount);

            for (int i = 0; i < randomCount; i++)
            {
                Cube newCube = SpawnCube();
                newCube.transform.position = cube.transform.localPosition;
                newCube.transform.localScale = cube.transform.localScale / 2;

                StartCoroutine(newCube.Segmentation());
            }
        }

        cube.Explode();
        cube.Destroyed -= OnDestroyed;
        _pool.PutObject(cube);

        _segmentationСhance++;
    }

    private Cube SpawnCube()
    {
        Cube cube = _pool.GetObject();
        cube.Destroyed += OnDestroyed;

        return cube;
    }
}
