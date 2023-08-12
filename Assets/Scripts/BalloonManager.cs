using System.Collections;
using UnityEngine;

public class BalloonManager : MonoBehaviour
{
    public GameObject balloonPrefab;
    public Vector3 spawnAreaSize = new Vector3(5f, 5f, 5f);
    public float minScaleFactor = 16f; // Minimum scale as a factor of original scale
    public float maxScaleFactor = 24f;  // Maximum scale as a factor of original scale
    public float interval = 3f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }

    private void Start()
    {
        StartCoroutine(GenerateBalloons());
    }

    private IEnumerator GenerateBalloons()
    {
        while (true)
        {
            GenerateBalloon();
            yield return new WaitForSeconds(interval);
        }
    }

    private void GenerateBalloon()
    {
        Vector3 randomPosition = GetRandomPositionInSpawnArea();
        GameObject newBalloon = Instantiate(balloonPrefab, randomPosition, Quaternion.Euler(-90f, 0f, 0f));

        // Set random scale for the balloon
        float randomScaleFactor = Random.Range(minScaleFactor, maxScaleFactor);
        Vector3 randomScale = Vector3.one * randomScaleFactor;
        newBalloon.transform.localScale = randomScale;


        Rigidbody balloonRigidbody = newBalloon.GetComponent<Rigidbody>();
        if (balloonRigidbody != null)
        {
            balloonRigidbody.velocity = Vector3.up * 2f; // You can adjust the velocity as needed
        }

        Renderer balloonRenderer = newBalloon.GetComponent<Renderer>();
        if (balloonRenderer != null)
        {
            float randomHue = Random.Range(0f, 1f);
            Color newColor = Color.HSVToRGB(randomHue, 1f, 1f);
            balloonRenderer.material.color = newColor;
        }

        // Destroy the balloon when it goes above the screen
        StartCoroutine(DestroyBalloon(newBalloon));
    }

    private Vector3 GetRandomPositionInSpawnArea()
    {
        Vector3 center = transform.position;
        Vector3 halfSize = spawnAreaSize / 2;

        float randomX = Random.Range(center.x - halfSize.x, center.x + halfSize.x);
        float randomY = Random.Range(center.y - halfSize.y, center.y + halfSize.y);
        float randomZ = Random.Range(center.z - halfSize.z, center.z + halfSize.z);

        return new Vector3(randomX, randomY, randomZ);
    }

    private IEnumerator DestroyBalloon(GameObject balloon)
    {
        Camera mainCamera = Camera.main; // 獲取主攝影機

        while (true)
        {
            Vector3 screenPos = mainCamera.WorldToViewportPoint(balloon.transform.position);

            if (screenPos.y < 0 || screenPos.y > 1)
            {
                Destroy(balloon);
                break;
            }

            yield return null;
        }
    }
}