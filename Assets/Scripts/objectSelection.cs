using UnityEngine;

public class objectSelection : MonoBehaviour
{
    // Layer mask to filter selectable objects
    public LayerMask selectableLayer;

    // Selected object reference
    private GameObject selectedObject;

    // Update is called once per frame
    void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // Raycast to detect selectable objects
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, selectableLayer))
            {
                // Check if the hit object is selectable
                GameObject hitObject = hit.collider.gameObject;

                // Deselect previously selected object if any
                if (selectedObject != null)
                {
                    // Implement logic for deselection (e.g., change color, scale, etc.)
                    DeselectObject(selectedObject);
                }

                // Select the newly hit object
                selectedObject = hitObject;

                // Implement logic for selection (e.g., change color, scale, etc.)
                SelectObject(selectedObject);
            }
            else
            {
                // If clicked outside of selectable objects, deselect current selection
                if (selectedObject != null)
                {
                    // Implement logic for deselection (e.g., change color, scale, etc.)
                    DeselectObject(selectedObject);
                    selectedObject = null;
                }
            }
        }
    }

    // Custom method to handle object selection
    void SelectObject(GameObject obj)
    {
        // Implement logic to highlight or change appearance of the selected object
        // For example, change material color, scale up, etc.
        // obj.GetComponent<Renderer>().material.color = Color.red;
    }

    // Custom method to handle object deselection
    void DeselectObject(GameObject obj)
    {
        // Implement logic to reset appearance of the deselected object
        // For example, revert material color to original, scale down, etc.
        // obj.GetComponent<Renderer>().material.color = Color.white;
    }
}
