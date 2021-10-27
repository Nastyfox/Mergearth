using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "InputReaderSO", menuName = "ScriptableObjects/InputReaderSO")]
public class InputReaderSO : ScriptableObject, PlayerActions.IPlayerControlActions, PlayerActions.IUIControlActions
{

    #region Variables
    //Player Control
    public event UnityAction jumpEvent;
    public event UnityAction jumpCanceledEvent;
    public event UnityAction interactEvent;
    public event UnityAction pauseEvent;
    public event UnityAction inventoryEvent;
    public event UnityAction<Vector2> moveEvent;

    //UI Control
    public event UnityAction dialogEvent;
    public event UnityAction creditsEvent;
    public event UnityAction unpauseEvent;
    public event UnityAction closeInventoryEvent;

    private PlayerActions playerActions;
    private InputActionMap previousActionMap;
    #endregion

    #region UnityMethods
    private void OnEnable()
    {
        if (playerActions == null)
        {
            playerActions = new PlayerActions();
            playerActions.PlayerControl.SetCallbacks(this);
            playerActions.UIControl.SetCallbacks(this);
        }

        EnablePlayerControlInput();
    }

    private void OnDisable()
    {
        DisableAllInput();
    }
    #endregion

    #region Getters & Setters
    public InputActionMap GetPreviousActionMap()
    {
        return previousActionMap;
    }
    #endregion

    #region Methods
    public void SaveActionMap()
    {
        previousActionMap = playerActions.GetActiveMap();
    }

    public void SwitchActionMap(InputActionMap map)
    {
        DisableAllInput();
        map.Enable();
    }
    public void EnablePlayerControlInput()
    {
        playerActions.PlayerControl.Enable();
        playerActions.UIControl.Disable();
    }

    public void EnableUIControlInput()
    {
        playerActions.UIControl.Enable();
        playerActions.PlayerControl.Disable();
    }

    public void DisableAllInput()
    {
        playerActions.PlayerControl.Disable();
        playerActions.UIControl.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (moveEvent != null)
            moveEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (jumpEvent != null && context.phase == InputActionPhase.Performed)
            jumpEvent.Invoke();

        if (jumpCanceledEvent != null && context.phase == InputActionPhase.Canceled)
            jumpCanceledEvent.Invoke();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (interactEvent != null && context.phase == InputActionPhase.Performed)
            interactEvent.Invoke();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (pauseEvent != null && context.phase == InputActionPhase.Performed)
            pauseEvent.Invoke();
    }

    public void OnUnpause(InputAction.CallbackContext context)
    {
        if (unpauseEvent != null && context.phase == InputActionPhase.Performed)
            unpauseEvent.Invoke();
    }

    public void OnCredits(InputAction.CallbackContext context)
    {
        if (creditsEvent != null && context.phase == InputActionPhase.Performed)
            creditsEvent.Invoke();
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (inventoryEvent != null && context.phase == InputActionPhase.Performed)
            inventoryEvent.Invoke();
    }

    public void OnDialog(InputAction.CallbackContext context)
    {
        if (dialogEvent != null && context.phase == InputActionPhase.Performed)
            dialogEvent.Invoke();
    }

    public void OnCloseInventory(InputAction.CallbackContext context)
    {
        if (closeInventoryEvent != null && context.phase == InputActionPhase.Performed)
        {
            closeInventoryEvent.Invoke();
        }
    }
    #endregion
}
