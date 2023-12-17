using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterMinigame : MonoBehaviour
{
    [SerializeField] Slider slider;
    public static CounterMinigame instance;
    [SerializeField] GameObject minigame;
    [SerializeField] KitchenChef chef;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        slider.maxValue = GameController.Instance.orders.Count;
        chef = FindAnyObjectByType<KitchenChef>();
    }
    public void Task()
    {
        slider.value += 1;
        if(slider.value == slider.maxValue)
        {
            for(int i = 0; i < slider.value; i++)
            {
                GameController.Instance.StartCooking(GameController.Instance.orders.Dequeue());
                chef.dishesCooking++;
            }
            chef.animator.SetBool("Cooking", true);
            Destroy(minigame);
            MiniGamesManager.instance.ExitMiniGame();
        }
    }
}
