using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casa : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        
        Debug.Log("XOC");
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            FindObjectOfType<Defensor>().UnaVidaMenys();
        }
    }
}
