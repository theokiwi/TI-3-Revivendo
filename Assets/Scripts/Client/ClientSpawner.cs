using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabClient;
    [SerializeField] private float summonTime;
    [SerializeField] private List<Transform> chairs = new List<Transform>();

    private float timer;
    private List<Transform> emptyChairs = new List<Transform>();

    private void Start()
    {
        foreach(Transform chair in chairs)
        {
            emptyChairs.Add(chair);
        }
        timer = summonTime;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            try
            {
                Summon();
            }
            catch
            {
                if(emptyChairs.Count == 0)
                {
                    Debug.LogWarning("Não tem cadeira vazia");
                }
                else
                {
                    Debug.LogError("Deu ruim na summon de cliente");
                }
            }
        }
    }

    public void FreeChair(Transform chair)
    {
        if (chairs.Contains(chair) && !emptyChairs.Contains(chair))
        {
            emptyChairs.Add(chair);
        }
    }

    public void Summon()
    {
        Summon(emptyChairs[Random.Range(0, emptyChairs.Count)]);
    }
    public void Summon(Transform chair)
    {
        GameObject newClient = Instantiate(prefabClient, chair.position, chair.rotation, chair);
        timer = summonTime;
        emptyChairs.Remove(chair);
    }       //spawna os clientes como _filho_ da cadeira. não é ideal mas dá pro gasto por enquanto.

}
