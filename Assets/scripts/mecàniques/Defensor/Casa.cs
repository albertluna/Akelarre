using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casa : MonoBehaviour
{
    private AudioSource audioXoc;

    private void Start()
    {
        audioXoc = GetComponent<AudioSource>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            audioXoc.PlayOneShot(audioXoc.clip, 1f);
            FindObjectOfType<Defensor>().UnaVidaMenys();

        }
    }
}
