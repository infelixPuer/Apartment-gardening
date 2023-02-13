using UnityEngine;
using UnityEngine.InputSystem;

public class MouseControls : MonoBehaviour
{
    [SerializeField] private LayerMask plantpotLayer;
    [SerializeField] private LayerMask watercanLayer;

    private Controls _controls;
    private Camera _mainCamera;
    
    public bool watering { get; set; }

    public static MouseControls instance { get; private set; }
    
    private void Awake()
    {
        if (instance != null & instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        
        _controls = new Controls();
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _controls.Enable();
        
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    private void PerformClick()
    {
        DetectPlantpot();
        DetectWatercan();
    }

    private void DetectPlantpot()
    {
        var ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        var hit2D = Physics2D.GetRayIntersection(ray, 25, plantpotLayer);

        if (hit2D.collider == null)
            return;

        var plantpot = hit2D.collider.gameObject.GetComponent<Plantpot>();

        if (watering)
        {
            if (!plantpot.isPlanted)
            {
                watering = false;
                return;
            }
            
            plantpot.Water();
            watering = false;
            return;
        }

        if (plantpot.isGrown)
        {
            plantpot.Harvest();
            return;
        }
        
        if (!plantpot.isPlanted)
            plantpot.Grow();
    }

    private void DetectWatercan()
    {
        var ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        var hit2D = Physics2D.GetRayIntersection(ray, 25, watercanLayer);

        if (hit2D.collider == null)
            return;

        watering = true;
    }

    private void Start()
    {
        _controls.Mouse.Click.performed += _ => PerformClick();
    }
}
