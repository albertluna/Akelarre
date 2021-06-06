using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ScenesManager {

    public enum Scene
    {
        Prototip,
        MenuMultijugador,
        MapaNivells
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
