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
    private GameObject hitTarget;
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
        GetMouseClick();
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
            if(other.transform.childCount == 0)
            {
                DropItem(other.gameObject);
            }
        }
        if (other.CompareTag("Bed"))
        {
            resting = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bed"))
        {
            resting = false;
        }
    }

    // Faz um raycast a partir da posicao do mouse na tela quando o botão esquerdo do mouse for pressionado
    // Retorna o Vector3 do ponto atingido pelo raycast para o metodo NavMesh que eentao decide como executar a movimentacao do player
    // AVISO: o metodo nao chamara o metodo NavMesh caso o Time.timeScale seja igual a 0, para evitar que o player se movimente na tela de pausa
    private void GetMouseClick()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit) && Time.timeScale != 0)
            {
                hitTarget = hit.transform.gameObject;
                NavMesh(hit.point);
            }
        }
    }

    // IA de navega��o do personagem controlavel
    // Caso o player tenha clicado em uma mesa, o target sera redirecionado para uma posicao pre-estabelicida para evitar bugs visuais
    void NavMesh(Vector3 target)
    {
        if(hitTarget.CompareTag("Table"))
        {
            target = hitTarget.GetComponent("targetPos").transform.position;
        }

        agent.SetDestination(target);
    }

    // Impede que o valor de stamina extrapole os limites estabelecidos e mantem o medidor correto
    // Faz a stamina decair com o tempo e estabelece condi��o de recupera��o
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

    // Pega o item desejado, o transforma em filho do holdPos e envia para posi��o de carregamento.
    void PickUp(GameObject item)
    {
        heldItem = item;
        heldItem.transform.SetParent(holdPos);
        heldItem.transform.position = holdPos.position;
        heldItem.transform.rotation = Quaternion.identity;
    }

    // Deixa o item carregado na posi��o recebida
    void DropItem(GameObject dropPos)
    {
        heldItem.transform.SetParent(dropPos.transform);
        heldItem.transform.localPosition = Vector3.zero;
        heldItem.transform.localRotation = Quaternion.identity;
        heldItem = null;
    }
}
