using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabClient;
    [SerializeField] private float summonTime;
    [SerializeField] private List<SeatBehaviour> chairs = new List<SeatBehaviour>();

    private float timer;
    private List<SeatBehaviour> emptyChairs = new List<SeatBehaviour>();

    private void Start()
    {
        foreach(SeatBehaviour chair in chairs)
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
                foreach(SeatBehaviour chair in chairs)
                {
                    FreeChair(chair);
                }
                if(emptyChairs.Count == 0)
                {
                    Debug.LogWarning("N�o tem cadeira vazia");
                }
                else
                {
                    Debug.LogError("Deu ruim na summon de cliente");
                }
            }
        }
    }

    public void FreeChair(SeatBehaviour chair)
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
    public void Summon(SeatBehaviour chair)
    {
        GameObject newClient = Instantiate(prefabClient, chair.seat.position, chair.seat.rotation, chair.seat);
        chair.client = newClient.GetComponent<ClientBehaviour>();
        timer = summonTime;
        emptyChairs.Remove(chair);
        //spawna os clientes como _filho_ da cadeira. n�o � ideal mas d� pro gasto por enquanto.
    }
}
