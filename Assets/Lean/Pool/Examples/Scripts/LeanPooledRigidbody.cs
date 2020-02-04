using UnityEngine;

namespace Lean.Pool.Examples
{
	/// <summary>This component allows you to reset a Rigidbody's velocity via Messages or via Poolable.</summary>
	[RequireComponent(typeof(Rigidbody))]
	[HelpURL(LeanPool.HelpUrlPrefix + "LeanPooledRigidbody")]
	[AddComponentMenu(LeanPool.ComponentPathPrefix + "Pooled Rigidbody")]
	public class LeanPooledRigidbody : MonoBehaviour, IPoolable
	{
		public void OnSpawn()
		{
            //Debug.Log("Hi OnSpawn");
		}

		public void OnDespawn()
		{
            //Debug.Log("Hi OnDespawn");

            var rigidbody = GetComponent<Rigidbody>();

			rigidbody.velocity        = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
		}
	}
}