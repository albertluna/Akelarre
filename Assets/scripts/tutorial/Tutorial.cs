using UnityEngine;
using System.Collections;
using Photon.Pun;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private GameSetUp GS;

    [SerializeField]
    private GameObject tutorialDefensor;
    [SerializeField]
    private GameObject tutorialRecollector;
    [SerializeField]
    private GameObject tutorialConstructor;
    [SerializeField]
    private GameObject boto;

    public void MostrarTutorial() { 
        Time.timeScale = 0f;
        this.gameObject.SetActive(true);
        if (GS.constructor != null) tutorialConstructor.SetActive(GS.constructor.photonView.IsMine);
        if (GS.recollector != null) tutorialRecollector.SetActive(GS.recollector.photonView.IsMine);
        if (GS.defensor != null) tutorialDefensor.SetActive(GS.defensor.photonView.IsMine);
        boto.SetActive(PhotonNetwork.IsMasterClient);
    }
    /// <summary>
    /// Funcio per tancar els tutorials
    /// </summary>
    public void Comencar()
    {
        Debug.Log("Controla? " + GS.gameObject.name);
        Debug.Log("Controla? " + GS.QuiControla().gameObject.name);

        GS.QuiControla().DestruirTutorial();

    }

    /*public void EsborrarHUDTutorial()
    {
        Destroy(HudTutorial);
        Destroy(this.gameObject);
    }*/
}
