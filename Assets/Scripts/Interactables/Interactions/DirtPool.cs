using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtPool : MonoBehaviour
{
    [SerializeField] private float cleaningTime;
    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            if(PlayerRefac.Instance.heldObject.GetType() == typeof(Broom)){
                transform.localScale -= Vector3.one/cleaningTime * Time.deltaTime;
                if(transform.localScale.x <= 0.2) Destroy(gameObject);
            }
        }
    }
}
