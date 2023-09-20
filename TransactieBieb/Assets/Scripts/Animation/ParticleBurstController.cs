using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBurstController : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] particleSystems;
    [SerializeField] private Vector2 randomTimeMinMax;
    private float timer;

    private void Start()
    {
        timer = 0;
    }

    private void Update()
    {
        if(timer <= 0)
        {
            particleSystems[Random.Range(0, particleSystems.Length)].Emit(1);
            timer = Random.Range(randomTimeMinMax.x, randomTimeMinMax.y);
        }

        timer -= Time.deltaTime;
    }

}
