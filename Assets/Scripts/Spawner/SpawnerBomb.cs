using UnityEngine;

public class SpawnerBomb : Spawner<Bomb>
{
    public void SpawnBomb(Vector3 position)
    {
        Bomb bomb = _pool.Get();

        bomb.LifeTimeEnded += OnBombLifeTimeEnded;

        bomb.transform.position = position;
    }

    private void OnBombLifeTimeEnded(Bomb bomb)
    {
        bomb.LifeTimeEnded -= OnBombLifeTimeEnded;

        _pool.Release(bomb);
    }
}