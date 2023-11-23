using UnityEngine;

public class TrashCan : AbstractInteractable
{
    public override void Interact(){
        if(PlayerRefac.Instance.heldObject != null){
            if(PlayerRefac.Instance.heldObject.CompareTag("Dish")) GameController.Instance.GetOrder(PlayerRefac.Instance.heldObject.GetComponent<Dish>().dish);
            Destroy(PlayerRefac.Instance.heldObject.gameObject);
            PlayerRefac.Instance.heldObject = null;
        }
    }

    private void OnCollisionEnter(Collision other){
        if(other.transform.CompareTag("Dirt")){
            Destroy(other.gameObject);
            Debug.Log("Destroyed Dirt");
        }
        if(other.transform.CompareTag("Dish")){
            GameController.Instance.GetOrder(other.transform.GetComponent<Dish>().dish);
            Destroy(other.gameObject);
            Debug.Log("Destroyed Dish");
        }
    }
}
