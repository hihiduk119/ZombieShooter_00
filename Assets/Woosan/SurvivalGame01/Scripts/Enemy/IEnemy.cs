using UnityEngine;
using UnityEngine.Events;

public interface IEnemy
{
    void Attack();
    void Dead();
    void Respawn();

    AttackEvent AttackEvent { get; }
}

public class AttackEvent : UnityEvent<float> {}