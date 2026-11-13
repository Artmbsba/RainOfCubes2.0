using UnityEngine;

public abstract class SpawnLinker<T> : MonoBehaviour where T : Component
{
    [SerializeField] private ViewSpawner _viewSpawner;

    protected GenericPool<T> Pool;

    protected void Start()
    {
        Initialize();

        Pool.AllCountChanged += _viewSpawner.UpdateAllCreated;
        Pool.ActiveCountChanged += _viewSpawner.UpdateActiveCount;
        Pool.CreatedCountChanged += _viewSpawner.UpdateCreatedCount;
    }

    protected void OnDisable()
    {
        Pool.AllCountChanged -= _viewSpawner.UpdateAllCreated;
        Pool.ActiveCountChanged -= _viewSpawner.UpdateActiveCount;
        Pool.CreatedCountChanged -= _viewSpawner.UpdateCreatedCount;
    }

    protected abstract void Initialize();
}