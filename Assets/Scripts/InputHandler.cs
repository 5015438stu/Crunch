using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class InputHandler : MonoBehaviour
{
    float currenttime = 0f;
    public float startingtime = 999f;
    public CinemachineVirtualCamera vc;

    [SerializeField] TextMeshProUGUI Countdown;

    void Start()
    {
        currenttime = startingtime;
    }

    void Update()
    {
        currenttime -= 1 * Time.deltaTime;
        Countdown.text = currenttime.ToString ("0");

        if(currenttime <= 0)
        {
            currenttime = 0;
        }
    }
}
