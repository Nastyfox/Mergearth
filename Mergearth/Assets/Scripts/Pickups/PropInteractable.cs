using UnityEngine;
using TMPro;

public class PropInteractable : MonoBehaviour
{
    #region Variables
    //Variables for interactable objects
    private TMP_Text interactionText;

    //Variables for chest opening
    private bool chestInRange;
    [SerializeField] private Animator chestAnimator;
    private int itemsNumber;
    private int pickupsNumber;
    [SerializeField] InteractableSO interactableSO;
    [SerializeField] GameObject[] itemsGO;
    [SerializeField] GameObject[] pickupsGO;

    //Variables for NPC talking
    private bool NPCInRange;
    private DialogSO dialogSO;
    #endregion

    #region UnityMethods
    // Update is called once per frame
    void Update()
    {
        //If player press E when he is close to an object he can interact with
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Check if it is a chest and open it
            if (chestInRange)
            {
                //Play animation
                chestAnimator.SetTrigger("OpenChest");
                //Play sound effect
                AudioManager.SharedInstance.PlaySoundEffect(interactableSO.soundEffect);

                //Spawn items from chest
                SpawnChestContent();


                //Deactivate box collider after opening it to avoid multiple openings
                this.GetComponent<BoxCollider2D>().enabled = false;
            }
            //If player is in range with NPC and dialog is not started yet
            else if (NPCInRange && !DialogManager.SharedInstance.GetStartedDialog())
            {
                //Launch dialog and deactivate interaction text
                DialogManager.SharedInstance.StartDialog(dialogSO);
            }
        }
    }

    private void Awake()
    {
        //Get the interaction text and deactivate it
        interactionText = GameObject.FindGameObjectWithTag("InteractionText").GetComponent<TMP_Text>();
        interactionText.enabled = false;
    }
    #endregion

    #region Methods
    private void SpawnChestContent()
    {
        //Set a random number of item
        itemsNumber = Random.Range(interactableSO.minItemsNumber, interactableSO.maxItemsNumber);

        for (int i = 0; i < itemsNumber; i++)
        {
            //Add a random item based on possible ones
            int itemType = Random.Range(0, interactableSO.possibleItems.Length);
            //Instantiate new item
            GameObject item = Instantiate(itemsGO[itemType]);
        }

        //Set a random number of pickups
        pickupsNumber = Random.Range(interactableSO.minItemsNumber, interactableSO.maxItemsNumber);

        for (int i = 0; i < pickupsNumber; i++)
        {
            //Add a random pickup based on possible ones
            int pickupType = Random.Range(0, interactableSO.possiblePickups.Length);
            //Instantiate new item
            GameObject pickup = Instantiate(pickupsGO[pickupType]);
        }
    }
    #endregion

    #region Collisions
    private void OnTriggerStay2D(Collider2D collision)
    {
        //If the player is in contact with interactable prop and if he's not climbing
        if (collision.gameObject.CompareTag("Player") && !PlayerMovement.SharedInstance.GetIsClimbing())
        {
            if (this.gameObject.CompareTag("Chest"))
            {
                //Chest is in range
                chestInRange = true;


                //Object is interactable so display interaction text
                interactionText.text = Constants.CHESTINTERACTION;
                interactionText.enabled = true;
            }
            else if (this.gameObject.CompareTag("NPC"))
            {
                //NPC is in range
                NPCInRange = true;

                //Object is interactable so display interaction text
                interactionText.text = Constants.NPCINTERACTION;
                interactionText.enabled = true;

                dialogSO = NPCDialog.SharedInstance.GetDialog();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //If the player gets out with pickup that he can interact with
        if (collision.gameObject.CompareTag("Player"))
        {
            if (this.gameObject.CompareTag("Chest"))
            {
                //Chest is not in range anymore so disable interaction text
                chestInRange = false;
                interactionText.enabled = false;
            }
            else if (this.gameObject.CompareTag("NPC"))
            {
                //Chest is not in range anymore so disable interaction text
                NPCInRange = false;
                interactionText.enabled = false;
            }
        }
    }
    #endregion
}
