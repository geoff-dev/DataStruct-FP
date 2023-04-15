using UnityEngine;

public class FPSCounter : MonoBehaviour {
    [SerializeField] float updateInterval = 0.5f; //How often should the number update
    float accum = 0.0f;
    int frames = 0;
    float timeleft;
    float fps;

    GUIStyle textStyle = new GUIStyle();

    void Start() {
        timeleft = updateInterval;
        textStyle.fontStyle = FontStyle.Bold;
        textStyle.normal.textColor = Color.white;
    }

    void Update() {
        float dt = Time.deltaTime;
        timeleft -= dt;
        accum += Time.timeScale / dt;
        ++frames;

        // Interval ended - Update GUI text and start new interval
        if (timeleft >= 0.0) 
            return;
        // Display two fractional digits (f0 format)
        fps = (accum / frames);
        timeleft = updateInterval;
        accum = 0.0f;
        frames = 0;
    }

    void OnGUI() {
        // Display the fps and round to 0 decimals
        string fpsString = $"FPS: {fps:F0}";
        GUI.Label(new Rect(5, 5, 100, 25), fpsString, textStyle);
    }
}