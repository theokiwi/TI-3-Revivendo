using System.Linq;
using UnityEngine;

public class ClientSpawn : MonoBehaviour
{
    [SerializeField] private int remainingClients;
    [SerializeField] private GameObject client;
    [SerializeField] private int interval;
    [SerializeField] private float timeLeft;
    [SerializeField] private ITimer timer;
    [SerializeField] private LayerMask layerMask;


    private void Start(){
        timeLeft = 1;
        timer = new Timer_CountDown();
    }

    private void FixedUpdate(){
        if(!BoxCast()){
            if(timer.Count(timeLeft, interval)){
                SpawnClient();
                timeLeft = 1;
            }
        }
    }

    private bool BoxCast(){
        Collider[] inArea = Physics.OverlapBox(transform.position, Vector3.one/2, transform.rotation, layerMask);
        Debug.Log(inArea.Count());
        return inArea.Count() > 0;
    }

    private void SpawnClient(){
        if(remainingClients > 0){
            Instantiate(client,transform.position,transform.rotation);
            remainingClients--;
        }
    }
    
}
