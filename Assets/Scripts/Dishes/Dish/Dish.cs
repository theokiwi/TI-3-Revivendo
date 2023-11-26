using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Dish : AbstractInteractable
{
    public DishData dish;
    [SerializeField] private GameObject dirt;

    public override void Interact(){
        PickUp();
    }

    public void OnCollisionEnter(Collision other){
        if(other.collider.CompareTag("Floor")){
            Debug.Log(dish);
            GameController.Instance.GetOrder(dish);
            Instantiate(dirt, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if(other.collider.CompareTag("Table")){
            Table table = other.collider.GetComponent<Table>();
            //table.Serve(transform);
        }
    }
}
