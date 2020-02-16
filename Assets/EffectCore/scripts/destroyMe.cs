using UnityEngine;
using System.Collections;

//Lean pool 사용
using Lean.Pool;

public class destroyMe : MonoBehaviour{

    float timer;
    public float deathtimer = 2;

    //LeanGameObjectPool leanGameObjectPool;

    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = this.GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        //timer = 0;

        //transform.position = Vector3.zero;
        //transform.rotation = Quaternion.identity;

        //if (_particleSystem != null)
        //{
        //    _particleSystem.Clear();
        //    _particleSystem.Simulate(0.0f, true, true);
        //    _particleSystem.Play();
        //}
    }

    private void OnDisable()
    {
        Reset();
    }

    /// <summary>
    /// 파티클 재사용하기 위한 리/
    /// </summary>
    private void Reset()
    {
        timer = 0;

        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

        if (_particleSystem != null)
        {
            _particleSystem.Clear();
            _particleSystem.Simulate(0.0f, true, true);
            _particleSystem.Play();
        }

        gameObject.SetActive(false);
    }

    void Update ()
    {
        timer += Time.deltaTime;

        if (timer >= deathtimer)
        {
            //[Object Pool]
            //Destroy(gameObject);
            Reset();
        }
	
	}
}
