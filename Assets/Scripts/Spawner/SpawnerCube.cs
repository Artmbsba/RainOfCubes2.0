using System;
using UnityEngine;
using System.Collections;

public class SpawnerCube : Spawner<Cube>
{
    [SerializeField] private Collider _mainPlatformCollaider;
    [SerializeField] private float _spawnHeight = 21;
    [SerializeField] private float _delay = 1;

    public event Action<Vector3> CubeReturnedToPool;

    private void Start()
    {
        StartCoroutine(DelaySpawn());
    }

    private IEnumerator DelaySpawn()
    {
        var wait = new WaitForSeconds(_delay);

        while (enabled)
        {
            yield return wait;

            PrepareCube();
        }
    }

    private void PrepareCube()
    {
        Cube cube = _pool.Get();

        cube.LifeTimeEnded += LifeTimeEnded;

        cube.transform.position = DefinePosition();
        cube.transform.rotation = Quaternion.identity;

        Rigidbody ridgidbody = cube.GetRigidbody;

        ridgidbody.linearVelocity = Vector3.zero;
        ridgidbody.angularVelocity = Vector3.zero;
    }

    private void LifeTimeEnded(Cube cube, Vector3 deadPosition)
    {
        cube.LifeTimeEnded -= LifeTimeEnded;

        _pool.Release(cube);

        CubeReturnedToPool?.Invoke(deadPosition);
    }

    private Vector3 DefinePosition()
    {
        Bounds bounds = _mainPlatformCollaider.bounds;

        float randomX = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
        float randomZ = UnityEngine.Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(randomX, bounds.min.y + _spawnHeight, randomZ);
    }
}