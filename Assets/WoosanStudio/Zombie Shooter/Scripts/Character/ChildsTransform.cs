using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 자식들의 모든 포지션을 가져오거나 세팅 할수 있음.
    /// </summary>
    public class ChildsTransform : MonoBehaviour
    {
        public List<Transform> GetTransforms()
        {
            List<Transform> transforms = new List<Transform>(GetComponentsInChildren<Transform>());

            return transforms;
        }

        public Queue<Vector3> GetAllPosition()
        {
            Transform[] allChildren = GetComponentsInChildren<Transform>();
            Queue<Vector3> positions = new Queue<Vector3>();

            foreach (Transform child in allChildren)
            {
                positions.Enqueue(child.localPosition);
            }

            return positions;
        }


        public void SetAllPosition(Queue<Vector3> positions)
        {
            Transform[] allChildren = GetComponentsInChildren<Transform>();
            foreach (Transform child in allChildren)
            {
                child.transform.localPosition = positions.Dequeue();
            }
        }


        public Queue<Quaternion> GetAllRotation()
        {
            Transform[] allChildren = GetComponentsInChildren<Transform>();
            Queue<Quaternion> rotations = new Queue<Quaternion>();

            foreach (Transform child in allChildren)
            {
                rotations.Enqueue(child.localRotation);
            }

            return rotations;
        }


        public void SetAllRotation(Queue<Quaternion> rotations)
        {
            Transform[] allChildren = GetComponentsInChildren<Transform>();
            foreach (Transform child in allChildren)
            {
                child.transform.localRotation = rotations.Dequeue();
            }
        }
    }
}
