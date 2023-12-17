using System.Linq;
using UnityEngine;

public class Dispenser : MonoBehaviour
{
    [SerializeField] Vector3 boxCastSize;
    [SerializeField] float maxDistance ;
    [SerializeField] LayerMask plateLayer;

    private void FixedUpdate(){
        IsOccupied();
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.green;
        if(IsOccupied())Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + (Vector3.up/4), boxCastSize);
    }

    public bool IsOccupied()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position + Vector3.up/4, boxCastSize/2, transform.rotation, plateLayer);   
        return colliders.Count() > 0;
    }
    
    
}
