using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMove : MonoBehaviour
{
    public Vector2 v = new Vector2(-400, 0);
    public float speed;
    private void Start()
    {
        Destroy(gameObject, 4f);
    }
    private void FixedUpdate()
    {
        transform.Translate(v * speed * Time.fixedDeltaTime);
    }
}
