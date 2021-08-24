using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class RolController : MonoBehaviour
{
    public PhotonView photonView;
    public GameSetUp gameSetup;

    [SerializeField]
    private GameObject[] eliminar;

    protected virtual void Start()
    {
        photonView = GetComponent<PhotonView>();
        gameSetup = FindObjectOfType<GameSetUp>();
        gameSetup.GetRols();

        //S'eliminen tots els objectes innecessaris de cada personatge
        //que no està sent controlats pel jugador com la càmera o el hud
        if (!photonView.IsMine)
        {
            foreach (GameObject go in eliminar) Destroy(go);
        }
    }
}
