using UnityEngine;
using System.Collections;
using Photon.Pun;

public class recollectorController : MonoBehaviourPunCallbacks// , IPunObservable
{
    public GameObject RC;
    public ColleccionableCreators[] creators;
    public float timer;
    public float maxEspera;
    public float minEspera;
    public PhotonView PV;

    public Colleccionable[] colleccionables;
    public static Colleccionable[] llistaColleccionables;

    [SerializeField]
    private GameObject[] eliminar;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        RC = GameObject.Find("RecollectorController");
        creators = RC.GetComponentsInChildren<ColleccionableCreators>();
        colleccionables = RC.GetComponentsInChildren<Colleccionable>();
        
        llistaColleccionables = colleccionables;
        float percentatgeTotal = 0;
        foreach(Colleccionable col in colleccionables)
        {
            percentatgeTotal += col.percentatge;
        }
        if (percentatgeTotal != 100) Debug.LogError("El percentatge total dels colleccionables no suma 100, suma" + percentatgeTotal);
        //set index de cada creator
        for (int i = 0; i < creators.Length; i++)
        {
            creators[i].index = i;
        }
        if (!PV.IsMine)
        {
            foreach (GameObject go in eliminar) Destroy(go);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            if (timer >= 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                int resultat = Random.Range(0, 99);
                int percentatgeAnterior = 0;
                int posicio = Random.Range(0, creators.Length - 1);

                //comprovar que la nova posicio no estigui ocupada
                while (creators[posicio].estaOcupat)
                {
                    posicio = Random.Range(0, creators.Length - 1);
                }

                //escollir quin dels diferents tipus de colleccionables es crearà
                foreach (Colleccionable col in colleccionables)
                {
                    if (resultat >= percentatgeAnterior && resultat < (col.percentatge + percentatgeAnterior))
                    {
                        PV.RPC("RPC_crearColleccionable", RpcTarget.All, posicio, col.color);
                    }
                    percentatgeAnterior += col.percentatge;
                }
                timer = Random.Range(minEspera, maxEspera);
            }
        }
    }

    public static Colleccionable escollirColleccionable(string color)
    {
        foreach (Colleccionable col in llistaColleccionables)
        {
            if (col.color.Equals(color)) { return col; }
        }
        return null;
    }

    [PunRPC]
    private void RPC_crearColleccionable(int posicio, string color)
    {
        //Debug.Log("L'index es " + posicio);
        this.creators[posicio].Instantiate(escollirColleccionable(color), PV.IsMine);
    }

    public int indexColleccionable(GameObject colleccionable)
    {
        int i = 0;
        Colleccionable c = colleccionable.GetComponent<Colleccionable>();
        foreach (ColleccionableCreators index in creators)
        {           
            //GameObject fill = index.gameObject.GetComponentInChildren<Colleccionable>().gameObject;
            if(index == c.parent)
            {
                return i;
            }
            i++;
        }
        return -1;
    }

    public void deleteColleccionable(int index)
    {
        PV.RPC("RPC_deleteColleccionable", RpcTarget.All, index);

    }

    [PunRPC]
    private void RPC_deleteColleccionable(int index)
    {
        creators[index].GetComponentInChildren<Colleccionable>().parent.estaOcupat = false;
        Destroy(creators[index].GetComponentInChildren<Colleccionable>().gameObject);
    }

}
