using UnityEngine;


public class Dish : AbstractInteractable
{
    public DishData dish;
    [SerializeField] private GameObject dirt;

    public override void Interact(){
        PickUp();
        PlayerRefac.Instance.rightClick = new Throw();
        Debug.Log(PlayerRefac.Instance.interaction.GetType());
    }

    public void OnCollisionEnter(Collision other){
        if(other.collider.CompareTag("Floor")){
            Debug.Log(dish);
            GameController.Instance.GetOrder(dish);
            Instantiate(dirt, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
