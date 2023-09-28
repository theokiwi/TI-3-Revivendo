using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //N�o precisa fazer tudo de input aqui ja que � prot�tipo mas vou fazer pra vcs verem o que eu to tendo de ideia    -alu

    private CameraMovement cameraMovement;

    private Vector2 mouseDelta;     
    private Vector2 prevMousePos;   //Frame do Update() anterior, se usarem no FixedUpdate() vai ficar esquisito    -alu
    private enum INPUT_MODE
    {
        standard,
        camera_rotate
    }

    private INPUT_MODE activeInputMode;

    private void Awake()
    {
        //Atribui��es essenciais
        cameraMovement = FindFirstObjectByType<CameraMovement>();
    }

    private void Start()
    {
        //Atribui��es iniciais
        activeInputMode = INPUT_MODE.standard;
        mouseDelta = Vector2.zero;
        prevMousePos = Input.mousePosition;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        //if !gamemanager.paused ou alguma coisa nisso tudo     -alu

        //Atualiza��o da delta do mouse (SEMPRE FAZER ANTES DE EXECU��ES/C�LCULOS)
        mouseDelta = prevMousePos - (Vector2)Input.mousePosition;   

        //Switch case para cada modo de input
        switch (activeInputMode)
        {
            case INPUT_MODE.standard:   StandardInputGet(); break;
            case INPUT_MODE.camera_rotate:     CameraRotInputGet(); break;
        }

        //Movimento de camera quando o mouse vai pra beirada da tela
        {
            if(Input.mousePosition.x <= 5)
            {
                cameraMovement.PanCamera(Vector2.left);
            }else if (Input.mousePosition.x >= Screen.width -5)
            {
                cameraMovement.PanCamera(Vector2.right);
            }
            if (Input.mousePosition.y <= 5)
            {
                cameraMovement.PanCamera(Vector2.down);
            }else if (Input.mousePosition.y >= Screen.height -5)
            {
                cameraMovement.PanCamera(Vector2.up);
            }
        }

        //Atualiza��o da posi��o pr�via do mouse (SEMPRE FAZER DEPOIS DE EXECU��ES/C�LCULOS)
        prevMousePos = Input.mousePosition;
    }

    //Pra coisas que n�o devem acontecer em outros modos e pra trocar entre eles
    private void StandardInputGet()     
    {
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            activeInputMode = INPUT_MODE.camera_rotate;
            Cursor.visible = false;
        }

    }

    //Roda enquanto t� segurando o bot�o do meio do mouse
    private void CameraRotInputGet()        //(Pra acontecer a coisa bonitinha de o cursor travar e sumir enquanto t� rodando a camera sabe     -alu)
    {
        if (Input.GetKeyUp(KeyCode.Mouse2))
        {
            activeInputMode = INPUT_MODE.standard;
            Cursor.visible = true;
        }

        cameraMovement.RotateCamera(mouseDelta.x);
    }
}
