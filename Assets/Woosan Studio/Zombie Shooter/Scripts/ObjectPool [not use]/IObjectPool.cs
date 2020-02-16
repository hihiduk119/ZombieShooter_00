using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public interface IObjectPool
    {
        GameObject ThisGameObject { get; set; }   
        List<GameObject> ObjectPool { get; set; }
        void Spawn();
        void DespawnOldest();
        void Despawn(GameObject clone, float t = 0.0f);
    }
}
