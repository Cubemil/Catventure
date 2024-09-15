using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public static class Helpers
{
    
    /*************** waiting for coroutines without filling garbage collector **************/
    
    private static readonly Dictionary<float, WaitForSeconds> WaitDictionary = new Dictionary<float, WaitForSeconds>();
    public static WaitForSeconds GetWait(float time)
    {
        if (WaitDictionary.TryGetValue(time, out var wait)) return wait;

        WaitDictionary[time] = new WaitForSeconds(time);
        return WaitDictionary[time];
    }

    /******************** checks if mouse/finger is over an ui element ****************/
    
    /*** Example implementation:
     *  public class Implementation : MonoBehaviour
     *  {
     *      [SerializeField] private TextMeshProUGUI _text;
     *      void Update()
     *      {
     *          _text.text = Helpers.IsOverUi() ? "Over UI" : "Not over UI";
     *      }
     *  }
     ***/
    
    private static PointerEventData _eventDataCurrentPosition; // takes mouse pos
    private static List<RaycastResult> _results;
    // if the ray cast returns anything like an ui element returns true
    public static bool IsOverUi()
    {
        _eventDataCurrentPosition = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
        _results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(_eventDataCurrentPosition, _results);
        return _results.Count > 0;
    }
    
    /*********************** find world point of canvas element *********************/
    
    /*** Example implementation:
     *  public class Cube : MonoBehaviour
     *  {
     *      [SerializeField] private RectTransform _followTarget;
     *      void Update()
     *      {
     *          transform.position = Helpers.GetWorldPositionOfCanvasElement(_followTarget);
     *      }
     *  }
     ***/
    
    public static Vector2 GetWorldPositionOfCanvasElement(RectTransform element)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(element, element.position, Camera.main, out var result);
        return result;
    }

    /**************************** delete all child objects *************************/

    public static void DeleteChildren(this Transform t)
    {
        foreach (Transform child in t) Object.Destroy(child.gameObject);
    }
    
    /************************* calculating isometric directions **********************/
    
    // calculated 4x4 matrix by rotation 45 on the y-axis => for our isometric viewpoint
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    // multiplies _isoMatrix with our input Vector3
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}
