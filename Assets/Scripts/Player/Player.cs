using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Image staminaMeter;
    [SerializeField] Transform holdPos;
    [SerializeField] float stamina, staminaDuration, staminaCoolDown;
    [SerializeField] float speed;
    [SerializeField] bool resting;
    private GameObject heldItem;
    NavMeshAgent agent;

    private void Start()
    {
        heldItem = null;
        agent = GetComponent<NavMeshAgent>();
        resting = false;
        stamina = 1;
    }

    void Update()
    {
        NavMesh();
        Stamina();
        SpeedControl();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickable") && heldItem == null)
        {
            PickUp(other.gameObject);
        }
        else if (other.CompareTag("DropPoint") && heldItem != null)
        {
            DropItem(other.gameObject.transform);
        }
    }

    // IA de navegação do personagem controlavel
    void NavMesh()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }

    // Impede que o valor de stamina extrapole os limites estabelecidos e mantem o medidor correto
    // Faz a stamina decair com o tempo e estabelece condição de recuperação
    void Stamina()
    {
        stamina = Mathf.Clamp(stamina, -0.1f, 1.1f);

        if (resting) stamina += Time.deltaTime / staminaCoolDown;
        else stamina -= Time.deltaTime / staminaDuration;

        staminaMeter.fillAmount = stamina;
    }

    // Reduz a velocidade do player conforme a stamina diminui (obs: Sim Artur, eu sei que podia estar melhor)
    void SpeedControl()
    {
        if (stamina < 0.25) agent.speed = speed / 3;
        else agent.speed = speed;
    }

    // Pega o item desejado, o transforma em filho do holdPos e envia para posição de carregamento.
    void PickUp(GameObject item)
    {
        heldItem = item;
        heldItem.transform.SetParent(holdPos);
        heldItem.transform.position = holdPos.position;
        heldItem.transform.rotation = Quaternion.identity;
    }

    // Deixa o item carregado na posição recebida
    void DropItem(Transform dropPos)
    {
        heldItem.transform.SetParent(null);
        heldItem.transform.localPosition = dropPos.position;
        heldItem.transform.localRotation = Quaternion.identity;
        heldItem = null;
    }

}
