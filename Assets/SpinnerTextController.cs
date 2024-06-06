
using TMPro;
using UnityEngine;
public class SpinnerTextController : Singleton<SpinnerTextController>
{
    [SerializeField]
    private Transform _facesContainerTransform;

    [SerializeField]
    private Transform _textCanvasPrefab;

    [SerializeField]
    private string[] _words = new string[12] 
    {
        "cuong1",
        "",
        "",
        "",
        "",
        "",
        "cuong7",
        "",
        "",
        "",
        "",
        ""
    };

    private void Start()
    {
        float angle = 0f;

        for (int i = 0; i < _words.Length; i++)
        {
            var childTransform = _facesContainerTransform.GetChild(i);
            var textCanvasObj = Instantiate(_textCanvasPrefab, childTransform);

            var canvas = textCanvasObj.GetComponent<Canvas>();

            var meshFilter = childTransform.GetComponent<MeshFilter>();
            var meshRenderer = childTransform.GetComponent<MeshRenderer>();

            canvas.worldCamera = Camera.main;

            var size = meshFilter.mesh.bounds.size;
            textCanvasObj.GetComponent<RectTransform>().sizeDelta = new Vector2(
                size.x,
                new Vector2(size.y, size.z).magnitude
                );

            textCanvasObj.gameObject.transform.position = meshRenderer.bounds.center;
            
            textCanvasObj.GetComponentInChildren<TMP_Text>().text = _words[i];

            

            if (i == 0)
            {
                angle = Mathf.Atan2(size.y, size.z) * Mathf.Rad2Deg + 90f;
            }

            float x = angle - i * 30f;

            var euler = new Vector3(x, 0f, 0f);

            textCanvasObj.gameObject.transform.Rotate(euler);
            textCanvasObj.gameObject.transform.transform.position -= textCanvasObj.gameObject.transform.forward * 0.01f;
        }

        Canvas.ForceUpdateCanvases();
    }
}