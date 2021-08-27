using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Defensor : MonoBehaviour
{
    [SerializeField]
    private int vides;
    [SerializeField]
    private AtacController controller;
    
    [SerializeField]
    private AudioClip destrossarBoles;
    [SerializeField]
    private AudioClip destrossarColleccionables;

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))//&&PV.IsMine)
        {
            GetComponent<AudioSource>().PlayOneShot(destrossarBoles, 1f);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Colleccionable") && controller.photonView.IsMine)
        {
            Colleccionable colleccionable = collision.GetComponentInParent<Colleccionable>();
            colleccionable.parent.GetRecollector().EliminarColleccionable(colleccionable);
            GetComponent<AudioSource>().PlayOneShot(destrossarColleccionables, 1f);
        }
    } 
}
