using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Recollector : MonoBehaviour
{
    //Nombre de vides que té el recol·lector
    private int nombreVides;
    [SerializeField]
    private PhotonView PV;
    [SerializeField]
    private RecollectorController rc;
    
    void Start()
    {
        nombreVides = rc.NombreVides();
    }

    /// <summary>
    /// Funció que gestiona quan el recol·lector toca un col·leccionable
    /// </summary>
    /// <param name="collision">Col·leccionable recollit</param>
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Colleccionable") && PV.IsMine)
        {
            Colleccionable colleccionable = collision.gameObject.GetComponentInParent<Colleccionable>();

            //ConstructorController constructor = FindObjectOfType<ConstructorController>();
            //S'envia el col·leccionable al constructor
            rc.GS.constructor.EnviarColleccionable(colleccionable.color);

            //S'elimina el col·leccionable
            int index = rc.indexColleccionable(colleccionable.gameObject);
            if (index == -1) Debug.LogError("Fail");

            rc.deleteColleccionable(index);
        }
    }

    /// <summary>
    /// Funció que gestiona quan el recol·lector toca una bola de l'atac
    /// </summary>
    /// <param name="collision">Bola d'atac</param>
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && nombreVides > 0)
        {
            nombreVides--;
            rc.ActualitzarVides(nombreVides);
            Destroy(collision.gameObject);
        }
    }
}
