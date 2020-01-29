using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Lean pool 사용
using Lean.Pool;

namespace WoosanStudio.ZombieShooter
{
    public class ObjectPoolFactory : MonoBehaviour , IObjectPoolFactory
    {
        public IObjectPool MakePool(Transform parent,GameObject prefab, Vector3 position , Quaternion rotation , int preload , int capacity)
        {
            GameObject objectPool = new GameObject("[ObjectPool] "+prefab.name);
            objectPool.transform.parent = parent;
            objectPool.transform.position = position;
            objectPool.transform.rotation = rotation;

            LeanGameObjectPool leanGameObjectPool = objectPool.AddComponent<LeanGameObjectPool>();

            //세부 세팅
            leanGameObjectPool.Prefab = prefab;
            leanGameObjectPool.Preload = preload;
            leanGameObjectPool.Capacity = capacity;
            leanGameObjectPool.Notification = LeanGameObjectPool.NotificationType.IPoolable;
            leanGameObjectPool.Recycle = true;
            leanGameObjectPool.Warnings = true;

            return (IObjectPool)leanGameObjectPool;
        }
    }
}
