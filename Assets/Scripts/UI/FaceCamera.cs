using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FaceCamera : MonoBehaviour
{
    [SerializeField] GameObject parent;
    private Camera mainCamera;


    private void Start()
    {
        mainCamera = Camera.main;
    }

   private void LateUpdate() 
   {
        transform.LookAt(parent.transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
   }
}
