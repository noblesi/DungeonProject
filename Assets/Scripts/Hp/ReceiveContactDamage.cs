using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Hp))]
[DisallowMultipleComponent]
public class ReceiveContactDamage : MonoBehaviour
{
    [SerializeField] private int contactDamageAmount;
    private Hp hp;

    private void Awake()
    {
        hp = GetComponent<Hp>();    
    }

    public void TakeContactDamage(int damageAmount = 0)
    {
       if(contactDamageAmount > 0)
            damageAmount = contactDamageAmount;

       hp.TakeDamage(damageAmount);
    }

    #region 유효성검사
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(contactDamageAmount), contactDamageAmount, true);
    }
#endif
    #endregion

}
