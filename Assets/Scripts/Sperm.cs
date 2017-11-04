using UnityEngine;

public class Sperm : MonoBehaviour
{
    [SerializeField]
    private GameObject Egg;
    private GameManager Manager;

    private Animator TailAnimator { get; set; }
    private Rigidbody2D rb;
    private bool Impregnating;

    private float initialOrientationX;
    private float initialOrientationY;
    private float initialOrientationZ;

    private Vector3 CameraPosition
    {
        get
        {
            return Camera.main.transform.position;
        }
        set
        {
            Camera.main.transform.position = value;
        }
    }

    private enum WiggleStates
    {
        Idle = 0,
        Left = 1,
        Right = 2
    }

    // Use this for initialization
    void Start()
    {
        Input.gyro.enabled = true;
        Manager = GameManager.GetManager().GetComponent<GameManager>();
        TailAnimator = transform.GetChild(0).GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        TailAnimator.speed = rb.velocity.magnitude;
        CameraPosition = Vector3.Lerp(CameraPosition, new Vector3(0, transform.position.y, -10), Time.deltaTime * 8);
        float pos_y = Mathf.Clamp(CameraPosition.y, 9, Egg.transform.position.y - 10);
        if(Manager.GameStarted)
        {
            if(Input.gyro.rotationRateUnbiased.z < -0.05f || Input.gyro.rotationRateUnbiased.z > 0.05f)
            {
                transform.Rotate(0, 0, Input.gyro.rotationRateUnbiased.z);
                Debug.Log(Input.gyro.rotationRateUnbiased.z);                
            }
        }
        CameraPosition = new Vector3(CameraPosition.x, pos_y, -10);
    }

    public void Wiggle()
    {        
        rb.velocity += new Vector2(transform.up.x, transform.up.y);
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
        if (Impregnating) //WIN THE GAME
        {
            Destroy(GetComponent<Sperm>());
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}
