using UnityEngine;
using System.Collections;

public class recollectorController : MonoBehaviour
{
    public Transform[] creators;
    public float timer;
    public float maxEspera;
    public float minEspera;

    public Colleccionable[] colleccionables;

    void Start()
    {
        creators = GetComponentsInChildren<Transform>();
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
            int resultat = Random.Range(0, 100);
            int percentatgeAnterior = 0;
            int posicio = Random.Range(1, creators.Length);

            foreach (Colleccionable col in colleccionables)
            {
                if(resultat >= percentatgeAnterior && resultat < (col.percentatge+percentatgeAnterior))
                {
                    Instantiate( col, creators[posicio].position, Quaternion.identity);

                }
            }
            timer = Random.Range(minEspera, maxEspera);
        }
    }
}
