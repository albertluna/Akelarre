using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Moviment : MonoBehaviourPun
{
    private Rigidbody rb;
    public PhotonView pv;

    public float velocity = 10;
    public camera camera;
    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        dir = camera.transform.forward + camera.transform.right;
        dir.Normalize();

        //animator.SetFloat("Speed", Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));

        Vector3 tmp = camera.transform.right * horizontalInput + camera.transform.forward * verticalInput;
        rb.velocity = velocity * new Vector3(tmp.x, 0, tmp.z);
    }
}
