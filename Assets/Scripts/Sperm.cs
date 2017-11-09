using UnityEngine;
using UnityEngine.UI;

public class Sperm : Photon.PunBehaviour
{
    [SerializeField]
    private Text EndGameText;
    private GameObject Egg;
    private GameManager Manager;

    private Animator TailAnimator { get; set; }
    public Rigidbody2D rb;
    private bool Impregnating;

    private float initialOrientationX;
    private float initialOrientationY;
    private float initialOrientationZ;

    private PhotonView pView;

    public static Sperm main;
    private Transform _cameraTransform;
    private Vector3 CameraPosition //TODO: Every object must spawn their own cameras. You have to work on that
    {
        get
        {
            return _cameraTransform.position;
        }
        set
        {
            _cameraTransform.position = value;
        }
    }

    private enum WiggleStates
    {
        Idle = 0,
        Left = 1,
        Right = 2
    }

    private void Awake()
    {
        if (photonView.isMine)
            Sperm.main = this;
    }

    // Use this for initialization
    void Start()
    {
        if (_cameraTransform == null) _cameraTransform = Camera.main.transform;
        Input.gyro.enabled = true;
        Egg = GameObject.FindGameObjectWithTag("Food");
        Manager = GameManager.GetManager().GetComponent<GameManager>();
        TailAnimator = transform.GetChild(0).GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        pView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pView.isMine)
        {
            TailAnimator.speed = rb.velocity.magnitude;
            CameraPosition = Vector3.Lerp(CameraPosition, new Vector3(0, transform.position.y, -10), Time.deltaTime * 8);
            float pos_y = Mathf.Clamp(CameraPosition.y, 9, Egg.transform.position.y - 10);
            if (Manager.GameStarted)
            {
                if (Input.gyro.rotationRateUnbiased.z < -0.05f || Input.gyro.rotationRateUnbiased.z > 0.05f)
                {
                    transform.Rotate(0, 0, Input.gyro.rotationRateUnbiased.z);
                }
            }
            CameraPosition = new Vector3(CameraPosition.x, pos_y, -10);
        }
    }
    
    public void Wiggle()
    {
        if (pView.isMine)
        {
            rb.velocity += new Vector2(Sperm.main.transform.up.x, Sperm.main.transform.up.y);
        }
    }

    public void ThrowSperm(int shakeCount)
    {
        Input.gyro.enabled = true;
        rb.AddForce(transform.up * shakeCount * 5);
        rb.angularVelocity = 0;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        rb.drag = 100;
        Impregnating = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.angularVelocity = 0;
        if (Impregnating) //WIN THE GAME
        {
            EndGameText.gameObject.SetActive(true);
            Destroy(GetComponent<Sperm>());
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}
