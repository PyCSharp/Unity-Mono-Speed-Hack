using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace CheatDLL
{
    public class Cheat : MonoBehaviour
    {
        private Rect windowRect = new Rect(100, 100, 300, 75);
        public float hSliderValue = 1.0F;
        public bool speedChanged;

        void OnGUI()
        {
            windowRect = GUI.Window(0, windowRect, DrawWindow, "Speed Hack");
        }

        void DrawWindow(int windowID)
        {
            GUI.Label(new Rect(140, 25, 100, 20), Mathf.Round(hSliderValue).ToString());
            hSliderValue = GUI.HorizontalSlider(new Rect(50, 40, 200, 100), hSliderValue, 0.0F, 100.0F);


            GUI.DragWindow(new Rect(0, 0, 10000, 20));
        }

        public void Update()
        {
            Time.timeScale = hSliderValue;
        }
    }

    public class Loader
    {
        public static void Init()
        {
            GameObject gameObject = new GameObject("CheatObject");
            gameObject.AddComponent<Cheat>();
        }
    }
}