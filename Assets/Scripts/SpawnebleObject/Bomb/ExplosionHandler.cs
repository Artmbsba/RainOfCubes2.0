using System.Collections.Generic;
using UnityEngine;

public class ExplosionHandler : MonoBehaviour
{
    [SerializeField] private float _explosionRadius = 25f;
    [SerializeField] private float _explosionForce = 50f;
    [SerializeField] private ParticleSystem _effect;
    [SerializeField] private float _effectLifeTime = 2f;

    public void Explode(Bomb bomb)
    {
        foreach (Rigidbody explodableObject in GetExplodableObjects(bomb))
            explodableObject.AddExplosionForce(_explosionForce, bomb.transform.position, _explosionRadius);

        ParticleSystem effect = Instantiate(_effect, bomb.transform.position, bomb.transform.rotation);

        Destroy(effect, _effectLifeTime);
    }

    private List<Rigidbody> GetExplodableObjects(Bomb bomb)
    {
        Collider[] hits = Physics.OverlapSphere(bomb.transform.position, _explosionRadius);

        List<Rigidbody> rigidbodies = new();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                rigidbodies.Add(hit.attachedRigidbody);

        return rigidbodies;
    }
}