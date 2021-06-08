using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class atacController : MonoBehaviour
{
    public GameObject AC;

    public Transform[] creators;
    public GameObject bullet;
    public float timer;
    public float maxEspera;
    public float minEspera;
    //public GameObject llistaCreators;
    public GameObject instanciador;
    [SerializeField]
    private PhotonView PV;
    public Defensor defensor;

    /// <summary> array de gameobjects
    /// https://stuartspixelgames.com/2017/08/02/make-all-of-objects-children-into-an-array-unity-c/
    /// </summary>
    /// 
    void Start()
    {
        AC = GameObject.Find("creadorsAtac");
        instanciador = GameObject.Find("instanciador");
        creators = AC.GetComponentsInChildren<Transform>();
        PV = GetComponent<PhotonView>();
        defensor = GetComponent<Defensor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine) {
            //https://stackoverflow.com/questions/33182283/how-do-i-create-random-game-objects-at-runtime-in-unity-2d-using-c
            if (timer >= 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                int posicio = Random.Range(1, creators.Length);
                PV.RPC("RPC_instanciarAtac", RpcTarget.All, posicio);
                timer = Random.Range(minEspera, maxEspera);
            }
        }
    }

    [PunRPC]
    private void RPC_instanciarAtac(int posicio)
    {
        GameObject instancia = Instantiate(bullet, creators[posicio].position, Quaternion.identity, instanciador.transform);
        instancia.GetComponent<MovimentAtac>().defensor = defensor;
        Debug.Log("INSTANCIAT");
    }
}
