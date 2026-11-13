using UnityEngine;

public class CubeLinker : SpawnLinker<Cube>
{
    [SerializeField] private SpawnerCube _spawnerCube;

    protected override void Initialize()
    {
        Pool = _spawnerCube.Pool;
    }
}