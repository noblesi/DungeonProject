using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HPEvent))]
[DisallowMultipleComponent]
public class Hp : MonoBehaviour
{
    [SerializeField] private HpBar hpBar;
    private int startingHp;
    private int currentHp;
    private HPEvent hpEvent;
    private Player player;
    private Coroutine immunityCoroutine;
    private bool isImmunityAfterHit = false;
    private float immunityTime = 0f;
    private SpriteRenderer spriteRenderer = null;
    private const float spriteFlashInterval = 0.2f;
    private WaitForSeconds waitForSecondsSpriteFlashInterval = new WaitForSeconds(spriteFlashInterval);

    [HideInInspector] public bool isDamageable = true;
    [HideInInspector] public Enemy enemy;

    private void Awake()
    {
        hpEvent = GetComponent<HPEvent>();
    }
    private void Start()
    {
        CallHpEvent(0);

        player = GetComponent<Player>();
        enemy = GetComponent<Enemy>();

        if(player != null )
        {
            if(player.playerDetails.isImmuneAfterHit)
            {
                isImmunityAfterHit = true;
                immunityTime = player.playerDetails.hitImmunityTime;
                spriteRenderer = player.spriteRenderer;
            }
        }
        else if(enemy != null)
        {
            if(enemy.enemyDetails.isImmuneAfterHit)
            {
                isImmunityAfterHit = true;
                immunityTime = enemy.enemyDetails.hitImmunityTime;
                spriteRenderer = enemy.spriteRendererArray[0];
            }
        }

        if (enemy != null && enemy.enemyDetails.isHpBarDisplayed == true && hpBar != null)
        {
            hpBar.EnableHpBar();
        }
        else if (hpBar != null)
        {
            hpBar.DisableHpBar();
        }
    }

    public void TakeDamage(int damage)
    {
        bool isRolling = false;

        if(player != null)
            isRolling = player.playerControl.isPlayerRolling;

        if(isDamageable && !isRolling)
        {
            currentHp -= damage;
            CallHpEvent(damage);

            PostHitImmunity();

            if (hpBar != null)
            {
                hpBar.SetHpBarValue((float)currentHp / (float)startingHp);
            }
        }
    }

    private void PostHitImmunity()
    {
        if (gameObject.activeSelf == false)
            return;

        if (isImmunityAfterHit)
        {
            if(immunityCoroutine != null)
                StopCoroutine(immunityCoroutine);

            immunityCoroutine = StartCoroutine(PostHitImmunityRoutine(immunityTime, spriteRenderer));
        }
    }

    private IEnumerator PostHitImmunityRoutine(float immunityTime,  SpriteRenderer spriteRenderer)
    {
        int iterations = Mathf.RoundToInt(immunityTime / spriteFlashInterval / 2f);

        isDamageable = false;

        while(iterations > 0)
        {
            spriteRenderer.color = Color.red;

            yield return waitForSecondsSpriteFlashInterval;

            spriteRenderer.color = Color.white;

            yield return waitForSecondsSpriteFlashInterval;

            iterations--;

            yield return null;
        }
        isDamageable = true;
    }

    private void CallHpEvent(int damage)
    {
        hpEvent.CallHpChangedEvent(((float)currentHp / (float)startingHp), currentHp, damage);
    }

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
