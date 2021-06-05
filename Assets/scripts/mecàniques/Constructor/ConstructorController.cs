using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructorController : MonoBehaviour
{
    public static ConstructorController constructorController;
    public GameObject creadorColleccionables;
    public GameObject colleccionable;
    public GameObject colleccionablesExterns;
    public Pocio pocio;
    public HUD_Constructor hud;
    public GameObject[] llistaColleccionables;
    public ConnexioConsRec connexio;

    // Start is called before the first frame update
    void Start()
    {
        pocio.Comencar();
        constructorController = this;
        connexio = GetComponent<ConnexioConsRec>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.F))
        {
            NouColleccionable();
        }
    }

    public void NouColleccionable()
    {
        if (colleccionable != null)
        {
            Destroy(colleccionable);
        }
        colleccionable = Instantiate(colleccionablesExterns, creadorColleccionables.transform);
    }

    public void CrearColleccionable(string nouColleccionable)
    {
        Debug.Log("NOVA COLLECIONABLE");
        constructorController.NouColleccionable(recollectorController.escollirColleccionable(nouColleccionable));
    }

    public void NouColleccionable(Colleccionable nouColleccionable)
    {
        if (colleccionable != null)
        {
            Destroy(colleccionable);
        }
        colleccionable = Instantiate(nouColleccionable.gameObject, creadorColleccionables.transform);
    }

    public void ClicarMaterial()
    {
        if (colleccionable != null)
        {
            Debug.Log("MAterial CLicat - desplaçar a la pocio i seguir camí");
            if (pocio.esCollecicionableCorrecte(colleccionable.GetComponent<Colleccionable>()))
            {
                pocio.Seguent();
            }
            else
            {
                pocio.Comencar();
            }
            hud.actualitzarProgres();
            Destroy(colleccionable);
        }
    }
}
