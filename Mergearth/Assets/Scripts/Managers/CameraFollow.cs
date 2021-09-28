using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region Variables
    //Variables for following player movement
    private GameObject player;
    [SerializeField] private float timeOffset;
    [SerializeField] private Vector3 posOffset;
    private Vector3 velocity;
    #endregion

    #region UnityMethods
    // Start is called before the first frame update
    void Start()
    {
        SmoothCamera();
    }

    // Update is called once per frame
    void Update()
    {
        SmoothCamera();
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    #endregion

    #region Methods
    private void SmoothCamera()
    {
        this.transform.position = Vector3.SmoothDamp(this.transform.position, player.transform.position + posOffset, ref velocity, timeOffset);
    }
    #endregion
}
