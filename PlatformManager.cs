using UnityEngine;
using System.Collections.Generic;

public class PlatformManager : MonoBehaviour
{
    public GameObject[] platformPrefabs; 
    public Transform player; 
    public float spawnDistance = 55f;
    public float fadeDuration = 5f;
    public int platformsToSpawn = 55; 
    private float lastSpawnedX = 0f;
    private int currentLevel = 0;

    void Start()
    {
        for (int i = 0; i < platformsToSpawn; i++)
        {
            SpawnPlatform();
        }
    }

    void Update()
    {
        
        if (player.position.x + spawnDistance > lastSpawnedX)
        {
            SpawnPlatform();
        }
        if (player.position.x > 50 && currentLevel == 0) 
        {
            currentLevel = 1;
            StartCoroutine(FadeOut(platformPrefabs[0]));
        }
        else if (player.position.x > 100 && currentLevel == 1)
        {
            currentLevel = 2;
            StartCoroutine(FadeOut(platformPrefabs[0]));
        }
        else if (player.position.x > 150 && currentLevel == 2)
        {
            StartCoroutine(FadeOut(platformPrefabs[1]));
            Debug.Log("Game Over! You reached the end of Level 3.");
            Time.timeScale = 0; 
        }
    }

    void SpawnPlatform()
    {    
        GameObject platform = Instantiate(platformPrefabs[0]);
        float platformWidth = platform.GetComponent<SpriteRenderer>().bounds.size.x;
        platform.transform.position = new Vector3(lastSpawnedX + platformWidth / 2, 0, 0);
        platform = Instantiate(platformPrefabs[1]);
        platformWidth = platform.GetComponent<SpriteRenderer>().bounds.size.x;
        platform.transform.position = new Vector3(lastSpawnedX + platformWidth / 2, 0, 0);
        platform = Instantiate(platformPrefabs[2]);
        platformWidth = platform.GetComponent<SpriteRenderer>().bounds.size.x;
        platform.transform.position = new Vector3(lastSpawnedX + platformWidth / 2, 0, 0);
        lastSpawnedX += platformWidth;
    }

System.Collections.IEnumerator FadeOut(GameObject platform)
{
    // Get the SpriteRenderer of the platform
    SpriteRenderer spriteRenderer = platform.GetComponent<SpriteRenderer>();

    if (spriteRenderer == null)
    {
        Debug.LogWarning("No SpriteRenderer found on the platform!");
        yield break; // Exit if no SpriteRenderer is present
    }

    // Store the original color of the sprite
    Color originalColor = spriteRenderer.color;
    float elapsedTime = 0f;

    // Gradually reduce the alpha value to fade out
    while (elapsedTime < fadeDuration)
    {
        elapsedTime += Time.deltaTime;
        float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration); // Interpolate alpha
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha); // Apply new color
        yield return null; // Wait for the next frame
    }

    // Ensure the sprite is fully transparent at the end
    spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

    // Optionally destroy the platform after fading
   //Destroy(platform);
}

}