using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetUp : MonoBehaviour
{
    public static GameSetUp GS;
    public PhotonPlayer player;

    public ConstructorController constructor;
    public Defensor defensor;
    public Recollector recollector;

    public int vides;


    public Transform[] spawnPoints;


    private void OnEnable()
    {
        Debug.Log("Enabelitzar");
        if(GameSetUp.GS = null)
        {
            GameSetUp.GS = this;
        }
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
            Debug.Log("GAME OVER");
        }
    }
}
