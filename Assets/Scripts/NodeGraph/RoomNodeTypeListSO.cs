using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="RoomNodeTypeListSO", menuName ="ScriptableObject/Dungeon/RoomNodeTypeList")]
public class RoomNodeTypeListSO : ScriptableObject
{
    public List<RoomNodeTypeSO> list;

    #region 유효성검사
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEnumerableValues(this, nameof(list), list);
    }
#endif
    #endregion
}
