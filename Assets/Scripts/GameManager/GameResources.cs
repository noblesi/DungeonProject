using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Tilemaps;

public class GameResources : MonoBehaviour
{
    private static GameResources instance;

    public static GameResources Instance
    {
        get
        {
            if(instance == null)
            {
                instance = Resources.Load<GameResources>("GameResources");
            }
            return instance;
        }
    }

    public RoomNodeTypeListSO roomNodeTypeList;

    public GameObject playerSelectionPrefab;

    public List<PlayerDetailsSO> playerDetailsList;

    public CurrentPlayerSO currentPlayer;

    public AudioMixerGroup soundsMasterMixerGroup;

    public SoundEffectSO doorOpenCloseSoundEffect;

    public Material dimmedMaterial;

    public Material litMaterial;

    public Shader variableLitShader;

    public TileBase[] enemyUnwalkableCollisionTilesArray;

    public TileBase preferredEnemyPathTile;

    public GameObject heartPrefab;

    public GameObject ammoIconPrefab;

    #region 유효성 검사
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullValue(this, nameof(roomNodeTypeList), roomNodeTypeList);
        HelperUtilities.ValidateCheckNullValue(this, nameof(currentPlayer), currentPlayer);
        HelperUtilities.ValidateCheckNullValue(this, nameof(doorOpenCloseSoundEffect), doorOpenCloseSoundEffect);
        HelperUtilities.ValidateCheckNullValue(this, nameof(litMaterial), litMaterial);
        HelperUtilities.ValidateCheckNullValue(this, nameof(dimmedMaterial), dimmedMaterial);
        HelperUtilities.ValidateCheckNullValue(this, nameof(variableLitShader), variableLitShader);
        HelperUtilities.ValidateCheckEnumerableValues(this, nameof(enemyUnwalkableCollisionTilesArray), enemyUnwalkableCollisionTilesArray);
        HelperUtilities.ValidateCheckNullValue(this, nameof(preferredEnemyPathTile), preferredEnemyPathTile);

        HelperUtilities.ValidateCheckNullValue(this, nameof(ammoIconPrefab), ammoIconPrefab);
    }
#endif
    #endregion
}
