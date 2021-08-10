using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject jugador;
    public float yOriginal;
    public float movimentSuau;
    public float radi;
    public float offset;

    // Start is called before the first frame update
    void Awake()
    {
        yOriginal = this.transform.position.y;
        offset = Vector3.Distance(this.transform.position, jugador.transform.position);
        Vector3 dir = jugador.transform.position.normalized;

        transform.position = new Vector3(dir.x * radi, yOriginal + (radi), dir.z * radi);
    }
#if UNITY_STANDALONE

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 dir = jugador.transform.position.normalized;
        float distanciaAlCentre = Vector3.Distance(jugador.transform.position, new Vector3(0, 0, 0));
        radi = distanciaAlCentre + offset;
        Vector3 posicioDesitjada = new Vector3(dir.x*radi, yOriginal+(radi), dir.z*radi); //posar a y distanciaAlCentre
        Vector3 posicioIntermitja = Vector3.Lerp(this.transform.position, posicioDesitjada, movimentSuau);
        transform.position = posicioIntermitja;
        transform.LookAt(jugador.transform.position);
    }
#endif
#if UNITY_ANDROID
// Update is called once per frame
    void Update()
    {
        Vector3 dir = jugador.transform.position.normalized;
        float distanciaAlCentre = Vector3.Distance(jugador.transform.position, new Vector3(0, 0, 0));
        radi = distanciaAlCentre + offset;
        Vector3 posicioDesitjada = new Vector3(dir.x*radi, yOriginal+(radi), dir.z*radi); //posar a y distanciaAlCentre
        Vector3 posicioIntermitja = Vector3.Lerp(this.transform.position, posicioDesitjada, movimentSuau);
        transform.position = posicioIntermitja;
        transform.LookAt(jugador.transform.position);
    }
#endif

    public void setJugador(GameObject jugador)
    {
        this.jugador = jugador;
    }
}
