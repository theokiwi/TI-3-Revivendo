using UnityEngine;

public class TheBigBoundingBox : MonoBehaviour
{
    private void OnCollisionEnter(Collision other){
        if(other.transform.CompareTag("Dish")){
            GameController.Instance.GetOrder(other.transform.GetComponent<Dish>().dish);
        }
        if(other.transform.CompareTag("Client")){
            GameController.Instance.AddPoints(-100);
        }
        Destroy(other.gameObject);
    }
}
