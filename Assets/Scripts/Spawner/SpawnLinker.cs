using UnityEngine;

public class SpawnLinker : MonoBehaviour
{
    [SerializeField] private SpawnerCube _spawnerCube;
    [SerializeField] private SpawnerBomb _spawnerBomb;

    [SerializeField] private ViewSpawnerCubes _viewSpawnerCubes;
    [SerializeField] private ViewSpawnerBombs _viewSpawnerBombs;

    private void Start()
    {
        _spawnerCube.CubeReturnedToPool += OnCubeLifeTimeEnded;

        _spawnerCube.Pool.AllCountChanged += _viewSpawnerCubes.UpdateAllCreated;
        _spawnerCube.Pool.ActiveCountChanged += _viewSpawnerCubes.UpdateActiveCount;
        _spawnerCube.Pool.CreatedCountChanged += _viewSpawnerCubes.UpdateCreatedCount;

        _spawnerBomb.Pool.AllCountChanged += _viewSpawnerBombs.UpdateAllCreated;
        _spawnerBomb.Pool.ActiveCountChanged += _viewSpawnerBombs.UpdateActiveCount;
        _spawnerBomb.Pool.CreatedCountChanged += _viewSpawnerBombs.UpdateCreatedCount;
    }

    private void OnDisable()
    {
        _spawnerCube.CubeReturnedToPool -= OnCubeLifeTimeEnded;

        _spawnerCube.Pool.AllCountChanged -= _viewSpawnerCubes.UpdateAllCreated;
        _spawnerCube.Pool.ActiveCountChanged -= _viewSpawnerCubes.UpdateActiveCount;
        _spawnerCube.Pool.CreatedCountChanged -= _viewSpawnerCubes.UpdateCreatedCount;

        _spawnerBomb.Pool.AllCountChanged -= _viewSpawnerBombs.UpdateAllCreated;
        _spawnerBomb.Pool.ActiveCountChanged -= _viewSpawnerBombs.UpdateActiveCount;
        _spawnerBomb.Pool.CreatedCountChanged -= _viewSpawnerBombs.UpdateCreatedCount;
    }

    private void OnCubeLifeTimeEnded(Vector3 deadPosition)
    {
        _spawnerBomb.SpawnBomb(deadPosition);
    }
}