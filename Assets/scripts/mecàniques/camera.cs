using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    [SerializeField]
    private GameObject jugador;
    private float yOriginal;
    [SerializeField]
    private float movimentSuau;
    private float offset;

    void Awake()
    {
        yOriginal = this.transform.position.y;
        //l'offset és la distància entre la càmera i el jugador que sempre es matindrà
        offset = Vector3.Distance(this.transform.position, jugador.transform.position);
    }

#if UNITY_STANDALONE
    void FixedUpdate()
    {
        //es calcula la direcció del jugador respecte la casa
        Vector3 dir = jugador.transform.position.normalized;
        //es calcula la distància
        float distanciaAlCentre = Vector3.Distance(jugador.transform.position, new Vector3(0, 0, 0));
        //es suma un offset per calcular la distància de la càmera al centre
        float radi = distanciaAlCentre + offset;
        //es fa una interposció entre la posició antiga de la càmera i on hauria d'anar per tenir un moviment susu
        Vector3 posicioDesitjada = new Vector3(dir.x*radi, yOriginal+(radi), dir.z*radi); //posar a y distanciaAlCentre
        Vector3 posicioIntermitja = Vector3.Lerp(this.transform.position, posicioDesitjada, movimentSuau);
        transform.position = posicioIntermitja;
        transform.LookAt(jugador.transform.position);
    }
#endif
#if UNITY_ANDROID
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
