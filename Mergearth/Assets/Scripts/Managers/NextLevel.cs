using UnityEngine;

public class NextLevel : MonoBehaviour
{
    #region Variables
    //Variables for new scene loading
    [SerializeField] private string sceneName;
    [SerializeField] private int levelNumber;
    #endregion

    #region Collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the player hit the level's end, load new scene
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(LoadScene.SharedInstance.LoadSceneByName(sceneName, levelNumber, true, true));
        }
    }
    #endregion
}
