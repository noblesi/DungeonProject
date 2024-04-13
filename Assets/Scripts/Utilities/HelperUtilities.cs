using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperUtilities
{
    public static bool ValidateCheckEmptyString(Object thisObj, string fieldName, string stringToCheck)
    {
        if(stringToCheck == "")
        {
            Debug.Log(fieldName + "이(가) 비어있는 상태입니다. 오브젝트에 포함되어야합니다." + thisObj.name.ToString());
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
                Debug.Log(fieldName + " NULL값이 포함되어있습니다." + thisObj.name.ToString());
                error = true;
            }
            else
            {
                count++;
            }
        }

        if (count==0)
        {
            Debug.Log(fieldName + " 어떠한 값도 포함되어있지않습니다." + thisObj.name.ToString());
            error = true;
        }
        return error;
    }
}
    