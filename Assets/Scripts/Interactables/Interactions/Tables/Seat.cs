using UnityEngine;

public class Seat : MonoBehaviour
{
    public bool occupied;
    public Client clientSeated;
    public Transform seatPos;


    public void Start(){
        if(seatPos == null) seatPos = transform;
    }
}
