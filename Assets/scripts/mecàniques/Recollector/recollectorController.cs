using UnityEngine;
using System.Collections;
using Photon.Pun;

public class RecollectorController : RolController
{

    [Header("Elements a controlar")]
    //Llista de creadors de col·leccionables
    public ColleccionableCreators[] creators;
    private Recollector recollector;
    //Llista dels col·leccionables del nivell
    [SerializeField]
    private Colleccionable[] colleccionables;
    //Variable per determinar si les boles recol·lectores son invisibles pel recol·lector
    public bool isInvisible;
    //Referència a la gestió del temps
    private float timer;
    private float maxEspera;
    private float minEspera;
    private float minVida;
    private float maxVida;
    [Header("Gestió del HUD")]
    [SerializeField]
    private GameObject[] vides;

    protected override void Awake()
    {
        base.Awake();
        creators = gameSetup.llistesRecollector.GetComponentsInChildren<ColleccionableCreators>();
        colleccionables = gameSetup.llistesRecollector.GetComponentsInChildren<Colleccionable>();
        maxEspera = gameSetup.maxEsperaRecollecta;
        minEspera = gameSetup.minEsperaRecollecta;
        minVida = gameSetup.minVidaColleccionables;
        maxVida = gameSetup.maxVidaColleccionables;
        recollector = GetComponentInChildren<Recollector>();

        foreach(ColleccionableCreators cc in creators) { cc.SetRecollector(this); }

        //Comprovacio que la suma de percentatges de probabilitat de sortir un col·leccionable concret sigui del 100%
        float percentatgeTotal = 0;
        foreach(Colleccionable col in colleccionables) {
            percentatgeTotal += col.percentatge;
        }
        if (percentatgeTotal != 100)
            Debug.LogError("El percentatge total dels colleccionables no suma 100, suma" + percentatgeTotal);
        timer = minEspera;
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            if (timer >= 0) timer -= Time.deltaTime;
            else {
                int resultat = Random.Range(0, 99);
                int percentatgeAnterior = 0;
                int posicio = novaPosicio();
                if (posicio == -1) return;
                

                //escollir quin dels diferents tipus de colleccionables es crearà
                foreach (Colleccionable col in colleccionables)
                {
                    if (resultat >= percentatgeAnterior && resultat < (col.percentatge + percentatgeAnterior))
                    {
                        float tempsVida = Random.Range(minVida, maxVida);
                        //Funció per crear el colleccionable per tots els jugadors
                        photonView.RPC("RPC_CrearColleccionable", RpcTarget.All, posicio, col.color, tempsVida);
                    }
                    percentatgeAnterior += col.percentatge;
                }
                //Es torna a comptar el temps d'espera
                timer = Random.Range(minEspera, maxEspera);
            }
        }
    }

    private int novaPosicio()
    {
        int posicio = Random.Range(0, this.creators.Length - 1);
        //Es comprova si hi ha alguna posicio lliure per a crear un colleccionable
        bool posicionsOcupades = true;
        foreach(ColleccionableCreators creator in creators)
        {
            posicionsOcupades &= creator.estaOcupat;
            Debug.Log("El creador " + creator + " esta " + creator.estaOcupat + 
                " d ocupat, per tant la variable posicionsOcupades es " + posicionsOcupades);
        }
        if (posicionsOcupades) posicio = -1;

        //comprovar que la nova posicio no estigui ocupada
        while (creators[posicio].estaOcupat && !posicionsOcupades)
        {
            posicio = Random.Range(0, this.creators.Length - 1);
        }
        return posicio;
    }

    /// <summary>
    /// Funció encarregada de retornar el colleccionable del color d'entrada
    /// </summary>
    /// <param name="color">nom del color</param>
    /// <returns>Colleccionable del color introduït</returns>
    public Colleccionable EscollirColleccionable(string color)
    {
        foreach (Colleccionable col in colleccionables)
        {
            if (col.color.Equals(color)) return col;
        }
        return null;
    }

    /// <summary>
    /// Funció encarregada d'instanciar un col·leccionable en el mapa
    /// </summary>
    /// <param name="posicio">Posició on s'instancia de la llista de creadors</param>
    /// <param name="color">Color del col·leccionable a instanciar</param>
    [PunRPC]
    private void RPC_CrearColleccionable(int posicio, string color, float vida)
    {
        creators[posicio].Instantiate(EscollirColleccionable(color), photonView.IsMine, isInvisible);
        creators[posicio].GetFill().tempsVida = vida;
    }

    /// <summary>
    /// Funció per obtenir la posició del col·leccionable  dins l'array de creadors
    /// </summary>
    /// <param name="colleccionable">col·leccionable que es passa per referència</param>
    /// <returns>index dins l'array de creadors</returns>
    public int IndexColleccionable(GameObject colleccionable)
    {
        int i = 0;
        Colleccionable c = colleccionable.GetComponent<Colleccionable>();
        foreach (ColleccionableCreators index in creators)
        {           
            if(index == c.parent)
            {
                return i;
            }
            i++;
        }
        return -1;
    }

    /// <summary>
    /// Funció per gestionar quan el recol·lector ha agafat el col·leccionable i s'ha d'eliminar
    /// </summary>
    /// <param name="collecionable">Referència al col·leccionable a eliminar</param>
    public void EliminarColleccionable(Colleccionable collecionable)
    {
        int posicio = IndexColleccionable(collecionable.gameObject);
        photonView.RPC("RPC_EliminarColleccionable", RpcTarget.All, posicio);
    }

    /// <summary>
    /// Funció per eliminar per tots els jugadors el col·leccionable
    /// </summary>
    /// <param name="posicio">index dins l'array de creadors</param>
    [PunRPC]
    private void RPC_EliminarColleccionable(int posicio)
    {
        creators[posicio].estaOcupat = false;
        Destroy(creators[posicio].GetFill().gameObject);
    }

    #region HUD
    /// <summary>
    /// Funció per mostrar per pantalla la quantitat de vides que té el recol·lector
    /// </summary>
    /// <param name="nVides">Nombre de vides del recol·lector</param>
    public void ActualitzarVides(int nVides)
    {
        if (photonView.IsMine)
        {
            for (int i = 0; i < NombreVides(); i++)
            {
                if (i < nVides) vides[i].SetActive(true);
                else vides[i].SetActive(false);
            }
            if (nVides <= 0)
            {
                photonView.RPC("RPC_CanviarCapa", RpcTarget.All);
            }
        }
        
    }

    public int NombreVides() { return vides.Length; }

    [PunRPC]
    protected override void RPC_DestruirTutorial()
    {
        base.RPC_DestruirTutorial();
    }

    [PunRPC]
    private void RPC_CanviarCapa()
    {
        recollector.CanviarCapa();
    }
    #endregion
}
