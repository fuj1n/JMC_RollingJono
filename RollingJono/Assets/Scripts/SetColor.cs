using UnityEngine;

public class SetColor : MonoBehaviour {
    public Color color;

    private void Start()
    {
        GetComponentInChildren<Renderer>().material.color = color;
    }
}
