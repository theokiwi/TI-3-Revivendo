using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : IAction
{
    public void Action(){
        GameObject throwObj = PlayerRefac.Instance.heldObject.gameObject;
        RaycastHit hitInfo = PlayerRefac.Instance.hitInfo;
        Rigidbody rb = throwObj.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.None;
        Vector3 throwDir = hitInfo.point - PlayerRefac.Instance.transform.position;
        throwObj.transform.SetParent(null);
        rb.useGravity = true;
        rb.AddForce(throwDir.normalized * PlayerRefac.Instance.throwForce, ForceMode.Impulse);
        PlayerRefac.Instance.heldObject = null;
        PlayerRefac.Instance.rightClick = null;
    }
}
