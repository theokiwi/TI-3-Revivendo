using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper 
{
    
    public static Transform FindChildWithTag(GameObject parent, string tag)
    {
        Transform t = parent.transform;
        for(int i = 0; i < t.childCount; i++)
        {
            if(t.GetChild(i).CompareTag(tag))
            {
                return t.GetChild(i);
            }
        }
        return null;
    }
}
