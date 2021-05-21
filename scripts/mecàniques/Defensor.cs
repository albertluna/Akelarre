using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defensor : MonoBehaviour
{
    Rigidbody rb;
    Collider colider;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        colider = GetComponent<CapsuleCollider>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Colleccionable"))
        {
            Destroy(collision.gameObject);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
