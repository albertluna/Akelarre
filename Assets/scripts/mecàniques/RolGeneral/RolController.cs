using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class RolController : MonoBehaviour
{
    public PhotonView PV;

    [SerializeField]
    protected GameSetUp GS;

    [SerializeField]
    private GameObject[] eliminar;

    protected virtual void Start()
    {
        PV = GetComponent<PhotonView>();
        GS = FindObjectOfType<GameSetUp>();

        if (!PV.IsMine)
        {
            foreach (GameObject go in eliminar) Destroy(go);
        }
    }

    
}
