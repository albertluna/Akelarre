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
        Debug.Log("spawnpicker = " + Rol);
            switch (Rol) {
                case DEFENSOR:
                    spawnPicker = 0;//0
                    PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerDefensorVariant"),
                    GS.spawnPoints[spawnPicker].position, GS.spawnPoints[spawnPicker].rotation, 0);
                    break;
                case RECOLLECTOR: //1
                Debug.Log("Instanciem recollector");

                spawnPicker = 1;
                    PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerRecollectorVariant"),
                    GS.spawnPoints[spawnPicker].position, GS.spawnPoints[spawnPicker].rotation, 0);
                    Debug.Log("Estic instanciant bé? " + myAvatar.gameObject.name);
                    break;
                case CONSTRUCTOR:
                    spawnPicker = 2;
                    PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerConstructorVariant"),
                    GS.spawnPoints[spawnPicker].position, GS.spawnPoints[spawnPicker].rotation, 0);
                    break;
        }
        GS.getRols();

    }
}
