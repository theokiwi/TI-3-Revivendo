using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    enum States {FOLLOW,FOLLOWINX};
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
}
