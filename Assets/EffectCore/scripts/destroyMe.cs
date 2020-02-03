using UnityEngine;
using System.Collections;

public class destroyMe : MonoBehaviour{

    float timer;
    public float deathtimer = 10;

    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = this.GetComponent<ParticleSystem>();
    }

    private void Reset()
    {
        timer = 0;
    }

    void Update ()
    {
        timer += Time.deltaTime;

        if (timer >= deathtimer)
        {
            //[Object Pool]
            //Destroy(gameObject);
            gameObject.SetActive(false);

            if (_particleSystem != null)
            {
                _particleSystem.Stop();
            }

            Reset();
        }
	
	}
}
