using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticaleDestroy : MonoBehaviour
{

    private ParticleSystem _particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_particleSystem)
        {
            if(!_particleSystem.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}
