using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class SwipeLetterSelection : MonoBehaviour
{
    public TextMeshPro[] letterObjects; // Array of TextMeshPro objects representing letters
    public string selectedLetters = ""; // String to store selected letters
    public List<string> validWords = new List<string>();
    public TextMeshPro wordPrefab;
    public Transform spawnPosition;

    private bool isSwiping = false;
    private Vector2 swipeStartPosition;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isSwiping = true;
            swipeStartPosition = mousePosition;

            // Clear the selected letters set at the beginning of each swipe
            selectedLettersSet.Clear();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isSwiping = false;
            swipeStartPosition = Vector2.zero;
            CheckValidWord();
        }

        // Handle swiping
        if (isSwiping)
        {
            Vector2 currentSwipePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 swipeDirection = currentSwipePosition - swipeStartPosition;

            // Check if the swipe distance is significant
            float swipeDistance = swipeDirection.magnitude;
            if (swipeDistance > 0.1f)
            {
                // Normalize the swipe direction
                swipeDirection.Normalize();

                // Raycast to detect the letter the player selects
                RaycastHit2D[] hits = Physics2D.RaycastAll(swipeStartPosition, swipeDirection);
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.collider != null && hit.collider.CompareTag("Letter"))
                    {
                        string selectedLetter = hit.collider.gameObject.GetComponent<TextMeshPro>().text;
                        SelectLetter(selectedLetter);
                    }
                }

                // Update the start position for the next swipe
                swipeStartPosition = currentSwipePosition;
            }
        }
    }


    private HashSet<string> selectedLettersSet = new HashSet<string>(); // Keep track of selected letters

    private void SelectLetter(string letter)
    {
        // Check if the letter has already been selected in this swipe
        if (!selectedLettersSet.Contains(letter))
        {
            // Add the selected letter to the string of selected letters
            selectedLetters += letter;
            // Add the selected letter to the set of selected letters
            selectedLettersSet.Add(letter);
            Debug.Log("Selected Letter: " + letter);
        }
    }


    private void CheckValidWord()
    {
        // Check if the selected word is valid
        if (validWords.Contains(selectedLetters))
        {
            Debug.Log("Valid Word: " + selectedLetters);
            SpawnTextMeshPro(selectedLetters);
        }
        else
        {
            Debug.Log("Invalid Word: " + selectedLetters);
        }

        // Clear selected letters after checking if a valid word is found or not
        selectedLetters = "";
    }


    private void SpawnTextMeshPro(string word)
    {
        // Instantiate TextMeshPro object
        TextMeshPro newWordText = Instantiate(wordPrefab, spawnPosition.position, Quaternion.identity);
        // Set the text of the TextMeshPro object
        newWordText.text = word;
    }
}
