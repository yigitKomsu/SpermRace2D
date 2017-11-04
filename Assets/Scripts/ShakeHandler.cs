using UnityEngine;

public class ShakeHandler : MonoBehaviour {
    public int ShakeCount;
    private int NextVibCount;
    private float ShakeTime;
    private GameManager Manager;

    private void Start()
    {
        Manager = GameManager.GetManager().GetComponent<GameManager>();
        NextVibCount = 0;
    }

    // Update is called once per frame
    void Update () {
        if (ShakeTime < 5)
        {
            if(ShakeTime > NextVibCount)
            {
                Handheld.Vibrate();
                NextVibCount++;
            }
            ShakeTime += Time.deltaTime;
            if (Input.acceleration.y > 0.8f)
            {
                ShakeCount++;
            }
        }
        else if(ShakeTime < 6)
        {
            ShakeTime += Time.deltaTime;
            Manager.InitGame();
        }
        else
        {
            Handheld.Vibrate();
            Manager.GameStarted = true;
            Manager.StartGame();
            GetComponent<Sperm>().ThrowSperm(ShakeCount);
            Destroy(GetComponent<ShakeHandler>());
        }

    }
}
