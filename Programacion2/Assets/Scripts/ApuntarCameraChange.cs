using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApuntarCameraChange : MonoBehaviour
{

    private bool estaApuntando = false;
    public GameObject mira;

    //public GameObject thirdPersonCam;
   // public GameObject apuntarCam;

    public CinemachineVirtualCamera apuntarCam;
    //public CinemachineFreeLook thirdPersonCam;


    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Mouse1)) {
           estaApuntando = true;
            apuntarCam.Priority += 2;

        }
        if (Input.GetKeyUp(KeyCode.Mouse1)) {
           estaApuntando = false;
            apuntarCam.Priority -= 2;
        }
        
         mira.SetActive(estaApuntando);

    }
}
