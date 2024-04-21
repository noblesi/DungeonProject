using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

#region 필수컴포넌트
[RequireComponent(typeof(HPEvent))]
[RequireComponent(typeof(Hp))]
[RequireComponent(typeof(DealContactDamage))]
[RequireComponent(typeof(DestroyedEvent))]
[RequireComponent(typeof(Destroyed))]
[RequireComponent(typeof(EnemyWeaponAI))]
[RequireComponent(typeof(AimWeaponEvent))]
[RequireComponent(typeof(AimWeapon))]
[RequireComponent(typeof(FireWeaponEvent))]
[RequireComponent(typeof(FireWeapon))]
[RequireComponent(typeof(SetActiveWeaponEvent))]
[RequireComponent(typeof(ActiveWeapon))]
[RequireComponent(typeof(WeaponFiredEvent))]
[RequireComponent(typeof(ReloadWeaponEvent))]
[RequireComponent(typeof(ReloadWeapon))]
[RequireComponent(typeof(WeaponReloadedEvent))]
[RequireComponent(typeof(EnemyMovementAI))]
[RequireComponent(typeof(MovementToPositionEvent))]
[RequireComponent(typeof(MovementToPosition))]
[RequireComponent(typeof(IdleEvent))]
[RequireComponent(typeof(Idle))]
[RequireComponent(typeof(AnimateEnemy))]
[RequireComponent(typeof(MaterializeEffect))]
[RequireComponent(typeof(SortingGroup))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(PolygonCollider2D))]
#endregion
[DisallowMultipleComponent]
public class Enemy : MonoBehaviour
{
    [HideInInspector] public EnemyDetailsSO enemyDetails;
    private HPEvent hpEvent;
    private Hp hp;
    [HideInInspector] public AimWeaponEvent aimWeaponEvent;
    [HideInInspector] public FireWeaponEvent fireWeaponEvent;
    private FireWeapon fireWeapon;
    private SetActiveWeaponEvent setActiveWeaponEvent;
    private EnemyMovementAI enemyMovementAI;
    [HideInInspector] public MovementToPositionEvent movementToPositionEvent;
    [HideInInspector] public IdleEvent idleEvent;
    private MaterializeEffect materializeEffect;
    private CircleCollider2D circleCollider2D;
    private PolygonCollider2D polygonCollider2D;
    [HideInInspector] public SpriteRenderer[] spriteRendererArray;
    [HideInInspector] public Animator animator;

    private void Awake()
    {
        hpEvent = GetComponent<HPEvent>();
        hp = GetComponent<Hp>();    
        aimWeaponEvent = GetComponent<AimWeaponEvent>();
        fireWeapon = GetComponent<FireWeapon>();
        fireWeaponEvent = GetComponent<FireWeaponEvent>();
        setActiveWeaponEvent = GetComponent<SetActiveWeaponEvent>();
        enemyMovementAI = GetComponent<EnemyMovementAI>(); 
        movementToPositionEvent = GetComponent<MovementToPositionEvent>();
        idleEvent = GetComponent<IdleEvent>();
        materializeEffect = GetComponent<MaterializeEffect>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        spriteRendererArray = GetComponentsInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        hpEvent.OnHpChanged += HpEvent_OnHpLost;
    }

    private void OnDisable()
    {
        hpEvent.OnHpChanged -= HpEvent_OnHpLost;
    }

    private void HpEvent_OnHpLost(HPEvent hpEvent, HPEventArgs hpEventArgs)
    {
        if(hpEventArgs.hp <= 0)
        {
            EnemyDestroyed();
        }
    }

    private void EnemyDestroyed()
    {
        DestroyedEvent destroyedEvent = GetComponent<DestroyedEvent>();
        destroyedEvent.CallDestroyedEvent(false, hp.GetStartingHp());
    }

    public void InitializeEnemy(EnemyDetailsSO enemyDetails, int enemySpawnNumber, DungeonLevelSO dungeonLevel)
    {
        this.enemyDetails = enemyDetails;

        SetEnemyMovementUpdateFrame(enemySpawnNumber);

        SetEnemyStartingHp(dungeonLevel);

        SetEnemyStartingWeapon();

        SetEnemyAnimationSpeed();

        StartCoroutine(MaterializeEnemy());
    }

    private void SetEnemyMovementUpdateFrame(int enemySpawnNumber)
    {
        enemyMovementAI.SetUpdateFrameNumber(enemySpawnNumber % Settings.targetFrameRateToSpreadPathfindingOver);
    }

    private void SetEnemyStartingHp(DungeonLevelSO dungeonLevel)
    {
        foreach(EnemyHPDetail enemyHpDetails in enemyDetails.enemyHPDetailArray)
        {
            if(enemyHpDetails.dungeonLevel == dungeonLevel)
            {
                hp.SetStartingHp(enemyHpDetails.enemyHP);
                return;

            }
        }
        hp.SetStartingHp(Settings.defaultEnemyHP);
    }

    private void SetEnemyStartingWeapon()
    {
        if(enemyDetails.enemyWeapon != null)
        {
            Weapon weapon = new Weapon()
            {
                weaponDetails = enemyDetails.enemyWeapon,
                weaponReloadTimer = 0f,
                weaponClipRemainingAmmo = enemyDetails.enemyWeapon.weaponClipAmmoCapacity,
                weaponRemainingAmmo = enemyDetails.enemyWeapon.weaponAmmoCapacity,
                isWeaponReloading = false
            };

            setActiveWeaponEvent.CallSetActiveWeaponEvent(weapon);  
        }
    }

    private void SetEnemyAnimationSpeed()
    {
        animator.speed = enemyMovementAI.moveSpeed / Settings.baseSpeedForEnemyAnimations;
    }

    private IEnumerator MaterializeEnemy()
    {
        EnemyEnable(false);

        yield return StartCoroutine(materializeEffect.MaterializeRoutine(enemyDetails.enemyMaterializeShader, enemyDetails.enemyMaterializeColor,
            enemyDetails.enemyMaterializeTime, spriteRendererArray, enemyDetails.enemyStandardMaterial));

        EnemyEnable(true);
    }

    private void EnemyEnable(bool isEnabled)
    {
        circleCollider2D.enabled = isEnabled;
        polygonCollider2D.enabled = isEnabled;

        enemyMovementAI.enabled = isEnabled;

        fireWeapon.enabled = isEnabled;
    }
}
