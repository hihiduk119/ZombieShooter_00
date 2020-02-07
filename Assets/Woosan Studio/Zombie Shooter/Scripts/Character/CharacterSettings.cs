using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    [CreateAssetMenu(menuName = "ZombieShooter/Character/Make Settings", fileName = "CharacterData")]
    public class CharacterSettings : ScriptableObject
    {
        [SerializeField] private float turnSpeed = 130f;
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float stoppingDistance =3f;
        [SerializeField] private bool useAi = false;


        public bool UseAi { get => useAi; }
        public float TurnSpeed { get => turnSpeed; }
        public float MoveSpeed { get => moveSpeed; }
        public float StopingDistance { get => stoppingDistance; }
    }
}