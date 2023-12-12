using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableDirt : UnsanitaryObject
{
    public override void OnStart()
    {
        transform.rotation = Quaternion.Euler(90, Random.Range(0f,360f), 0);
    }
}
