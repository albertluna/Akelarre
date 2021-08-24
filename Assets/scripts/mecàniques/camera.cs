using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    //Punter al persontge que mira la càmera i mou el jugador
    [SerializeField]
    private Moviment jugador;
    //Alçada inicial de la càmera
    private float yOriginal;
    //Sensibilitat del moviment de la càmera al moure's
    [SerializeField]
    private float movimentSuau;
    //Distància entre la càmera i el personatge
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
        //es fa una interposció entre la posició antiga de la càmera i on hauria d'anar per tenir un moviment suau
        Vector3 posicioDesitjada = new Vector3(dir.x*radi, yOriginal+(radi), dir.z*radi);
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

    public void SetJugador(Moviment jugador) { this.jugador = jugador;}
}
