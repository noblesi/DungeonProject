using System;
using UnityEngine;

public class HPEvent : MonoBehaviour
{
    public event Action<HPEvent, HPEventArgs> OnHpChanged;

    public void CallHpChangedEvent(float hpPercent, int hp, int damage)
    {
        OnHpChanged?.Invoke(this, new HPEventArgs()
        {
            hpPercent = hpPercent,
            hp = hp,
            damage = damage
        });
    }
}

public class HPEventArgs : EventArgs
{
    public float hpPercent;
    public int hp;
    public int damage;
}
