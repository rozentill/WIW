using UnityEngine;
using System.Collections;

public static class GlobalData {

    public static bool detectInput = true;
    public static int score = 0;

    public static bool getHit = false;
    public static bool slowDown = false;
    public static bool stopFly = false;

    public static int result = 0;

    public static float tutorialWaitTime = .1f;

    public static float noteTolerate = 70f;

    public static int successNoteCount = 0;
    public static int requireNoteCount = 10;

    public static void Init()
    {
        detectInput = true;
        score = 0;

        getHit = false;
        slowDown = false;
        stopFly = false;

        result = 0;

        successNoteCount = 0;
    }
}
