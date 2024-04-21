using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyDetails_", menuName ="ScriptableObject/Enemy/EnemyDetails")]
public class EnemyDetailsSO : ScriptableObject
{
    public string enemyName;
    public GameObject enemyPrefab;
    public float chaseDistance = 50f;

    public Material enemyStandardMaterial;
    public float enemyMaterializeTime;
    public Shader enemyMaterializeShader;
    [ColorUsage(true, true)]
    public Color enemyMaterializeColor;

    public WeaponDetailsSO enemyWeapon;
    public float firingIntervalMin = 0.1f;
    public float firingIntervalMax = 1f;
    public float firingDurationMin = 1f;
    public float firingDurationMax = 2f;
    public bool firingLineOfSightRequired;

    public EnemyHPDetail[] enemyHPDetailArray;
    public bool isImmuneAfterHit = false;
    public float hitImmunityTime;
    public bool isHpBarDisplayed = false;

    #region 유효성검사
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEmptyString(this, nameof(enemyName), enemyName);
        HelperUtilities.ValidateCheckNullValue(this, nameof(enemyPrefab), enemyPrefab);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(chaseDistance), chaseDistance, false);
        HelperUtilities.ValidateCheckNullValue(this, nameof(enemyStandardMaterial), enemyStandardMaterial);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(enemyMaterializeTime), enemyMaterializeTime, true);
        HelperUtilities.ValidateCheckNullValue(this, nameof(enemyMaterializeShader), enemyMaterializeShader);
        HelperUtilities.ValidateCheckPositiveRange(this, nameof(firingIntervalMin), firingIntervalMin, nameof(firingIntervalMax), firingIntervalMax, false);
        HelperUtilities.ValidateCheckPositiveRange(this, nameof(firingDurationMin), firingDurationMin, nameof(firingDurationMax), firingDurationMax, false);

        HelperUtilities.ValidateCheckEnumerableValues(this, nameof(enemyHPDetailArray), enemyHPDetailArray);
        if (isImmuneAfterHit)
        {
            HelperUtilities.ValidateCheckPositiveValue(this, nameof(hitImmunityTime), hitImmunityTime, false);  
        }
    }
#endif
    #endregion
}
