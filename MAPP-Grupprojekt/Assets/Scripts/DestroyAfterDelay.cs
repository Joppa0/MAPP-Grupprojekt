using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    [SerializeField] private float delay;

    public void DestroyParticles()
    {
        //yield return new WaitForSeconds(delay);
        var em = GetComponent<ParticleSystem>().emission;
        em.rateOverTime = 0;
        Destroy(gameObject, delay);
    }
}
