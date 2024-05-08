using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordFormation : MonoBehaviour
{
    public TMP_Text selectedWordText; // TMP_Text component to display the selected word
    public LayerMask letterLayer; // Layer mask for the letters

    private string selectedWord = ""; // Currently selected word
    private List<GameObject> selectedLetters = new List<GameObject>(); // List to store selected letters

    void Update()
    {
        // Check for touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Check if touch began
            if (touch.phase == TouchPhase.Began)
            {
                // Raycast to detect letters
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero, Mathf.Infinity, letterLayer);
                if (hit.collider != null)
                {
                    GameObject letterTile = hit.collider.gameObject;
                    // Check if the letter has already been selected
                    if (!selectedLetters.Contains(letterTile))
                    {
                        // Add the letter to the selected word
                        selectedLetters.Add(letterTile);
                        selectedWord += letterTile.GetComponent<TMP_Text>().text;
                        UpdateSelectedWordUI();
                    }
                }
            }
        }
    }

    // Update the UI to display the selected word
    void UpdateSelectedWordUI()
    {
        selectedWordText.text = selectedWord;
    }

    // Function to clear the selected word
    public void ClearSelectedWord()
    {
        selectedWord = "";
        selectedLetters.Clear();
        UpdateSelectedWordUI();
    }

    // Function to submit the selected word
    public void SubmitWord()
    {
        // Here you can implement logic to check if the submitted word is valid
        // For example, you could compare it against a list of valid words
        Debug.Log("Submitted word: " + selectedWord);
        ClearSelectedWord();
    }
}
