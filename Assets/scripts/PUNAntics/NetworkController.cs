using UnityEngine;
using System.Collections;
using Photon.Pun;

public class NetworkController : MonoBehaviourPunCallbacks
{
    // Use this for initialization
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); 
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Estem connectats al " + PhotonNetwork.CloudRegion + " servidor!");
        base.OnConnectedToMaster();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
