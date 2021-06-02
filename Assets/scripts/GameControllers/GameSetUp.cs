using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetUp : MonoBehaviour
{
    public static GameSetUp GS;
    public PhotonPlayer player;

    public Transform[] spawnPoints;

    void Start()
    {
        Debug.Log("get photon player");
        //player = GameObject.Find("RoomController").GetComponent<RoomController>().photonPlayer;
        //player.Instantiate();
    }

    private void OnEnable()
    {
        Debug.Log("Enabelitzar");
        if(GameSetUp.GS = null)
        {
            GameSetUp.GS = this;
        }
    }

    public int getSpawnpointLength()
    {
        return spawnPoints.Length;
    }
}
