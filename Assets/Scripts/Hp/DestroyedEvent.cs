using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class DestroyedEvent : MonoBehaviour
{
    public event Action<DestroyedEvent, DestroyedEventArgs> OnDestroyed;

    public void CallDestroyedEvent(bool playerDead, int points)
    {
        OnDestroyed?.Invoke(this, new DestroyedEventArgs() { playerDead = playerDead, points = points});
    }
}

public class DestroyedEventArgs : EventArgs
{
    public bool playerDead;
    public int points;
}

