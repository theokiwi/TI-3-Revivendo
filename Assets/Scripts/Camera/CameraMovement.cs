using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{ 
    [SerializeField] private float panSpeed;
    [SerializeField] private float rotSpeed;
    [SerializeField] [Range(0, 1)] private float panSmoothing;    //quantidade de "follow through" do movimento
    [SerializeField] [Range(0, 1)] private float rotSmoothing;    //quantidade de "follow through" da rotacao

    //Guardam a input pra acontecer no proximo frame de fisica
    private Vector2 panInput;   //nomes horriveis, eu sei mas nao sei do que chamar     -alu
    private float rotInput;     //eu sei que isso parece que mente sobre como funciona pro resto do codigo mas me parece adequado pra separar a input no update
                                //das coisas no fixedupdate, algm de nao deixar o resto do codigo comunicar diretamente com propriedades. Instinto de POO talvez.   -alu
    private bool _rotating;     //mergi a parte de input pra camera aqui, entao agora precisa tbm dessa bool e a posicao do mouse no frame anterior;
    private Vector2 previousMousePos = Vector2.zero;

    private Camera cam;  //cam.transform.parent pra ser usado em tudo menos zoom.  -alu

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

        Vector2 mousePos = cam.ScreenToViewportPoint(Input.mousePosition);
        Vector2 mouseDelta = previousMousePos - mousePos;
        previousMousePos = mousePos;

        if(mousePos.x > 0.95)
            PanCamera(Vector2.right);
        if(mousePos.y > 0.95)
            PanCamera(Vector2.up);
        if(mousePos.x < 0.05)
            PanCamera(Vector2.left);
        if(mousePos.y < 0.05)
            PanCamera(Vector2.down);

        if(Input.GetKeyDown(KeyCode.Mouse2))
            _rotating = true;
        if(Input.GetKeyUp(KeyCode.Mouse2))
            _rotating = false;

        if(_rotating) 
        {
            RotateCamera(mouseDelta.x);
        }
        
        #endregion
    }

    private void FixedUpdate()
    {
        //Aplicacao da input em movimento
        cam.transform.parent.Translate(panInput.x * panSpeed, 0, panInput.y * panSpeed);
        panInput = Vector2.Lerp(Vector2.zero, panInput, panSmoothing);  //lerps pro movimento parar lentamente ao inves de de forma brusca
        cam.transform.parent.Rotate(0, rotInput * rotSpeed, 0);
        rotInput = Mathf.Lerp(0, rotInput, rotSmoothing);
    }

    public void PanCamera(Vector2 vector)
    {
        panInput = (panInput + vector).normalized;
    }

    public void RotateCamera(float amount)
    {
        rotInput += amount;
    }
}
