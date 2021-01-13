using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

namespace WoosanStudio.ZombieShooter.NavMesh
{
	/// <summary>
    /// 내비메쉬에 레이를 쏘고 위치 확인
    /// </summary>
    public class TestMeshHit : MonoBehaviour
    {
		//public GameObject SpherePrefab;

		public NavMeshData navMeshData;

		public float range = 10.0f;

        private void Start()
        {
			Bounds bounds = navMeshData.sourceBounds;

		}

        bool RandomPoint(Vector3 center, float range, out Vector3 result)
		{
			for (int i = 0; i < 30; i++)
			{
				Vector3 randomPoint = center + Random.insideUnitSphere * range;
				NavMeshHit hit;

				//자신의 위치에서 바닦으로 레이를 체크.
				//메쉬가 나비메쉬가 있으면 hit에 반환
				if (UnityEngine.AI.NavMesh.SamplePosition(this.transform.position, out hit, 10.0f, UnityEngine.AI.NavMesh.AllAreas))
				{
					result = hit.position;

					Debug.Log("hit.position = " + hit.position.ToString());

					//GameObject clone = Instantiate(SpherePrefab) as GameObject;
					//SpherePrefab.transform.position = hit.position;

					return true;
				}
			}
			result = Vector3.zero;
			return false;
		}
		void Update()
		{
			Vector3 point;
			if (RandomPoint(transform.position, range, out point))	
			{
				Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);

				
			}
		}
	}
}
