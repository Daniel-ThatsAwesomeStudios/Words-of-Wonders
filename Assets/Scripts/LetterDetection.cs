using UnityEngine;

public class LetterDetection : MonoBehaviour
{
    // Public variables
    public TextMesh textMesh; // Reference to the TextMesh component

    void OnMouseDown()
    {
        // Check if the object has a TextMesh component
        if (textMesh != null)
        {
            // Get the text content from the TextMesh object
            string textContent = textMesh.text;

            // Get the position of the mouse click in world space
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Raycast to detect if the mouse click hits the collider of this object
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // Calculate the local position of the click on the TextMesh
                Vector2 localClickPosition = transform.InverseTransformPoint(clickPosition);

                // Get the character index at the clicked position
                int index = GetCharacterIndexAtPosition(localClickPosition, textMesh);

                if (index != -1)
                {
                    char clickedChar = textContent[index];
                    Debug.Log("Detected letter: " + clickedChar);
                    // Do whatever you need with the detected letter here
                }
            }
        }
    }

    int GetCharacterIndexAtPosition(Vector2 localPosition, TextMesh textMesh)
    {
        // Calculate the approximate character size based on the font size
        float charWidth = textMesh.characterSize * textMesh.fontSize / 2f;
        float charHeight = textMesh.characterSize * textMesh.fontSize;

        // Calculate the index of the character at the clicked position
        int column = Mathf.FloorToInt(localPosition.x / charWidth);
        int row = Mathf.FloorToInt(-localPosition.y / charHeight);
/*
        // Check if the row and column are within the text bounds
        if (row >= 0 && row < textMesh.textInfo.lineCount)
        {
            int lineFirstCharIndex = textMesh.textInfo.lineInfo[row].firstCharacterIndex;
            int lineLastCharIndex = textMesh.textInfo.lineInfo[row].lastCharacterIndex;

            int characterIndex = lineFirstCharIndex + column;

            // Check if the character index is valid
            if (characterIndex >= lineFirstCharIndex && characterIndex <= lineLastCharIndex)
            {
                return characterIndex;
            }
        }
*/
        return -1; // Return -1 if no character is found at the clicked position
    }
}
