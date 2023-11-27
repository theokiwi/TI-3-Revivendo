using UnityEngine;

public class TheBigBoundingBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dish"))
        {
            GameController.Instance.GetOrder(other.GetComponent<Dish>().dish);
        }
        if (other.CompareTag("Client"))
        {
            GameController.Instance.AddPoints(-100);
            GameController.Instance.LoseClient();
        }
        Destroy(other.gameObject);
    }
}
