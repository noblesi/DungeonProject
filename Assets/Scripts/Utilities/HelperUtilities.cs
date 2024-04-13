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

    public static bool ValidateCheckEnumerableValues(Object thisObj, string fieldName, IEnumerable enumerableObjectToCheck)
    {
        bool error = false;
        int count = 0;

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
}
    