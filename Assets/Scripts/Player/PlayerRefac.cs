using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerRefac : Singleton<PlayerRefac>
{

    #region Run time behaviour

    public static Action E_action, ESC_action;
    public IAction rightClick;
    private NavMeshAgent agent;


    private void Start(){
        agent = GetComponent<NavMeshAgent>();
        E_action += GameController.Instance.OpenKitchen;
        //ESC_action += GameController.Instance.PauseMenu;
        default_shader = Shader.Find("Standard");
    }

    private void Update(){
        if(Time.timeScale != 0){
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
    }

    #endregion

    #region Base
    public Transform holdPos;
    public GameObject heldObject;
    public AbstractInteractable interaction;
    public float throwForce;


    private void Interact(AbstractInteractable i){
        i.Interact();
        Highlight(targetObject, default_shader, Color.blue);
        targetObject = null;
        interaction = null;
    }

    private bool HasReachedDestination(Transform destination){
        float distance = Vector3.Distance(transform.position, destination.position);
        if(distance <= 2.25f){
            return true;
        }
        return false;
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
                return;
            }
            if(NavMesh.SamplePosition(hit.transform.position, out NavMeshHit data, 2, NavMesh.AllAreas)){
                if(targetObject != null){
                    Highlight(targetObject, default_shader, Color.blue);
                }
                agent.destination = data.position;
                targetObject = hit.transform.gameObject;
                interaction = hit.transform.GetComponent<AbstractInteractable>();
                Highlight(targetObject, HL_shader, Color.yellow);
                HLObject = null;
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
