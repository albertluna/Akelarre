using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ScenesManager {

    public enum Scene
    {
        Prototip,
        MenuMultijugador,
        MapaNivells,
        Nivell1,
        Nivell2,
        Nivell3
    }

    public static void Load(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public static string GetScene(Scene scene)
    {
        return scene.ToString();
    }
}
