using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderSpawner : MonoBehaviour
{
    [SerializeField] GameObject order;
    [SerializeField] GameObject canvas;
    private void Start()
    {
        Spawn();
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.D))
        {
            Spawn();
        }
    }
    public void Spawn()
    {
        foreach (DishData d in GameController.Instance.orders)
        {
            GameObject g = Instantiate(order);
            g.transform.SetParent(canvas.transform);
            g.transform.localPosition = new Vector3(3, -150, 0);
        }
    }
}
