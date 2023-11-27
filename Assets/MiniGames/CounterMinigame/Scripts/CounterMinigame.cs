using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterMinigame : MonoBehaviour
{
    [SerializeField] Slider slider;
    public static CounterMinigame instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        slider.maxValue = GameController.Instance.orders.Count;
        for(int i = 0; i < slider.maxValue; i++)
        {
            foreach(DishData d in GameController.Instance.orders)
            {

            }
        }
    }
    public void Task()
    {
        slider.value += 1;
        if(slider.value == slider.maxValue)
        {
            for(int i = 0; i < slider.value; i++)
            {
                GameController.Instance.StartCooking(GameController.Instance.orders.Dequeue());
            }
        }
    }
}
