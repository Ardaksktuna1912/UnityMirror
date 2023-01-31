using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerCameraCamHolder : NetworkBehaviour
{
    [SerializeField] private NetworkIdentity target;
    private float rotY=90 ;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    void Update()
    {
        if (target==null)
        {
            target = NetworkClient.localPlayer;
            return;
        }
        if (!target.isLocalPlayer)
        {
            Destroy(this.gameObject);
            return;
        }

        rotY += Input.GetAxis("Mouse X");
        transform.rotation = Quaternion.Euler(new Vector3(0, rotY, 0));
        transform.position = target.transform.position + (Vector3.up * 2);
        target.transform.forward = transform.forward;
    }
}
