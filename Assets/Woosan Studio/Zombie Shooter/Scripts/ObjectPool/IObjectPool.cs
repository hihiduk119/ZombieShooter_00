using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public interface IObjectPoolFactory
    {
        IObjectPool MakePool(Transform parent, GameObject prefab, Vector3 position, Quaternion rotation, int preload, int capacity);
    }

    public interface IObjectPool
    {
        void Spawn();
        void DespawnOldest();
        void Despawn(GameObject clone, float t = 0.0f);
    }
}
