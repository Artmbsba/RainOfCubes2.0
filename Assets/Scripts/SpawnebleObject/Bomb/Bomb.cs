using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Timer))]
[RequireComponent(typeof(ExplosionHandler))]
[RequireComponent(typeof(Transparency))]
public class Bomb : SpawnebelObject
{
    [SerializeField] protected Color _color = Color.black;

    private ExplosionHandler _explosionHandler;
    private Transparency _transparency;

    public event Action<Bomb> LifeTimeEnded;

    protected override void Awake()
    {
        base.Awake();

        _explosionHandler = GetComponent<ExplosionHandler>();
        _transparency = GetComponent<Transparency>();
    }

    public override void OnSpawn()
    {
        base.OnSpawn();

        _transparency.FadeOut(LifeTime);
        StartLifeCounter();
    }

    public override void OnDespawn()
    {
        base.OnDespawn();

        _explosionHandler.Explode(this);
        _transparency.ResetTransparency();
    }

    protected override void CompleteLifeTime()
    {
        LifeTimeEnded?.Invoke(this);
    }
}