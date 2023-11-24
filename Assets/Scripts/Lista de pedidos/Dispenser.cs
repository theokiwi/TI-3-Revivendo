using UnityEngine;

public class Dispenser : MonoBehaviour
{
    [SerializeField] Vector3 boxCastSize;
    [SerializeField] float maxDistance = 300.0f;
    [SerializeField] LayerMask plateLayer;
    private bool hitDetect;
    private RaycastHit hit;

    private void OnDrawGizmos(){
        Gizmos.color = Color.green;
        if(IsOccupied())Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + (Vector3.up/4), boxCastSize);
    }

    public bool IsOccupied()
    {
        hitDetect = Physics.BoxCast(transform.position + (Vector3.up/4), boxCastSize/2, Vector3.up, out hit, transform.rotation, maxDistance, plateLayer);
        if(hitDetect)
        {
            Debug.Log(hit.collider.name);
        }
        return hitDetect;
    }
    
    
}
