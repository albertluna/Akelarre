using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PhotonPlayer : MonoBehaviour
{
    public PhotonView PV;
    public GameObject myAvatar;
    public string Rol;

    public const string CONSTRUCTOR = "CONSTRUCTOR";
    public const string DEFENSOR = "DEFENSOR";
    public const string RECOLLECTOR = "RECOLLECTOR";

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        
    }

    public void Instantiate()
    {
        GameSetUp GS = FindObjectOfType<GameSetUp>();
        int spawnPicker = 0;
        Debug.Log("spawnpicker = " + spawnPicker);
        if (PV.IsMine)
        {
            switch (Rol) {
                case DEFENSOR:
                    spawnPicker = 0;//0
                myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerDefensorVariant"),
                    GS.spawnPoints[spawnPicker].position, GS.spawnPoints[spawnPicker].rotation, 0);
                    break;
                case RECOLLECTOR: //1
                    spawnPicker = 1;
                    myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerRecollectorVariant"),
                    GS.spawnPoints[spawnPicker].position, GS.spawnPoints[spawnPicker].rotation, 0);
                    break;
            }
        }
    }
    
}
