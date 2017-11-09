using UnityEngine;
using UnityEngine.UI;

public class ShakeHandler : Photon.PunBehaviour
{
    public static ShakeHandler handler;
    [SerializeField]
    private GameObject StartText, Exit, ShakeIt;
    private GameObject StartObj, ExitObj, ShakeItObj;
    [SerializeField]
    private GameObject[] PushButtons;
    private GameObject Left; private GameObject Right;
    [SerializeField]
    private int ShakeCount;
    private int NextVibCount;
    private float ShakeTime;
    private GameManager Manager;


    private void Awake()
    {
        ShakeHandler.handler = this;
    }

    private void Start()
    {
        Left = Instantiate(PushButtons[0], GameObject.Find("Canvas").transform);
        Left.SetActive(false);
        Right = Instantiate(PushButtons[1], GameObject.Find("Canvas").transform);
        Right.SetActive(false);
        Manager = GameManager.MyObject;
        NextVibCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (ShakeTime < 5)
        {
            if (ShakeTime > NextVibCount)
            {
                Handheld.Vibrate();
                NextVibCount++;
            }
            ShakeTime += Time.deltaTime;
            if (Input.acceleration.y > 0.2f)
            {
                ShakeCount++;
            }
        }
        else if (ShakeTime < 6)
        {
            ShakeTime += Time.deltaTime;
            Manager.InitGame();
        }
        else if (!Manager.GameStarted)
        {
            Handheld.Vibrate();
            Manager.GameStarted = true;
            if(photonView.isMine)
            {
                Left.SetActive(true);
                Left.GetComponent<ButtonHandler>().Init(GetComponent<Sperm>());
                Right.SetActive(true);
                Right.GetComponent<ButtonHandler>().Init(GetComponent<Sperm>());
                GetComponent<Sperm>().ThrowSperm(ShakeCount);
            }
        }
    }    
}
