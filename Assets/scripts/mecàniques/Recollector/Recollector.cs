using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Recollector : MonoBehaviour
{
    //Nombre de vides que té el recol·lector
    private int nombreVides;
    [SerializeField]
    private RecollectorController recollectorController;
    
    void Start()
    {
        nombreVides = recollectorController.NombreVides();
    }

    /// <summary>
    /// Funció que gestiona quan el recol·lector toca un col·leccionable
    /// </summary>
    /// <param name="collision">Col·leccionable recollit</param>
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Colleccionable") && recollectorController.photonView.IsMine)
        {
            Colleccionable colleccionable = collision.gameObject.GetComponentInParent<Colleccionable>();

            //S'envia el col·leccionable al constructor
            recollectorController.gameSetup.constructor.EnviarColleccionable(colleccionable.color);

            //S'elimina el col·leccionable
            recollectorController.EliminarColleccionable(colleccionable);
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
            recollectorController.ActualitzarVides(nombreVides);
            Destroy(collision.gameObject);
        }
    }

    public void CanviarCapa()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Recollector");
    }
}
