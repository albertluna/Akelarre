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
        int spawnPicker = Random.Range(0, GameSetUp.GS.spawnPoints.Length);
        Debug.Log("spawnpicker = " + spawnPicker);
        if (PV.IsMine)
        {
            myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"),
                GameSetUp.GS.spawnPoints[spawnPicker].position, GameSetUp.GS.spawnPoints[spawnPicker].rotation, 0);
        }
    }
    
}
