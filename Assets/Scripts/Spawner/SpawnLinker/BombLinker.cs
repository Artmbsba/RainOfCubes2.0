using UnityEngine;

public class BombLinker : SpawnLinker<Bomb>
{
    [SerializeField] private SpawnerBomb _spawnerBomb;

    protected override void Initialize()
    {
        Pool = _spawnerBomb.Pool;
    }
}