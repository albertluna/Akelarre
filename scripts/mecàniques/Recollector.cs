using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recollector : MonoBehaviour
{
    Rigidbody rb;
    Collider colider;
    public int vides;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        colider = GetComponent<CapsuleCollider>();
        vides = 3;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Colleccionable"))
        {
            Debug.Log("Transportar colleccionable a constructor");
            Destroy(collision.gameObject);
        } else if(collision.gameObject.CompareTag("Bullet") && vides > 0)
        {
            vides--;
            Destroy(collision.gameObject);            
        }
    }

}
