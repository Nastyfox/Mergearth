using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour
{
    #region Variables
    //Variables for respawning player
    private Animator fadeSystemAnimator;
    private float fadeDuration;
    #endregion

    #region UnityMethods
    // Start is called before the first frame update
    void Start()
    {
        //Get the animator for fadesystem
        fadeSystemAnimator = LoadScene.SharedInstance.GetFadeAnimator();

        //Get fadein duration
        foreach (AnimationClip clip in fadeSystemAnimator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == "FadeIn")
            {
                fadeDuration = clip.length;
            }
        }
    }
    #endregion

    #region Collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(RespawnDeathZone());
        }
    }
    #endregion

    #region IEnumerators
    private IEnumerator RespawnDeathZone()
    {
        //Launch fade animation and load new scene
        fadeSystemAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(fadeDuration);
        PlayerSpawn.SharedInstance.SpawnPlayer();
        yield return new WaitForSeconds(fadeDuration);
        PlayerHealth.SharedInstance.TakeDamage(Constants.DEATHZONEDAMAGE);
    }
    #endregion
}
