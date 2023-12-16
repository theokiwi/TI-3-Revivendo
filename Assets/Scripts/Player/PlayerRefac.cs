using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerRefac : Singleton<PlayerRefac>
{

    #region Run time behaviour

    public static Action E_action, ESC_action;
    public IAction rightClick;
    private NavMeshAgent agent;
    private float maxSpeed, minSpeed;
    [SerializeField] float  slowCD, slowTime;
    private bool slow;
    private bool intercating;
    [SerializeField] private Animator animator;

    private void Start(){
        intercating = false;
        slow = false;
        agent = GetComponent<NavMeshAgent>();
        //E_action += GameController.Instance.OpenKitchen;
        ESC_action += GameController.Instance.PauseToggle;
        default_shader = Shader.Find("Standard");
        maxSpeed = agent.speed;
        minSpeed = maxSpeed/3;
    }

    private void Update(){
        if(!GameController.Instance._IsPaused){
            CastRay();
            if(targetObject != null && HasReachedDestination(targetObject.transform)){
                Interact(interaction);
            }
        }
        if(Input.GetKeyDown(KeyCode.E)){
            E_action.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            ESC_action.Invoke();
        }
        if(Vector3.Distance(agent.destination, transform.position) < .5f)
        {
            animator.SetBool("Walk_On", false);
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Dirt")){
            SlowDown(slowCD);
        }
    }

    #endregion

    #region Base
    public Transform holdPos;
    public GameObject heldObject;
    public AbstractInteractable interaction;
    public float throwForce;


    private void Interact(AbstractInteractable i){
        if(!intercating){
            intercating = true;

            i.Interact();
            if(i.GetType() == typeof(Dish) || i.GetType() == typeof(Client))
            {
                animator.SetBool("Plate_On", true);
            }
            if(i.GetType() == typeof(Table))
            {
                animator.SetBool("Plate_On", false);
            }
            interaction = null;
            Highlight(targetObject, default_shader, Color.blue);
            targetObject = null;
            
            intercating = false;
        }
    }

    private bool HasReachedDestination(Transform destination){
        float distance = Vector3.Distance(transform.position, destination.position);
        if(distance <= 2.25f){
            return true;
        }
        return false;
    }

    private void SlowDown(float cd){
        StartCoroutine(Slowed(cd));
    }

    private bool CountDown(float value){
        value -= Time.deltaTime;
        if(value <= 0){
            return true;
        }
        else{
            return false;
        }
    }
    private IEnumerator Slowed(float cd){
        slowTime = cd;
        if(slow){
            yield break;
        }
        else{
            slow = true;
            agent.speed = minSpeed;
            yield return new WaitUntil(() => CountDown(slowTime));
            agent.speed = maxSpeed;
            slow = false;
        }
        yield break;
    }

    #endregion

    #region Mouse interaction 

    [SerializeField] Shader HL_shader, default_shader;
    [SerializeField] Color HL_color = Color.blue, target_color = Color.yellow;
    [SerializeField] LayerMask interactLayer;
    private GameObject HLObject;
    private GameObject targetObject;
    public RaycastHit hitInfo;

    private void CastRay(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hitInfo, Mathf.Infinity, interactLayer) && Time.timeScale != 0){
            OnHoverMouse(hitInfo.transform);
            if(Input.GetMouseButtonDown(0)){
                OnClick(hitInfo);
            }
            if(Input.GetMouseButtonDown(1)){
                if(rightClick != null) rightClick.Action();
                animator.SetBool("Plate_On", false);        //nao tem como saber daqui direito quando que o prato sai entao qualquer botao direito desliga a animacao
            }
        }
        else if(HLObject != null){
            Highlight(HLObject, default_shader, Color.blue);
            HLObject = null;
        }

        // Internal functions
        void OnHoverMouse(Transform hit){
            if(hit.gameObject != targetObject && !hit.gameObject.CompareTag("Floor")){
                if(HLObject != null){
                    Highlight(HLObject, default_shader, Color.blue);
                }
                Highlight(hit.gameObject, HL_shader, Color.blue);
                HLObject = hit.gameObject;
            }
            else if(HLObject != null){
                Highlight(HLObject, default_shader, Color.blue);
                HLObject = null;
            }
        }
        void OnClick(RaycastHit hit){
            if(hit.transform.CompareTag("Floor")){
                agent.destination = hit.point;
                if(Vector3.Distance(agent.destination, transform.position) > .5f)
                {
                    animator.SetBool("Walk_On", true);
                }
                return;
            }
            if(NavMesh.SamplePosition(hit.transform.position, out NavMeshHit data, 3f, NavMesh.AllAreas)){
                if(targetObject != null){
                    Highlight(targetObject, default_shader, Color.blue);
                }
                agent.destination = data.position;
                animator.SetBool("Walk_On", true);
                targetObject = hit.transform.gameObject;
                interaction = hit.transform.GetComponent<AbstractInteractable>();
                Highlight(targetObject, HL_shader, Color.yellow);
                HLObject = null;
                if(Vector3.Distance(agent.destination, transform.position) > .5f)
                {
                    animator.SetBool("Walk_On", true);
                }
            }
            
        }
    }

    private void Highlight(GameObject target, Shader shader, Color color){
        MeshRenderer[] renderers = target.GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer render in renderers){
            foreach(Material mat in render.materials){
                mat.shader = shader;
                mat.SetColor("_HighlightColor", color);
            }
        }
    }

    #endregion

}
