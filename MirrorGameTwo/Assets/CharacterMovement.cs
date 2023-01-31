using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CharacterMovement : NetworkBehaviour
{
    private CharacterController chcont;
    private Animator anim;
    [SerializeField] private float movementspeed;
    [SerializeField] private GameObject cam;

    private float horizontal, vertical;
    private void Awake()
    {
        chcont = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        GameObject camobj = Instantiate(cam);
        NetworkServer.Spawn(camobj);
    }

    void Update()
    {
        if (!isLocalPlayer) return;
        
        horizontal = Input.GetAxis("Horizontal")*movementspeed*Time.deltaTime;
        vertical = Input.GetAxis("Vertical") * movementspeed * Time.deltaTime;
        chcont.Move(new Vector3(horizontal,0,vertical));


        if (horizontal!=0 || vertical!=0)
        {
            anim.SetBool("IsMoving",true);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }
        
    }
}
