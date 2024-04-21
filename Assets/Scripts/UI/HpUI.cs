using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class HpUI : MonoBehaviour
{
    private List<GameObject> hpHeartsList = new List<GameObject>();

    private void OnEnable()
    {
        GameManager.Instance.GetPlayer().hpEvent.OnHpChanged += HpEvent_OnHpChanged;
    }

    private void OnDisable()
    {
        GameManager.Instance.GetPlayer().hpEvent.OnHpChanged -= HpEvent_OnHpChanged;
    }

    private void HpEvent_OnHpChanged(HPEvent hpEvent, HPEventArgs hpEventArgs)
    {
        SetHpBar(hpEventArgs);
    }

    private void ClearHpBar()
    {
        foreach(GameObject heartIcon in hpHeartsList)
        {
            Destroy(heartIcon);
        }

        hpHeartsList.Clear();
    }

    private void SetHpBar(HPEventArgs hpEventArgs)
    {
        ClearHpBar();

        int hpHearts = Mathf.CeilToInt(hpEventArgs.hpPercent * 100f / 20f);

        for(int i = 0; i < hpHearts; i++)
        {
            GameObject heart = Instantiate(GameResources.Instance.heartPrefab, transform);

            heart.GetComponent<RectTransform>().anchoredPosition = new Vector2(Settings.uiHeartSpacing * i, 0f);

            hpHeartsList.Add(heart);
        }
    }
}
