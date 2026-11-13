using System;
using UnityEngine;

public class Spawner<T>: MonoBehaviour where T : Component
{
    [SerializeField] protected T _prefab;
    [SerializeField] protected int _poolCapacity = 10;
    [SerializeField] protected int _poolMaxSize = 10;

    protected GenericPool<T> _pool;

    public GenericPool<T> Pool => _pool;

    protected void Awake()
    {
        _pool = new GenericPool<T>(_prefab, _poolCapacity, _poolMaxSize);
    }
}