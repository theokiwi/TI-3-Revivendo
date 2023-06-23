using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Shader highlightShader;
    [SerializeField] Shader defaultShader;
    [SerializeField] Image staminaMeter;
    [SerializeField] LayerMask floor;
    [SerializeField] LayerMask plates;
    [SerializeField] Transform holdPos;
    [SerializeField] float stamina, staminaDuration, staminaCoolDown;
    [SerializeField] float speed;
    [SerializeField] bool resting;
    private DishContainer heldItem;
    private GameObject hitTarget;
    private GameObject targetObject = null;
    private GameObject markedObject = null;
    NavMeshAgent agent;

    private void Start()
    {
        heldItem = null;
        agent = GetComponent<NavMeshAgent>();
        resting = false;
        stamina = 5;
        AudioManager.instance.Ruins();
    }

    void Update()
    {
        if(Time.timeScale != 0)
        {
            GetMouseClick();
            Stamina();
            Interact();
            //SpeedControl();
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

    private void OnTriggerEnter(Collider other)
    {
        /*
        if (other.CompareTag("Pickable") && heldItem == null)
        {
            PickUp(other.gameObject);
            AudioManager.instance.Item();
        }
        else if (other.CompareTag("DropPoint"))
        {
            if (other.transform.childCount == 0)
            { 
                SeatBehaviour seat = other.GetComponent<SeatBehaviour>();

                if (!seat.client)
                {
                    return;
                }
                else
                if (seat.client.clientState == ClientBehaviour.CLIENT_STATES.Ready)
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
        }
        if (other.CompareTag("Bed"))
        {
            resting = true;
        }
        */
    }

    private void OnTriggerExit(Collider other)
    {
        /*
        if (other.CompareTag("Bed"))
        {
            resting = false;
        }
        */
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
        if(target.CompareTag("Pickable") && heldItem == null)
        {
            PickUp(target);
            AudioManager.instance.Item();
        }
        else if(target.CompareTag("Table"))
        {
            Transform dropPoint = Helper.FindChildWithTag(target, "DropPoint");
            if(dropPoint.childCount == 0)
            {
                ManageSeat(target.GetComponent<SeatBehaviour>());
            }            
        }
    }

    // Faz um raycast a partir da posicao do mouse na tela quando o botão esquerdo do mouse for pressionado
    // Retorna o Vector3 do ponto atingido pelo raycast para o metodo NavMesh que eentao decide como executar a movimentacao do player
    // AVISO: o metodo nao chamara o metodo NavMesh caso o Time.timeScale seja igual a 0, para evitar que o player se movimente na tela de pausa
    private void GetMouseClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, floor | plates) && Time.timeScale != 0)
            {
                hitTarget = hit.transform.gameObject;

                if(hitTarget.CompareTag("Pickable") || hitTarget.CompareTag("Table") && hitTarget != markedObject)
                {
                    if(targetObject == null)
                    {
                        targetObject = hitTarget;
                        Highlight(targetObject, highlightShader, Color.blue);
                    }
                    else if(hitTarget != targetObject)
                    {
                        Highlight(targetObject, defaultShader, Color.blue);
                        targetObject = null;
                    }
                }
                else if(targetObject != null)
                {
                    Highlight(targetObject, defaultShader, Color.blue);
                    targetObject = null;
                }

                if(Input.GetMouseButtonDown(0))
                {
                    if(hitTarget.CompareTag("Floor"))
                    {
                        agent.destination = hit.point;
                    }
                    else if(hitTarget.CompareTag("Pickable") || hitTarget.CompareTag("Table"))
                    {
                        Debug.Log("click");
                        if(NavMesh.SamplePosition(hitTarget.transform.position, out NavMeshHit data, 2, NavMesh.AllAreas))
                        {
                            if(markedObject != null)
                            {
                                Highlight(markedObject, defaultShader, Color.white);
                                markedObject = null;
                            }
                            agent.destination = data.position;
                            markedObject = hitTarget;
                            targetObject = null;
                            Highlight(markedObject, highlightShader, Color.yellow);
                        }
                    }
                }
            }
            else if(targetObject != null)
            {
                Highlight(targetObject, defaultShader, Color.blue);
            }
            targetObject = null;
    }

    private void Interact()
    {
        if(!agent.pathPending)
        {
            if(agent.remainingDistance <= agent.stoppingDistance)
            {
                if(!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    if(markedObject != null)
                    {
                        transform.LookAt(markedObject.transform.position);
                        Highlight(markedObject, defaultShader, Color.white);
                        markedObject = null;
                    }
                }
            }
        }
    }

    // IA de navega��o do personagem controlavel
    // Caso o player tenha clicado em uma mesa, o target sera redirecionado para uma posicao pre-estabelicida para evitar bugs visuais
    /*void NavMesh(Vector3 target)
    {
        if(hitTarget.CompareTag("Table"))
        {
            // O objeto referencia do target deve ser SEMPRE o primeiro objeto filho do hitTarget no inspetor
            target = hitTarget.transform.GetChild(0).transform.position;
        }

        agent.SetDestination(target);
    }*/

    // Impede que o valor de stamina extrapole os limites estabelecidos e mantem o medidor correto
    // Faz a stamina decair com o tempo e estabelece condi��o de recupera��o
    void Stamina()
    {
        stamina = Mathf.Clamp(stamina, -0.1f, 1.1f);

        if (resting) stamina += Time.deltaTime / staminaCoolDown;
        else stamina -= Time.deltaTime / staminaDuration;

        staminaMeter.fillAmount = stamina;

        if(stamina <= 0)
        {
            GameController.Instance.GameOver();
        }
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

    private void Highlight(GameObject target, Shader shader, Color color)
    {
        Debug.Log("highlightGoes burr");
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
