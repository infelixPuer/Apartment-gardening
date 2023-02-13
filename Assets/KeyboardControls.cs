using TMPro;
using UnityEngine;

public class KeyboardControls : MonoBehaviour
{
    // Bind key pressing to skipping week
    // Also plant should grow after every week
    // And you can't grow plant in one week
    // If plant isn't watered, then it should not grow

    [SerializeField] private TextMeshProUGUI weekText;
    [SerializeField] private Plantpot plantpot;

    private Controls _controls;

    private const string WeekStr = "Week: ";

    private int _weekCounter = 0;

    private void Awake()
    {
        _controls = new Controls();
        weekText.text = WeekStr + _weekCounter;
    }

    private void Start()
    {
        _controls.Keybord.Skipweek.performed += _ => PerformPress();
    }

    private void PerformPress()
    {
        _weekCounter++;
        weekText.text = WeekStr + _weekCounter;
        if (!plantpot.isPlanted)
            return;
        
        plantpot.Grow();
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }
}
