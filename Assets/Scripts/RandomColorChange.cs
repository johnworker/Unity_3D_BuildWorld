using UnityEngine;

public class RandomColorChange : MonoBehaviour
{
    public Renderer[] targetRenderers; // Array of renderers to change color
    //public float interval = 2f; // Time interval for color change

    private void Start()
    {
        // Call the ChangeColor method repeatedly with the specified interval
        Invoke("ChangeColor", 0f);
    }

    private void ChangeColor()
    {
        foreach (Renderer renderer in targetRenderers)
        {
            float randomHue = Random.Range(0f, 1f);
            Color newColor = Color.HSVToRGB(randomHue, 1f, 1f);
            renderer.material.color = newColor;
        }
    }
}