using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casa : MonoBehaviour
{
    private AudioSource audioXoc;
    private GameSetUp GS;

    private void Start()
    {
        audioXoc = GetComponent<AudioSource>();
        GS = FindObjectOfType<GameSetUp>();
    }

    /// <summary>
    /// Funció per detectar quan una bola de l'atac ha xocat contra la casa
    /// </summary>
    /// <param name="collision">bola d'atac que ha col·lisionat</param>
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            audioXoc.PlayOneShot(audioXoc.clip, 1f);
            //Es resta una vida
            FindObjectOfType<Defensor>().UnaVidaMenys();
        }
    }
}
