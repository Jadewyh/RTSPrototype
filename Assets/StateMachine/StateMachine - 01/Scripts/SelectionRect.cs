using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SelectionRect : MonoBehaviour
{


  private Rect rect;
  [SerializeField]
  private RectTransform rectTransform;

  private Vector3 rectStartPos;
  private float clickTime = 0f;

  private void Awake()
  {
    rectTransform.gameObject.SetActive(false);

    rect = new Rect();
  }

  private void Update()
  {
    //mouse button clicked
    if (Input.GetMouseButtonDown(0))
    {
      clickTime = Time.time;
      rectStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    //mouse button held down
    if (Input.GetMouseButton(0) /*&& (Time.time - clickTime >= delay)*/)
    {
      Display(rectStartPos, Input.mousePosition);
    }

    //release mouse button
    if (Input.GetMouseButtonUp(0))
    {
      rectTransform.gameObject.SetActive(false);
    }

  }

  /// <summary>
  /// Shows the selection square on the screen.
  /// </summary>
  /// <param name="startPosition">The position of the startClick in WORLD SPACE</param>
  /// <param name="endPosition">the mouse's current position</param>
  public void Display(Vector3 startPosition, Vector3 endPosition)
  {
    rectTransform.gameObject.SetActive(true);

    Vector3 startPos = Camera.main.WorldToScreenPoint(startPosition);

    //Get the middle position of the square
    Vector3 middle = (startPos + endPosition) / 2f;

    //Set the middle position of the GUI square
    rectTransform.position = middle;

    //Change the size of the square
    float sizeX = Mathf.Abs(startPos.x - endPosition.x);
    float sizeY = Mathf.Abs(startPos.y - endPosition.y);

    //Set the size of the square
    rectTransform.sizeDelta = new Vector2(sizeX, sizeY);

    //TL
    rect.min = new Vector2(middle.x - sizeX / 2f, middle.y + sizeY / 2f);
    rect.max = new Vector3(middle.x + sizeX / 2f, middle.y - sizeY / 2f);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="unit">The unit to check</param>
  /// <returns></returns>
  public bool Contains(Unit unit)
  {
    Vector3 point = unit.transform.position;
    point = Camera.main.WorldToScreenPoint(point);
   
    if (rect.Contains(point, true) || RaycastCorners(unit))
    {
      return true;
    }

    return false;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="unit"></param>
  /// <returns></returns>
  private bool RaycastCorners(Unit unit)
  {
    RaycastHit hit;
    Ray[] rays = {
       Camera.main.ScreenPointToRay(rect.min),
       Camera.main.ScreenPointToRay(new Vector3(rect.min.x, rect.max.y, 0f)),
       Camera.main.ScreenPointToRay(rect.max),
       Camera.main.ScreenPointToRay(new Vector3(rect.max.x, rect.min.y, 0f)),
    };
    for (int i = 0; i < rays.Length; ++i)
    {
      if (Physics.Raycast(rays[i].origin, rays[i].direction, out hit, 100f))
      {
        if (hit.collider.GetComponent<Unit>() == unit)
        {
          return true;
        }
      }
    }
    return false;
    
  }

}