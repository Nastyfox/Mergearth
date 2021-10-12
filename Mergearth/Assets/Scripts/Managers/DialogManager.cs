using System.Collections;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    #region Variables
    //Variable for instance of the dialog manager (singleton)
    public static DialogManager SharedInstance;

    //Variables for dialogSO
    private DialogSO dialog;

    //Variables for Dialog UI
    [SerializeField] private TMP_Text NPCName;
    [SerializeField] private TMP_Text NPCSentence;
    [SerializeField] private Animator dialogAnimator;

    //Variables to browse sentences
    private int sentenceNumber;
    private bool dialogStarted;

    //Variables for controls
    [SerializeField] private InputReaderSO inputReader = default;
    private bool launchNextSentence;
    #endregion

    #region UnityMethods
    private void Awake()
    {
        //Get the instance for dialog manager
        SharedInstance = this;

        //Set the first sentence and dialog not started
        sentenceNumber = 0;
        dialogStarted = false;
    }

    private void OnEnable()
    {
        //Add listeners for player controls events invoked by inputreader

        //Trigger the dialog
        inputReader.dialogEvent += TriggerDialog;
    }

    private void OnDisable()
    {
        //Remove listeners for player controls events invoked by inputreader

        //Trigger the dialog
        inputReader.dialogEvent -= TriggerDialog;
    }

    // Update is called once per frame
    private void Update()
    {
        //If player press input, go to the next sentence
        if (dialogStarted && launchNextSentence && !ShopManager.SharedInstance.GetIsShopOpen())
        {
            NextSentence();
            launchNextSentence = false;
        }
        //If player press input and shop is open, close it
        else if(launchNextSentence && ShopManager.SharedInstance.GetIsShopOpen())
        {
            ShopManager.SharedInstance.CloseShop();
        }
    }
    #endregion

    #region Getters & Setters
    public bool GetStartedDialog()
    {
        return dialogStarted;
    }
    #endregion

    #region Methods
    public void StartDialog(DialogSO dialogSO)
    {
        //Store dialogSO
        this.dialog = dialogSO;

        //Activate UI controls only
        inputReader.EnableUIControlInput();

        //Start animation for dialog UI
        dialogAnimator.SetBool("isOpen", true);

        //Say that dialog has started
        dialogStarted = true;

        //Set npc name 
        NPCName.text = dialog.NPCName;

        //Display first sentence and set the next one
        StopAllCoroutines();
        StartCoroutine(TypeSentence(dialog.NPCSentences[sentenceNumber]));
        sentenceNumber++;

        //Don't display the next sentence immediatly
        launchNextSentence = false;

        //Activate UI Controls only
        inputReader.EnableUIControlInput();
    }

    public void NextSentence()
    {
        //If it's not the last sentence, display the current one and go to the next one
        if (sentenceNumber <= (dialog.NPCSentences.Length - 1))
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence(dialog.NPCSentences[sentenceNumber]));
            sentenceNumber++;
        }
        //End dialog if it was the last sentence
        else
        {
            StartCoroutine(EndDialog());
        }
    }
    #endregion

    #region IEnumerators
    private IEnumerator EndDialog()
    {
        //Close dialog UI
        dialogAnimator.SetBool("isOpen", false);

        //If it's not a shop, end dialog and make the player play again
        if (!dialog.isShop)
        {
            //Activate player controls only
            inputReader.EnablePlayerControlInput();
        }
        //If it's a shop and it has not been opened yet, show shop panel and instantiate items inside
        else if(!ShopManager.SharedInstance.GetShopHasBeenOpenened() && dialog.isShop)
        {
            //Add items in the shop
            ShopManager.SharedInstance.AddItemsInShop();
        }
        //If it's a shop and it has been opened yet, show shop panel
        else if (ShopManager.SharedInstance.GetShopHasBeenOpenened() && dialog.isShop)
        {
            //Show shop panel
            ShopManager.SharedInstance.OpenShop();
        }

        //Wait 1 second before possibility to start another dialog
        yield return new WaitForSeconds(1);
        dialogStarted = false;
        sentenceNumber = 0;
    }

    private IEnumerator TypeSentence(string sentence)
    {
        //Display each letter every 0.05 sec
        NPCSentence.text = "";
        foreach (char letter in sentence)
        {
            NPCSentence.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }
    #endregion

    #region InputMethods
    private void TriggerDialog()
    {
        launchNextSentence = true;
    }
    #endregion
}
