public class KitchenChef : AbstractInteractable
{
    public override void Interact(){
        GameController.Instance.StartCooking(GameController.Instance.orders.Dequeue());
    }
}
