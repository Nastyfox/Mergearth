using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuSelectionHandler : MonoBehaviour
{

    #region Variables
    [SerializeField] private InputReader _inputReader;
    [SerializeField] [ReadOnly] private GameObject _defaultSelection;
    [SerializeField] [ReadOnly] private GameObject _currentSelection;
    [SerializeField] [ReadOnly] private GameObject _mouseSelection;
    #endregion

    #region UnityMethods
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	#endregion

}
