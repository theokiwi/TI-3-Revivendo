using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject arrow;
    public GameObject bow;
    public GameObject chargedArrow;
    public GameObject border;


    private void Start()
    {
        Recharge();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && chargedArrow != null)
        {
            Shoot();
            Invoke("Recharge", 1.5f);
        }
    }
    public void Recharge()
    {
        GameObject g = Instantiate(arrow);
        g.transform.SetParent(bow.transform);
        g.GetComponent<LinearMove>().enabled = false;
        g.transform.localPosition = bow.transform.localPosition;
        chargedArrow = g;
    }
    public void Shoot()
    {
        chargedArrow.GetComponent<LinearMove>().enabled = true;
        chargedArrow.transform.parent = border.transform;
        chargedArrow = null;
    }
}
