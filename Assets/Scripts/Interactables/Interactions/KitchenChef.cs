public class KitchenChef : AbstractInteractable
{
    public override void Interact(){
        try{
            GameController.Instance.StartCooking(GameController.Instance.orders.Dequeue());
        }catch{}
    }
}
