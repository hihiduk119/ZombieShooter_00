using UnityEngine;

namespace WoosanStudio.Extension
{
    public static class TransformExtension
    {
        public static Transform Reset(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
            return transform;
        }

        public static Transform Reset(this Transform transform , Vector3 position)
        {
            transform.localPosition = position;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
            return transform;
        }

        public static Transform Reset(this Transform transform, Quaternion rotation)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = rotation;
            transform.localScale = Vector3.one;
            return transform;
        }

        public static Transform Reset(this Transform transform, Vector3 position, Quaternion rotation)
        {
            transform.localPosition = position;
            transform.localRotation = rotation;
            transform.localScale = Vector3.one;
            return transform;
        }
    }
}
