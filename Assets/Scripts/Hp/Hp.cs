using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Hp : MonoBehaviour
{
    private int startingHp;
    private int currentHp;

    public void SetStartingHp(int startingHp)
    {
        this.startingHp = startingHp;
        currentHp = startingHp;
    }

    public int GetStartingHp()
    {
        return startingHp;
    }
}
