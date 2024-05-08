using UnityEngine;

public class WordSelection : MonoBehaviour
{
    // Public variables
    public SpriteRenderer letterRenderer; // Reference to the SpriteRenderer component of the letter object
    public Color detectionColor = Color.red; // Color to highlight detected letters

    // Private variables
    private Texture2D letterTexture; // Texture of the letter object
    private bool[,] letterPixels; // 2D array to store pixel information of the letter

    void Start()
    {
        // Get the texture of the letter object
        if (letterRenderer != null && letterRenderer.sprite != null)
        {
            letterTexture = letterRenderer.sprite.texture;
            GetLetterPixels();
        }
        else
        {
            Debug.LogError("LetterRenderer or its sprite is not assigned.");
        }
    }

    void GetLetterPixels()
    {
        // Create a 2D array to store the pixel information of the letter
        letterPixels = new bool[letterTexture.width, letterTexture.height];

        // Loop through each pixel of the letter texture
        for (int x = 0; x < letterTexture.width; x++)
        {
            for (int y = 0; y < letterTexture.height; y++)
            {
                // Check if the pixel is opaque (not transparent)
                Color pixelColor = letterTexture.GetPixel(x, y);
                letterPixels[x, y] = (pixelColor.a > 0.5f); // Assuming alpha value greater than 0.5 is considered opaque
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has a SpriteRenderer component
        SpriteRenderer otherRenderer = other.GetComponent<SpriteRenderer>();
        if (otherRenderer != null && otherRenderer.sprite != null)
        {
            // Compare the pixel information of the colliding object with the letter object
            Texture2D otherTexture = otherRenderer.sprite.texture;
            bool[,] otherPixels = new bool[otherTexture.width, otherTexture.height];
            // Assuming the colliding object's texture is the same size as the letter object's texture

            for (int x = 0; x < otherTexture.width; x++)
            {
                for (int y = 0; y < otherTexture.height; y++)
                {
                    Color pixelColor = otherTexture.GetPixel(x, y);
                    otherPixels[x, y] = (pixelColor.a > 0.5f); // Again, assuming alpha value greater than 0.5 is considered opaque
                }
            }

            // Compare the pixels of the letter and colliding object
            bool letterDetected = ComparePixels(letterPixels, otherPixels);

            // If the letter is detected, change the color of the colliding object
            if (letterDetected)
            {
                otherRenderer.color = detectionColor;
            }
        }
    }

    bool ComparePixels(bool[,] pixels1, bool[,] pixels2)
    {
        // Compare each pixel of the two textures
        for (int x = 0; x < pixels1.GetLength(0); x++)
        {
            for (int y = 0; y < pixels1.GetLength(1); y++)
            {
                if (pixels1[x, y] != pixels2[x, y])
                {
                    return false; // If any pixel doesn't match, return false
                }
            }
        }
        return true; // If all pixels match, return true
    }
}
