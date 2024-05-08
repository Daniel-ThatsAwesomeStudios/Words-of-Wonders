using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SwipeWordSelection : MonoBehaviour
{
    public TextMeshPro textMesh; // Reference to the TextMesh component
    public List<string> dictionary; // List of words to check against

    private List<char> selectedLetters = new List<char>(); // List to store selected letters
    private Vector2 touchStartPos; // Starting position of the swipe

    void Update()
    {
        // Detect swipe input
        if (Input.GetMouseButtonDown(0))
        {
            touchStartPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Vector2 touchEndPos = Input.mousePosition;
            Vector2 swipeDirection = touchEndPos - touchStartPos;

            if (swipeDirection.magnitude > 50f) // Minimum swipe distance
            {
                // Get the characters within the swipe range
                GetSelectedLetters(touchStartPos, touchEndPos);

                // Check if the selected letters form a word
                CheckWord();
            }
        }

    }
  
    void GetSelectedLetters(Vector2 startPos, Vector2 endPos)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(startPos), endPos - startPos);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                Vector2 localClickPosition = transform.InverseTransformPoint(hit.point);

                int index = GetCharacterIndexAtPosition(localClickPosition, textMesh);

                if (index != -1)
                {
                    char clickedChar = textMesh.text[index];
                    if (!selectedLetters.Contains(clickedChar))
                    {
                        selectedLetters.Add(clickedChar);
                        Debug.Log("Selected letter: " + clickedChar);
                    }
                }
            }
        }
    }

    private int GetCharacterIndexAtPosition(Vector2 localClickPosition, TextMeshPro textMesh)
    {
        throw new NotImplementedException();
    }

    int GetCharacterIndexAtPosition(Vector2 localPosition, TextMesh textMesh)
    {
        // Split the text into lines
        string[] lines = textMesh.text.Split('\n');

        // Find the row based on local position
        float lineHeight = textMesh.lineSpacing * textMesh.fontSize;
        int row = Mathf.FloorToInt(-localPosition.y / lineHeight);

        if (row >= 0 && row < lines.Length)
        {
            string line = lines[row];

            // Find the column based on local position
            float charWidth = textMesh.characterSize * textMesh.fontSize;
            int column = Mathf.FloorToInt(localPosition.x / charWidth);

            if (column >= 0 && column < line.Length)
            {
                int index = column + row * line.Length;
                return index;
            }
        }

        return -1;
    }

    void CheckWord()
    {
        string word = new string(selectedLetters.ToArray());
        if (dictionary.Contains(word))
        {
            Debug.Log("Word formed: " + word);
        }
        else
        {
            Debug.Log("Not a valid word: " + word);
        }

        selectedLetters.Clear(); // Clear selected letters for next swipe
    }
     void OnMouseDown()
    {
        Debug.Log("CLick");
    }
}
