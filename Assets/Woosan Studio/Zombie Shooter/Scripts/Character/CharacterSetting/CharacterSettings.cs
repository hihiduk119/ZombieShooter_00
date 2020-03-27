using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 몬캐릭터의 네비메쉬 관련 이동 및 정지거리 등의 셋업 값.
    /// </summary>
    [CreateAssetMenu(menuName = "ZombieShooter/Character/Make Settings", fileName = "CharacterData")]
    public class CharacterSettings : ScriptableObject
    {
        [SerializeField] private float turnSpeed = 130f;
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float stoppingDistance = 3f;
        [SerializeField] private bool useAi = false;

        public bool UseAi { get => useAi; }
        public float TurnSpeed { get => turnSpeed; }
        public float MoveSpeed { get => moveSpeed; }
        public float StopingDistance { get => stoppingDistance; }
    }
}