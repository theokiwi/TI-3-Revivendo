using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new MenuSO", menuName = "Scriptable Objects/MenuSO")]
public class MenuSO : ScriptableObject
{
    public List<DishData> menu = new List<DishData>(); 
}
