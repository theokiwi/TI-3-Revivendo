using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : MonoBehaviour
{
    public bool occupied;
    [SerializeField] Vector3 boxCastSize;
    [SerializeField] float maxDistance = 300.0f;
    [SerializeField] LayerMask plateLayer;
    private bool hitDetect;
    private RaycastHit hit;

    private bool IsOccupied()
    {
        hitDetect = Physics.BoxCast(gameObject.transform.position, boxCastSize/2, Vector3.up, out hit, gameObject.transform.rotation, maxDistance, plateLayer);
        if(hitDetect)
        {
            Debug.Log(hit.collider.name);
        }
        return hitDetect;
    }
    
    
}
