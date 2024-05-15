using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    [SerializeField] private float delay;

    // Destroys the particle system after the given delay.
    public void DestroyParticles()
    {
        // Stops spawning new particles.
        var em = GetComponent<ParticleSystem>().emission;
        em.rateOverTime = 0;

        Destroy(gameObject, delay);
    }
}
