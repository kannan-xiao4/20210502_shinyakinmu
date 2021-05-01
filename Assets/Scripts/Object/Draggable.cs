using UnityEngine;

public abstract class Draggable : MonoBehaviour
{
    [SerializeField] private GameObject self;

    private Camera cam;
    private Vector3 initialPosition;

    private Vector3 screenPoint;
    private Vector3 offset;

    private Acceptable dropped;
    
    protected virtual void Awake()
    {
        cam = Camera.main;
        initialPosition = transform.position;
    }

    private void OnMouseDown()
    {
        screenPoint = cam.WorldToScreenPoint(transform.position);
        offset = transform.position - cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    private void OnMouseDrag()
    {
        var currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        var currentPosition = cam.ScreenToWorldPoint(currentScreenPoint) + this.offset;
        transform.position = currentPosition;
    }

    private void OnMouseUp()
    {
        if (dropped != null)
        {
            dropped.OnDropped(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        dropped = other.gameObject.GetComponent<Acceptable>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        dropped = null;
    }

    protected virtual void OnDestroy()
    {
        var newOne = Instantiate(self, initialPosition, Quaternion.identity);
        newOne.SetActive(true);
        cam = null;
        dropped = null;
    }
}