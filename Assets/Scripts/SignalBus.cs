using System;
using UnityEngine;

public class SignalBus : MonoBehaviour
{
    public event Action<bool> EnemyInTargetStateChanged;
    public event Action<Vector3> PlayerKilled;
    
    public void InvokeEnemyInTargetStateChanged(bool isInTarget)
    {
        EnemyInTargetStateChanged?.Invoke(isInTarget);
    }

    public void InvokePlayerKilled(Vector3 killPosition)
    {
        PlayerKilled?.Invoke(killPosition);
    }
}
