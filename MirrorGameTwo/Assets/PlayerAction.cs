using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerAction : NetworkBehaviour
{
    [SerializeField] private float AttackRadius;
    private NetworkAnimator NetworkAnim;

    private void Awake()
    {
        NetworkAnim = GetComponent<NetworkAnimator>();      
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position+transform.forward+transform.up, AttackRadius);

        
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (!isLocalPlayer) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isServer)
            {
                OnAttack();
            }

            else
            {
                if (isClient)
                {
                    CMDAttack();
                }
            }
            NetworkAnim.SetTrigger("Fight");
        }

        

        
    }
    public void TakeDamageCh()
    {
        RPCTakeDamageCh();
    }
    [ClientRpc]
    public void RPCTakeDamageCh()
    {
        if (isClient)
        {
            NetworkAnim.SetTrigger("TakeDown"); 
        }
    }
    [Command]
    private void CMDAttack() 
    {
        OnAttack();
    }
    private void OnAttack() 
    {
        Collider[] hits = Physics.OverlapSphere(transform.position + transform.forward + transform.up, AttackRadius);

        foreach (Collider hit in hits)
        {
            if (hit.gameObject == this.gameObject) continue;
            
            if (hit.gameObject.TryGetComponent(out PlayerAction enemy)) 
            {
                enemy.TakeDamageCh();
            } 
        }
    }
}
