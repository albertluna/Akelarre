using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderPocio : MonoBehaviour
{
    public Slider mascara;
    private Image[] llistaColors;
    [SerializeField]
    private Image UIInicial;

    [SerializeField]
    private Transform parent;

    private void Awake()
    {
        mascara = GetComponent<Slider>();
    }
    public void PintarLlistaPocio(Pocio pocio)
    {
        llistaColors = new Image[pocio.GetLlargadaLlista()];
        for(int i = 0; i < pocio.GetLlargadaLlista(); i++)
        {
            InstanciarUIColor(i, pocio.GetLlargadaLlista());
            //Pintar del color de la pocio
            llistaColors[i].color = GetColor(pocio.GetLlista()[i].color);
        }
        Destroy(UIInicial.gameObject);
    }

    private void InstanciarUIColor(int i, int llargadaLlista)
    {
        llistaColors[i] = Instantiate(UIInicial, parent.position,
                Quaternion.identity, parent).GetComponent<Image>();
        float midaUIColor = (float) 1/llargadaLlista;

        RectTransform posicioColor = llistaColors[i].GetComponent<RectTransform>();
        posicioColor.anchorMin = new Vector2(0, (llargadaLlista - i) * midaUIColor - midaUIColor);
        posicioColor.anchorMax = new Vector2(1, (llargadaLlista - i) * midaUIColor);
        posicioColor.offsetMax = new Vector2(0,0);
        posicioColor.offsetMin = new Vector2(0,0);
    }

    public void SetMaxValueMascara(int value) { mascara.maxValue = value; }
    public void SetValueMascara(int value) { mascara.value = value; }

    //TODO: Millorar tema reconeixer color a l'hora de pintar
    private Color GetColor(string color)
    {
        switch (color)
        {
            case "BLAU":
                return Color.blue;
                break;
            case "GROC":
                return Color.yellow;
                break;
            case "LILA":
                return Color.magenta;
                break;
            case "VERD":
                return Color.green;
                break;
            case "VERMELL":
                return Color.red;
                break;
            default:
                return Color.white;
                break;
        }
    }
    
}
