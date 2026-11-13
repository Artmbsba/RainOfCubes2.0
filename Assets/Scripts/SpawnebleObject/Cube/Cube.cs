using System;
using UnityEngine;

[RequireComponent(typeof(TouchDetector))]
[RequireComponent(typeof(Timer))]
[RequireComponent(typeof(ColorChanger))]
[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : SpawnebelObject
{
    [SerializeField] protected Color _color = Color.white;

    private ColorChanger _colorController;
    private TouchDetector _touchController;
    private Renderer _renderer;
    private bool _hasAlreadyBeenTouch;

    public event Action<Cube, Vector3> LifeTimeEnded;

    public Rigidbody GetRigidbody => _rigidbody;

    protected override void Awake()
    {
        base.Awake();

        _touchController = GetComponent<TouchDetector>();
        _renderer = GetComponent<Renderer>();
        _colorController = GetComponent<ColorChanger>();
    }

    public override void OnSpawn()
    {
        base.OnSpawn();

        _touchController.TouchHasOccurred += OnTouchOccurred;
        _colorController.SetDefaultColor(_renderer, _color);
        _hasAlreadyBeenTouch = false;
    }

    public override void OnDespawn()
    {
        base.OnDespawn();

        _touchController.TouchHasOccurred -= OnTouchOccurred;
    }

    protected override void CompleteLifeTime()
    {
        LifeTimeEnded?.Invoke(this, transform.position);
    }

    private void OnTouchOccurred()
    {
        if (_hasAlreadyBeenTouch)
            return;

        _hasAlreadyBeenTouch = true;
        _colorController.ChangColorOfCube(_renderer);

        StartLifeCounter();
    }
}