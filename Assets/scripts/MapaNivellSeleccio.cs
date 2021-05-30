using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MapaNivellSeleccio : MonoBehaviourPunCallbacks, IInRoomCallbacks {

    public static MapaNivellSeleccio room;
    private PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTutorialButtonClicked()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        if (PhotonNetwork.IsMasterClient)
        {
            
            PhotonNetwork.LoadLevel(ScenesManager.GetScene(ScenesManager.Scene.Constructor));
        } else
        {
            PhotonNetwork.LoadLevel(ScenesManager.GetScene(ScenesManager.Scene.Prototip));
        }
    }



}
