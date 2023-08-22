using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Shader highlightShader, defaultShader;
    [SerializeField] LayerMask floor, plates;
    private Transform holdPos;
    private DishContainer heldItem = null;
    private GameObject targetObject = null;
    [SerializeField] private GameObject markedObject;
    private NavMeshAgent agent;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(Time.timeScale != 0)
        {
            RayCasting();
            if(markedObject != null && HasReachedDestination(markedObject))
            {
                Interact(markedObject);
                markedObject = null;
            }
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            GameController.Instance.OpenKitchen();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameController.Instance.PauseMenu();
        }
    }

    private void ManageSeat(SeatBehaviour seat)
    {
        if (!seat.client)
        {
            return;
        }
        else if (seat.client.clientState == ClientBehaviour.CLIENT_STATES.Ready)
        {
            seat.client.clientState = ClientBehaviour.CLIENT_STATES.Waiting;
            GameController.Instance.GetOrder(seat.client.dishData);
        }
        else if(heldItem != null)
        {
            DropItem(seat);
            AudioManager.instance.Item();
        }
    }

    private void Interact(GameObject target)
    {
        Debug.Log("interacting");
        Highlight(target, defaultShader, Color.blue);
        if(target.CompareTag("Pickable") && heldItem == null)
        {
            PickUp(target);
            //AudioManager.instance.Item();
        }
        else if(target.CompareTag("Table"))
        {
            Transform dropPoint = Helper.FindChildWithTag(target, "DropPoint");
            if(dropPoint.childCount == 0)
            {
                //ManageSeat(target.GetComponent<SeatBehaviour>());
            }            
        }
    }

    // Faz um raycast a partir da posicao do mouse na tela quando o botão esquerdo do mouse for pressionado
    // Retorna o Vector3 do ponto atingido pelo raycast para o metodo NavMesh que eentao decide como executar a movimentacao do player
    // AVISO: o metodo nao chamara o metodo NavMesh caso o Time.timeScale seja igual a 0, para evitar que o player se movimente na tela de pausa
    private void RayCasting()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, floor | plates) && Time.timeScale != 0)
            {
                onHoverMouse(hit.transform);
                if(Input.GetMouseButtonDown(0))
                {
                    if(hit.transform.CompareTag("Floor")) agent.destination = hit.point;
                    else OnClickObject(hit.transform.gameObject);
                }
            }
            else if(targetObject != null)
            {
                Highlight(targetObject, defaultShader, Color.blue);
                targetObject = null;
            }
    }

    private void onHoverMouse(Transform hitTarget)
    {
        if ((hitTarget.CompareTag("Pickable") || hitTarget.CompareTag("Table")) && (hitTarget.gameObject != markedObject))
        {
            if(targetObject != null)
            {
                Highlight(targetObject, defaultShader, Color.blue);
            }
            Highlight(hitTarget.gameObject, highlightShader, Color.blue);
            targetObject = hitTarget.gameObject;
        }
        else if (targetObject != null)
        {
            Highlight(targetObject, defaultShader, Color.blue);
            targetObject = null;
        }
    }

    private bool HasReachedDestination(GameObject destination)
    {
        float distance = Vector3.Distance(transform.position, destination.transform.position);
        Debug.Log(distance);
        if (distance <= agent.stoppingDistance + 0.25f)
        {
            return true;   
        }
        return false;
    }

    // Pega o objeto clicado, calcula uma posição destino em até 2 metros do alvo, tira o highlight do ultimo alvo e highlighta o novo alvo
    private void OnClickObject(GameObject hitTarget)
    {
        if (NavMesh.SamplePosition(hitTarget.transform.position, out NavMeshHit data, 2, NavMesh.AllAreas))
        {
            if(markedObject != null)
            {
                Highlight(markedObject, defaultShader, Color.blue);
                markedObject = null;
            }

            agent.destination = data.position;
            markedObject = hitTarget;
            Highlight(markedObject, highlightShader, Color.yellow);
            targetObject = null;
        }
    }

    // Pega o item desejado, o transforma em filho do holdPos e envia para posi��o de carregamento.
    void PickUp(GameObject item)
    {
        heldItem = item.GetComponent<DishContainer>();
        heldItem.transform.SetParent(holdPos);
        heldItem.transform.position = holdPos.position;
        heldItem.transform.rotation = Quaternion.identity;
    }

    // Deixa o item carregado na posicao recebida, apenas se ha cliente no assento.
    void DropItem(SeatBehaviour seat)
    {
            seat.ServedDish = heldItem;

            heldItem.transform.SetParent(seat.transform);
            heldItem.transform.localPosition = Vector3.zero;
            heldItem.transform.localRotation = Quaternion.identity;
            heldItem.tag = "Untagged";
            heldItem = null;
    }

    // Faz o highlight de objetos
    private void Highlight(GameObject target, Shader shader, Color color)
    {
        Debug.Log($"{target.name} is being highlighted");

        MeshRenderer [] renders;
        renders = target.GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer data in renders)
        {
            foreach(Material mat in data.materials)
            {
                mat.shader = shader;
                mat.SetColor("_HighlightColor", color);
            }
        }
    }
}
