using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class CompleteTheGame : MonoBehaviour
{
    private string targetTag = "Player"; // Tag of the GameObjects to search for
    [SerializeField] private float range = 1.5f; // Radius of the sphere
    [SerializeField] private GameObject intractText; 
    [SerializeField] private GameObject successMenu; 
    private bool isSuccess = true;


    private void Update()
    {
        // Get the position of the GameObject
        Vector3 centerPosition = transform.position;

        // Find all colliders within the specified range
        Collider[] colliders = Physics.OverlapSphere(centerPosition, range);

        // Check if any colliders were found
        if (colliders.Length > 0)
        {
            // Iterate through the found colliders
            foreach (Collider collider in colliders)
            {
                // Check if the collider belongs to a GameObject with the target tag
                GameObject gameObject = collider.gameObject;
                if (gameObject.tag == targetTag)
                {
                    // Found a matching GameObject
                    // Debug.Log("Found GameObject: " + gameObject.name);

                    intractText.SetActive(true);
                    if (InputManager.GetInstance.IsInteractPressed)
                    {
                        GameOverManager.GetInstance.OnGameOver?.Invoke(isSuccess);
                        intractText.SetActive(false);
                        enabled = false;
                    }
                }

                else
                {
                    intractText.SetActive(false);
                }
            }
        }
    }
}
