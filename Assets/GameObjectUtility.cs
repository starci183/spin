
using UnityEngine;

public static class GameObjectUtility
{
    public static void SetInteractability(Transform transform, bool isEnabled = true)
    {
        var canvasGroup = transform.GetComponent<CanvasGroup>();

        canvasGroup.interactable = isEnabled;
        canvasGroup.blocksRaycasts = isEnabled;
    }

    public static Transform GetClosestSiblingGameObject(Transform transform, bool isOlder = true)
    {
        var parent = transform.transform.parent;
        var index = transform.transform.GetSiblingIndex();
        var siblingIndex = isOlder ? index - 1 : index + 1;

        if (siblingIndex < 0 || siblingIndex >= parent.childCount) return null;

        return parent.GetChild(siblingIndex);
    }

    public static Vector2 GetMousePos()
    {
        var mousePos = Input.mousePosition;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    public static Vector2 GetMousePositionRelativeToRectTransform(RectTransform rect)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, GetMousePos(), null, out Vector2 localPoint);
        return localPoint * rect.localScale;
    }

    public static bool IsPositionInsideRectTransformArea(Vector2 position, RectTransform area)
    {
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(area, position, null, out Vector2 localPosition))
        {
            return false;
        }

        var rect = area.rect;
        return rect.Contains(localPosition);
    }

    public static bool IsMousePositionInsideRectTransformArea(RectTransform area)
    {
        return IsPositionInsideRectTransformArea(GetMousePos(), area);
    }

    public static void RemoveAllChildGameObjects(Transform transform)
    {
        foreach (Transform child in transform)
        {
            Object.Destroy(child.gameObject);
        }
    }


}
