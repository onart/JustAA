using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Scenemover
{
    public static void MoveScene(string sc)
    {
        SceneManager.LoadScene(sc);
        SysManager.menuon = false;
    }
}
