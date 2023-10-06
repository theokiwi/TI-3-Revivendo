using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new ClientSO", menuName = "Scriptable Objects/ClientSO") ]
public class ClientSO : ScriptableObject
{
    public DishData[] menu;
}
