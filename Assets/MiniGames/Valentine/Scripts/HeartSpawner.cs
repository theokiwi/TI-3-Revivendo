using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartSpawner : MonoBehaviour
{
    public GameObject heart;
    public Sprite[] heartSprite;
    public GameObject dad;

    private void Start()
    {
        InvokeRepeating("Spawn", 0, 0.3f);
    }
    public void Spawn()
    {
        GameObject g = Instantiate(heart);
        g.transform.SetParent(dad.transform);
        g.transform.localPosition = new Vector2(Random.RandomRange(-170f, 370f), -477);
        g.gameObject.GetComponent<Image>().sprite = heartSprite[Random.Range(0, heartSprite.Length)];
    }
}
