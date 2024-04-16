using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperUtilities
{
    public static bool ValidateCheckEmptyString(Object thisObj, string fieldName, string stringToCheck)
    {
        if(stringToCheck == "")
        {
            Debug.Log(fieldName + "��(��) ����ִ� �����Դϴ�. ������Ʈ�� ���ԵǾ���մϴ�." + thisObj.name.ToString());
            return true;
        }
        return false;
    }

    public static bool ValidateCheckNullValue(Object thisObj, string fieldName, Object objToCheck)
    {
        if(objToCheck == null)
        {
            Debug.Log(fieldName + "�� ���� NULL���Դϴ�." + thisObj.name.ToString());
            return true;
        }
        return false;
    }

    public static bool ValidateCheckEnumerableValues(Object thisObj, string fieldName, IEnumerable enumerableObjectToCheck)
    {
        bool error = false;
        int count = 0;

        if(enumerableObjectToCheck == null)
        {
            Debug.Log(fieldName + "NULL ���� �ֽ��ϴ�." + thisObj.name.ToString());
            return true;
        }

        foreach(var item in enumerableObjectToCheck)
        {
            if (item == null)
            {
                Debug.Log(fieldName + " NULL���� ���ԵǾ��ֽ��ϴ�." + thisObj.name.ToString());
                error = true;
            }
            else
            {
                count++;
            }
        }

        if (count==0)
        {
            Debug.Log(fieldName + " ��� ���� ���ԵǾ������ʽ��ϴ�." + thisObj.name.ToString());
            error = true;
        }
        return error;
    }

    public static bool ValidateCheckPositiveValue(Object thisObj, string fieldName, int valueToCheck, bool isZeroAllowed)
    {
        bool error = false;

        if (isZeroAllowed)
        {
            if(valueToCheck < 0)
            {
                Debug.Log(fieldName + "��(��) �ݵ�� ��� �Ǵ� 0�� �����ؾ��մϴ�." + thisObj.name.ToString());
                error = true;
            }
        }
        else
        {
            if(valueToCheck <= 0)
            {
                Debug.Log(fieldName + "��(��) �ݵ�� ����� �����ؾ��մϴ�." + thisObj.name.ToString());
                error = true;
            }
        }

        return error;
    }

    public static Vector3 GetSpawnPositionNearestToPlayer(Vector3 playerPosition)
    {
        Room currentRoom = GameManager.Instance.GetCurrentRoom();

        Grid grid = currentRoom.instantiatedRoom.grid;

        Vector3 nearestSpawnPosition = new Vector3(10000f, 10000f, 0f);

        foreach(Vector2Int spawnPositionGrid in currentRoom.spawnPositionArray)
        {
            Vector3 spawnPositionWorld = grid.CellToWorld((Vector3Int)spawnPositionGrid);

            if(Vector3.Distance(spawnPositionWorld, playerPosition) < Vector3.Distance(nearestSpawnPosition, playerPosition))
            {
                nearestSpawnPosition = spawnPositionWorld;
            }
        }

        return nearestSpawnPosition;
    }
}
    