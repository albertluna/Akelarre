using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameSetUp : MonoBehaviour
{
    public PhotonPlayer player;

    public ConstructorController constructor;
    public Defensor defensor;
    public Recollector recollector;

    public int vides;

    public GameObject GameOver;
    public GameObject Victory;


    public Transform[] spawnPoints;

    void Start()
    {
        GameOver.SetActive(false);
        Victory.SetActive(false);
    }


    public int getSpawnpointLength() { return spawnPoints.Length; }

    public void getRols()
    {
        constructor = FindObjectOfType<ConstructorController>();
        defensor = FindObjectOfType<Defensor>();
        recollector = FindObjectOfType<Recollector>();
    }

    public void restaVida()
    {
        vides--;
        if (vides == 0)
        {
            PartidaPerduda();
        }
    }

    public void PartidaPerduda()
    {
        Debug.Log("GAME OVER");
        Time.timeScale = 0;
        GameOver.SetActive(true);
    }

    public void PartidaGuanyada()
    {
        Debug.Log("Victoria");
        Time.timeScale = 0;
        Victory.SetActive(true);
    }

    public void OnBotoVictoria()
    {
        PhotonNetwork.LoadLevel(ScenesManager.GetScene(ScenesManager.Scene.MapaNivells));

    }
}
