using UnityEngine;

namespace SPTarkov.TestModule.Classes
{
    public class TeleportMarker : MonoBehaviour
    {
        private LineRenderer myRenderer;
        public Color Line = Color.yellow;

        public void Start()
        {
            name = "nexus#4880_teleport_marker";
            myRenderer = gameObject.AddComponent<LineRenderer>();
            myRenderer.enabled = false;
            myRenderer.startColor = Line;
            myRenderer.endColor = Line;
        }
    }
}
