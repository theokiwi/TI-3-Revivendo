using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMove : MonoBehaviour
{
    Vector2 v = new Vector2(-400, 0);
    public float speed;
    private void FixedUpdate()
    {
        transform.Translate(v * speed * Time.fixedDeltaTime);
    }
}
