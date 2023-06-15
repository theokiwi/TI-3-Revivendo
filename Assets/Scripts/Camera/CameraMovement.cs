using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{ 
    [SerializeField] private float panSpeed;
    [SerializeField] private float rotSpeed;
    [SerializeField] private float zoomSpeed;
    [SerializeField] [Range(0, 1)] private float panSmoothing;    //quantidade de "follow through" do movimento
    [SerializeField] [Range(0, 1)] private float rotSmoothing;    //quantidade de "follow through" da rotacao
    [SerializeField] private float minZoom;     
    [SerializeField] private float maxZoom;     

    //Guardam a input pra acontecer no proximo frame de fisica
    private Vector2 panInput;   //nomes horriveis, eu sei mas nao sei do que chamar     -alu
    private float rotInput;     //eu sei que isso parece que mente sobre como funciona pro resto do codigo mas me parece adequado pra separar a input no update
                                //das coisas no fixedupdate, algm de nao deixar o resto do codigo comunicar diretamente com propriedades. Instinto de POO talvez.   -alu
    private bool _rotating;     //mergi a parte de input pra camera aqui, entao agora precisa tbm dessas bools e a posicao do mouse no frame anterior;
    private bool _panning;
    private Vector2 previousMousePos = Vector2.zero;

    private Camera cam;  //cam.transform.parent pra ser usado em tudo menos zoom.  -alu

    //Tem jeitos melhores de fazer isso mas vai ser isso por enquanto
    //Objetos de cenario a serem escondidos dependendo da direcao da camera
    [SerializeField] private float disableWallAngle;
    [SerializeField] private GameObject northWall;
    [SerializeField] private GameObject southWall;
    [SerializeField] private GameObject eastWall;
    [SerializeField] private GameObject westWall;

    private void Awake()
    {
        cam = Camera.main;
        if (cam.transform.parent == null)
        {
            cam.transform.SetParent(Instantiate(new GameObject("cameraParent"), Vector3.zero, Quaternion.Euler(0,-45,0), null).transform);
        }
    }

    private void Update()
    {
        //Atualizacao de inputs
        #region inputs

        //zoom (acontece primeiro pra ter o tamanho da camera)

        float camSize = cam.orthographicSize;
        camSize += -Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

        camSize = Mathf.Clamp(camSize,minZoom,maxZoom);

        cam.orthographicSize = camSize;
        
        //movimento 

        Vector2 mousePos = cam.ScreenToViewportPoint(Input.mousePosition);
        Vector2 mouseDelta = previousMousePos - mousePos;
        previousMousePos = mousePos;

        if(Input.GetKeyDown(KeyCode.Mouse1))
            _panning = true;
        if(Input.GetKeyUp(KeyCode.Mouse1))
            _panning = false;
        if(_panning) 
        {
            PanCamera(mouseDelta);
        }              

        //rotacao

        if(Input.GetKeyDown(KeyCode.Mouse2))
            _rotating = true;
        if(Input.GetKeyUp(KeyCode.Mouse2))
            _rotating = false;

        if(_rotating) 
        {
            RotateCamera(mouseDelta.x);
        }              
        #endregion

        //aqui que desliga as paredes
        northWall.SetActive(
            Vector3.Angle(Vector3.forward, cam.transform.parent.forward) < disableWallAngle?
            true : false
            );
        southWall.SetActive(
            Vector3.Angle(Vector3.back, cam.transform.parent.forward) < disableWallAngle?
            true : false
            );
        
        eastWall.SetActive(
            Vector3.Angle(Vector3.right, cam.transform.parent.forward) < disableWallAngle?
            true : false
            );
        westWall.SetActive(
            Vector3.Angle(Vector3.left, cam.transform.parent.forward) < disableWallAngle?
            true : false
            );
    }

    private void FixedUpdate()
    {
        //Aplicacao da input em movimento
        cam.transform.parent.Translate(panInput.x * panSpeed * cam.orthographicSize, 0, panInput.y * panSpeed * cam.orthographicSize);
        panInput = Vector2.Lerp(Vector2.zero, panInput, panSmoothing);  //lerps pro movimento parar lentamente ao inves de de forma brusca
        cam.transform.parent.Rotate(0, rotInput * rotSpeed, 0);
        rotInput = Mathf.Lerp(0, rotInput, rotSmoothing);
    }

    public void PanCamera(Vector2 vector)
    {
        panInput = (panInput + vector);
    }

    public void RotateCamera(float amount)
    {
        rotInput += amount;
    }
}
