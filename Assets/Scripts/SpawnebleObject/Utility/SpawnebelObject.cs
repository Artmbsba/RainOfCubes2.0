using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Timer))]
public abstract class SpawnebelObject : MonoBehaviour, IPoolable
{
    [SerializeField] protected float _minLifeTime = 2.0f;
    [SerializeField] protected float _maxLifeTime = 5.0f;

    protected Timer _timer;
    protected Rigidbody _rigidbody;

   public float LifeTime => Random.Range(_minLifeTime, _maxLifeTime);

    protected virtual void Awake()
    {
        _timer = GetComponent<Timer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    protected virtual void StartLifeCounter()
    {
        _timer.StartCounter(LifeTime);
    }

    protected abstract void CompleteLifeTime();

    public virtual void OnSpawn()
    {
        _timer.OnComplited += CompleteLifeTime;
    }

    public virtual void OnDespawn()
    {
        _timer.OnComplited -= CompleteLifeTime;
    }
}