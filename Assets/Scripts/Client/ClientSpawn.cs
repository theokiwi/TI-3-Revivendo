using System.Linq;
using UnityEngine;

public class ClientSpawn : MonoBehaviour
{
    [SerializeField] public GameObject client;
    [SerializeField] private int interval;
    [SerializeField] private float timeLeft;
    [SerializeField] private ITimer timer;
    [SerializeField] private LayerMask layerMask;
    public int maxClients = 1000;
    //public int clientsBeingServed = 0;

    private void Start(){
        StartDay();
    }
    public void StartDay()
    {
        timeLeft = 0;
        timer = new Timer_CountDown();
    }

    private void FixedUpdate(){
        if(!IsOccupied()){
            if(timer.Count(ref timeLeft, interval)){
                SpawnClient();
                timeLeft = 1;
                Debug.Log("New Client");
            }
        }
    }

    private void OnDrawGizmos() {
        if (IsOccupied()) Gizmos.color = Color.red;
        else Gizmos.color = Color.cyan; 
        Vector3 boxSize = new Vector3 (2, 2, 2);
        Gizmos.DrawWireCube(transform.position + Vector3.up, boxSize);    
    }

    private bool IsOccupied(){
        Vector3 boxSize = new Vector3(2, 2, 2);
        Collider [] inArea = Physics.OverlapBox (transform.position + Vector3.up, boxSize/2, transform.rotation, layerMask);
        foreach(Collider data in inArea){
            if(data.CompareTag("Client")) return true;
        }
        return false;
    }

    private void SpawnClient(){
        //clientsBeingServed++;
        Instantiate(client,transform.position,transform.rotation);
    }
    
}
