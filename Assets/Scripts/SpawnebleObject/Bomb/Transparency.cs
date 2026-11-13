using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Transparency : MonoBehaviour
{
    [SerializeField] private float _minAlpha = 0f;

    private float _maxAlpha = 1f;
    private Renderer _renderer;
    private Material _material;
    private Color _defaultColor;

    private Coroutine _coroutine;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _material = _renderer.material;
        _defaultColor = _material.GetColor("_BaseColor");

        SetupTransparentMode();
    }

    private void SetupTransparentMode()
    {
        _material.SetFloat("_Surface", 1.0f);
        _material.SetFloat("_Blend", 0.0f);

        _material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        _material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        _material.SetInt("_ZWrite", 0);

        _material.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");

        _material.DisableKeyword("_ALPHATEST_ON");
        _material.DisableKeyword("_ALPHAPREMULTIPLY_ON");

        _material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
    }

    public void FadeOut(float duration)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(AnumationAlpha(duration));
    }

    public void ResetTransparency()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        Color color = _defaultColor;
        color.a = 1f;
        _material.SetColor("_BaseColor", color);
    }

    private IEnumerator AnumationAlpha(float duration)
    {
        float elapsed = 0f;
        Color color = _material.GetColor("_BaseColor");
        _material.SetColor("_BaseColor", color);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float speed = elapsed / duration;

            color.a = Mathf.MoveTowards(_maxAlpha, _minAlpha, speed);
            _material.SetColor("_BaseColor", color);

            yield return null;
        }
    }
}