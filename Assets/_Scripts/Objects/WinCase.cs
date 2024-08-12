using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnCollision : MonoBehaviour
{
    // The index of the scene to load
    public int sceneIndex = 2;

    // This method is called when another collider enters the trigger collider attached to this GameObject
    void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding is the player (assuming the player has the tag "Player")
        if (other.CompareTag("Player"))
        {
            // Unlock the cursor and make it visible
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Load the scene with the specified index
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
