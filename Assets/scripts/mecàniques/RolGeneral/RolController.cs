using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class RolController : MonoBehaviour
{
    public PhotonView PV;
    public GameSetUp GS;

    [SerializeField]
    private GameObject[] eliminar;

    protected virtual void Start()
    {
        PV = GetComponent<PhotonView>();
        GS = FindObjectOfType<GameSetUp>();
        GS.getRols();

        //S'eliminen tots els objectes innecessaris de cada personatge
        //que no està sent controlats pel jugador com la càmera o el hud
        if (!PV.IsMine)
        {
            foreach (GameObject go in eliminar) Destroy(go);
        }
    }
}
