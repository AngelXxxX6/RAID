using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static Action EnemyDead;

    public static void OnEnemyDead()
    {
        EnemyDead?.Invoke();
    }
}
