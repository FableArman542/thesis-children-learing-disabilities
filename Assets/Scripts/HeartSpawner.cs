using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSpawner : MonoBehaviour
{

    public GameObject heartPrefab;  // Drag and drop the heart prefab in the Unity editor

    [SerializeField]
    private float spawnRate = 1f;    // How often to spawn hearts (in seconds)
    private float lastSpawnTime;    // Time when the last heart was spawned
    private float minX = -2.5f;      // Minimum x position for the heart spawn
    private float maxX = 2.5f;       // Maximum x position for the heart spawn
    private float minY = -2.5f;      // Minimum y position for the heart spawn
    private float maxY = 2.5f;       // Maximum y position for the heart spawn


    void Start()
    {
        lastSpawnTime = Time.time;  // Set the last spawn time to the current time
        GetCameraBounds(Camera.main, out minX, out maxX, out minY, out maxY);
    }

    void Update()
    {
        // Check if it's time to spawn a new heart
        if (Time.time - lastSpawnTime > spawnRate)
        {
            lastSpawnTime = Time.time;  // Update the last spawn time

            // Generate a random position within the specified range
            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);
            Vector3 position = new Vector3(x, y, 0f);

            // Spawn the heart at the random position
            var spawnedHeart = Instantiate(heartPrefab, position, Quaternion.identity);
            LeanTween.scale(spawnedHeart, Vector3.one, .8f);
        }
    }

    public static void GetCameraBounds(Camera camera, out float minX, out float maxX, out float minY, out float maxY)
    {
        // Get the size of the camera's viewport in world units
        Vector2 cameraSize = camera.ViewportToWorldPoint(new Vector3(1f, 1f, 0f)) - camera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));

        // Calculate the minimum and maximum x and y positions based on the camera's position and size
        minX = camera.transform.position.x - cameraSize.x / 2f;
        maxX = camera.transform.position.x + cameraSize.x / 2f;
        minY = camera.transform.position.y - cameraSize.y / 2f;
        maxY = camera.transform.position.y + cameraSize.y / 2f;
    }

}
