using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class CustomHierarchy : MonoBehaviour
{
    static CustomHierarchy()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
    }
    private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        var obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        if (obj != null)
        {
            var position = new Vector2(selectionRect.position.x + selectionRect.width - 20, selectionRect.position.y);
            Rect offsetRect = new Rect(position, selectionRect.size);
            obj.SetActive(EditorGUI.Toggle(offsetRect, obj.activeSelf));
        }
    }
}
