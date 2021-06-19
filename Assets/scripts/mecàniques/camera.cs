using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject jugador;
    public float yOriginal;
    public float movimentSuau = 0.125f;
    public float radi;
    private float offset;
    //private Animation cinematiques;
    //private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        //cinematiques = GetComponent<Animation>();
        yOriginal = this.transform.position.y;
        offset = Vector3.Distance(this.transform.position, jugador.transform.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 dir = jugador.transform.position.normalized;
        float distanciaAlCentre = Vector3.Distance(jugador.transform.position, new Vector3(0, 0, 0));
        radi = distanciaAlCentre + offset;
        Vector3 posicioDesitjada = new Vector3(dir.x*radi, distanciaAlCentre, dir.z*radi);
        Vector3 posicioIntermitja = Vector3.Lerp(this.transform.position, posicioDesitjada, movimentSuau);
        transform.position = posicioIntermitja;
        transform.LookAt(jugador.transform);
    }

    public void setJugador(GameObject jugador)
    {
        this.jugador = jugador;
    }
}
