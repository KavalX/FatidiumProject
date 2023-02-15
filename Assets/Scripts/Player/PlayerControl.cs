using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerControl : NetworkBehaviour
{
    [SerializeField]
    protected float speed = 0.1f;
    
    [SerializeField]
    private NetworkVariable<float> forwardBackPosition = new();

    [SerializeField]
    private NetworkVariable<float> leftRightPosition = new();






    private Rigidbody2D rigBody;




    private void Start()
    {

        rigBody = GetComponentInParent<Rigidbody2D>();

    }

    private void Update()
    {

        if (IsServer)
        {
            UpdateServer();
        }

        if (IsClient && IsOwner)
        {
            UpdateClient();
        }

    }

    private void UpdateServer()
    {
        transform.position = new Vector3(transform.position.x + leftRightPosition.Value, transform.position.y,
            transform.position.z + forwardBackPosition.Value);
    }

    private void UpdateClient()
    {
        this.transform.rotation = Quaternion.identity;
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 force = new Vector2(x, y);

        //rigBody.AddForce(force * walkSpeed);
        // update the server
        UpdateClientPositionServerRpc(force, speed);

    }

    [ServerRpc]
    private void UpdateClientPositionServerRpc(Vector3 direction, float walkSpeed)
    {
        rigBody.velocity = direction* walkSpeed;
    }
}
