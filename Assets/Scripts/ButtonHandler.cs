using UnityEngine;

public class ButtonHandler : MonoBehaviour
{

    public Sperm mySperm;

    public void Init(Sperm sperm)
    {
        mySperm = sperm;
    }

    private void Wiggle()
    {
        mySperm.Wiggle();
    }

    private void OnMouseDown()
    {
        mySperm.Wiggle();
    }

}
