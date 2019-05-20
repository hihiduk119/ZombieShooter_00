using UnityEngine;
using UnityEngine.Events;

public interface IEnemy
{
    void Attack();
    void Dead();
    void Respawn();

    AttackEvent GetAttackEvent { get; }
}

public class AttackEvent : UnityEvent<float> {}