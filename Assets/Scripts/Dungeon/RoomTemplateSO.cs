using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Room_", menuName = "ScriptableObject/Dungeon/Room")]
public class RoomTemplateSO : ScriptableObject
{
    [HideInInspector] public string guid;

    public GameObject prefab;
    [HideInInspector] public GameObject previousPrefab;

    public RoomNodeTypeSO roomNodeType;
    public Vector2Int lowerBounds;
    public Vector2Int upperBounds;

    [SerializeField] public List<DoorWay> doorwayList;

    public Vector2Int[] spawnPositionArray;

    public List<SpawnableObjectsByLevel<EnemyDetailsSO>> enemiesByLevelList;

    public List<RoomEnemySpawnParameters> roomEnemySpawnParametersList;
    public List<DoorWay> GetDoorwayList()
    {
        return doorwayList;
    }

    #region 유효성검사
#if UNITY_EDITOR

    private void OnValidate()
    {
        if (guid == "" || previousPrefab != prefab)
        {
            guid = GUID.Generate().ToString();
            previousPrefab = prefab;
            EditorUtility.SetDirty(this);
        }
        HelperUtilities.ValidateCheckNullValue(this, nameof(prefab), prefab);
        HelperUtilities.ValidateCheckNullValue(this, nameof(roomNodeType), roomNodeType);

        HelperUtilities.ValidateCheckEnumerableValues(this, nameof(doorwayList), doorwayList);

        if(enemiesByLevelList.Count > 0 || roomEnemySpawnParametersList.Count > 0)
        {
            HelperUtilities.ValidateCheckEnumerableValues(this, nameof(enemiesByLevelList), enemiesByLevelList);
            HelperUtilities.ValidateCheckEnumerableValues(this, nameof(roomEnemySpawnParametersList), roomEnemySpawnParametersList);    

            foreach(RoomEnemySpawnParameters roomEnemySpawnParameters in roomEnemySpawnParametersList)
            {
                HelperUtilities.ValidateCheckNullValue(this, nameof(roomEnemySpawnParameters.dungeonLevel), roomEnemySpawnParameters.dungeonLevel);
                HelperUtilities.ValidateCheckPositiveRange(this, nameof(roomEnemySpawnParameters.minTotalEnemiesToSpawn),
                    roomEnemySpawnParameters.minTotalEnemiesToSpawn, nameof(roomEnemySpawnParameters.maxTotalEnemiesToSpawn),
                    roomEnemySpawnParameters.maxTotalEnemiesToSpawn, true);

                HelperUtilities.ValidateCheckPositiveRange(this, nameof(roomEnemySpawnParameters.minSpawnInterval),
                    roomEnemySpawnParameters.minSpawnInterval, nameof(roomEnemySpawnParameters.maxSpawnInterval),
                    roomEnemySpawnParameters.maxSpawnInterval, true);

                HelperUtilities.ValidateCheckPositiveRange(this, nameof(roomEnemySpawnParameters.minConcurrentEnemies),
                    roomEnemySpawnParameters.minConcurrentEnemies, nameof(roomEnemySpawnParameters.maxConcurrentEnemies),
                    roomEnemySpawnParameters.maxConcurrentEnemies, false);

                bool isEnemyTypesListForDungeonLevel = false;

                foreach (SpawnableObjectsByLevel<EnemyDetailsSO> dungeonObjectsByLevel in enemiesByLevelList)
                {
                    if (dungeonObjectsByLevel.dungeonLevel == roomEnemySpawnParameters.dungeonLevel &&
                        dungeonObjectsByLevel.spawnableObjectRatioList.Count > 0)
                        isEnemyTypesListForDungeonLevel = true;

                    HelperUtilities.ValidateCheckNullValue(this, nameof(dungeonObjectsByLevel.dungeonLevel), dungeonObjectsByLevel.dungeonLevel);

                    foreach (SpawnableObjectRatio<EnemyDetailsSO> dungeonObjectRatio in dungeonObjectsByLevel.spawnableObjectRatioList)
                    {
                        HelperUtilities.ValidateCheckNullValue(this, nameof(dungeonObjectRatio.dungeonObject), dungeonObjectRatio.dungeonObject);

                        HelperUtilities.ValidateCheckPositiveValue(this, nameof(dungeonObjectRatio.ratio), dungeonObjectRatio.ratio, false);
                    }
                }

                if (isEnemyTypesListForDungeonLevel == false && roomEnemySpawnParameters.dungeonLevel != null)
                {
                    Debug.Log("던전 레벨에 특정 적 유형이 없습니다." + roomEnemySpawnParameters.dungeonLevel.levelName + " " + this.name.ToString());
                }
            }
        }


        HelperUtilities.ValidateCheckEnumerableValues(this, nameof(spawnPositionArray), spawnPositionArray);
    }

#endif
    #endregion
}
