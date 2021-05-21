using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;


public class DelayStartRoomController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int waitingRoomSceneIndex;

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
        base.OnEnable();
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
        base.OnDisable();
    }

    public override void OnJoinedRoom()
    {
        SceneManager.LoadScene(waitingRoomSceneIndex);
        base.OnJoinedRoom();
    }
}
