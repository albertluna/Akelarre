using UnityEngine;
using System.Collections;

public class recollectorController : MonoBehaviour
{
    public ColleccionableCreators[] creators;
    public float timer;
    public float maxEspera;
    public float minEspera;

    public Colleccionable[] colleccionables;
    void Start()
    {
        creators = GetComponentsInChildren<ColleccionableCreators>();
        colleccionables = GetComponentsInChildren<Colleccionable>();
        float percentatgeTotal = 0;
        foreach(Colleccionable col in colleccionables)
        {
            percentatgeTotal += col.percentatge;
        }
        if (percentatgeTotal != 100) Debug.LogError("El percentatge total dels colleccionables no suma 100, suma" + percentatgeTotal);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            int resultat = Random.Range(0, 99);
            int percentatgeAnterior = 0;
            int posicio = Random.Range(0, creators.Length-1);

            //comprovar que la nova posicio no estigui ocupada
            while(creators[posicio].estaOcupat)
            {
                posicio = Random.Range(0, creators.Length-1);
            }

            //escollir quin dels diferents tipus de colleccionables es crearà
            foreach (Colleccionable col in colleccionables)
            {
                Debug.Log("percentatge " + percentatgeAnterior + " i col.per = " + col.percentatge);
                if(resultat >= percentatgeAnterior && resultat < (col.percentatge+percentatgeAnterior))
                {
                    creators[posicio].Instantiate(col);
                }
                percentatgeAnterior += col.percentatge;
            }
            timer = Random.Range(minEspera, maxEspera);
        }
    }
}
