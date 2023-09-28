using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : IAction
{
    public void Action(){
        GameObject throwObj = PlayerRefac.Instance.heldObject.gameObject;
        Rigidbody rb = throwObj.GetComponent<Rigidbody>();
        Vector3 throwDir = PlayerRefac.Instance.hitInfo.point - PlayerRefac.Instance.transform.position;
        throwObj.transform.SetParent(null);
        rb.AddForce(throwDir.normalized * PlayerRefac.Instance.throwForce, ForceMode.Impulse);
        PlayerRefac.Instance.rightClick = null;
    }
}
