using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="DungeonLevel_", menuName ="ScriptableObject/Dungeon/DungeonLevel")]
public class DungeonLevelSO : ScriptableObject
{
    public string levelName;

    public List<RoomTemplateSO> roomTemplateList;
    public List<RoomNodeGraphSO> roomNodeGraphList;

    #region ��ȿ�� �˻�
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEmptyString(this, nameof(levelName), levelName);
        if (HelperUtilities.ValidateCheckEnumerableValues(this, nameof(roomTemplateList), roomTemplateList))
            return;

        if (HelperUtilities.ValidateCheckEnumerableValues(this, nameof(roomNodeGraphList), roomNodeGraphList))
            return;

        bool isCorridorEW = false;
        bool isCorridorNS = false;
        bool isEntrance = false;

        foreach(RoomTemplateSO roomTemplate in roomTemplateList)
        {
            if (roomTemplate == null)
                return;

            if (roomTemplate.roomNodeType.isCorridorEW)
                isCorridorEW = true;
            if (roomTemplate.roomNodeType.isCorridorNS)
                isCorridorNS = true;
            if (roomTemplate.roomNodeType.isEntrance)
                isEntrance = true;
        }

        if(isCorridorEW == false)
        {
            Debug.Log(this.name.ToString() + "���κ���Ÿ���� Ư������ �ʾҽ��ϴ�."); 
        }

        if(isCorridorNS == false)
        {
            Debug.Log(this.name.ToString() + "���κ���Ÿ���� Ư������ �ʾҽ��ϴ�.");
        }

        if(isEntrance == false)
        {
            Debug.Log(this.name.ToString() + "�Ա�Ÿ���� Ư������ �ʾҽ��ϴ�.");
        }

        foreach (RoomNodeGraphSO roomNodeGraph in roomNodeGraphList)
        {
            if (roomNodeGraph == null)
                return;

            foreach (RoomNodeSO roomNode in roomNodeGraph.roomNodeList)
            {
                if (roomNode == null) continue;

                if (roomNode.roomNodeType.isEntrance || roomNode.roomNodeType.isCorridorEW || roomNode.roomNodeType.isCorridorNS ||
                    roomNode.roomNodeType.isCorridor || roomNode.roomNodeType.isNone)
                    continue;

                bool isRoomNodeTypeFound = false;

                foreach(RoomTemplateSO roomTemplate in roomTemplateList)
                {
                    if (roomTemplate == null)
                        continue;

                    if(roomTemplate.roomNodeType == roomNode.roomNodeType)
                    {
                        isRoomNodeTypeFound = true;
                        break;
                    }
                }

                if (!isRoomNodeTypeFound)
                    Debug.Log(this.name.ToString() + "��(��) �����ϴ�.");
            }
        }
    }

#endif
    #endregion
}
