using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    enum States {FOLLOW,FOLLOWINX, FOLLOWINY};
    [SerializeField] States state;
    [SerializeField] float max;
    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (state == States.FOLLOW)
        {
            DoFollow();
        }
        else if (state == States.FOLLOWINX)
        {
            DoFollowX();
        }
        else if (state == States.FOLLOWINY)
        {
            DoFollowY();
        }
    }
    public void DoFollow()
    {
        transform.position = Input.mousePosition;
    }
    public void DoFollowX()
    {
        Vector2 v = new Vector2(Input.mousePosition.x, transform.position.y);
        transform.position = v;
    }
    public void DoFollowY()
    {
        Vector2 v = new Vector2(transform.position.x, Input.mousePosition.y);
        transform.position = v;
    }
}
