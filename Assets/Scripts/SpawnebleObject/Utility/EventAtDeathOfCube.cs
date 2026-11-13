using UnityEngine;

public class EventAtDeathOfCube: MonoBehaviour
{
    [SerializeField] private SpawnerCube _spawnerCube;
    [SerializeField] private SpawnerBomb _spawnerBomb;

    private void OnEnable()
    {
        _spawnerCube.CubeReturnedToPool += OnCubeLifeTimeEnded;
    }

    private void OnDisable()
    {
        _spawnerCube.CubeReturnedToPool -= OnCubeLifeTimeEnded;
    }

    private void OnCubeLifeTimeEnded(Vector3 deadPosition)
    {
        _spawnerBomb.SpawnBomb(deadPosition);
    }
}
