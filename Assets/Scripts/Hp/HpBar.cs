using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    [SerializeField] private GameObject hpBar;

    public void EnableHpBar()
    {
        gameObject.SetActive(true);
    }

    public void DisableHpBar()
    {
        gameObject.SetActive(false);
    }

    public void SetHpBarValue(float hpPercent)
    {
        hpBar.transform.localScale = new Vector3(hpPercent, 1f, 1f);
    }
}
 