using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultModalController : Singleton<ResultModalController>
{
    [SerializeField]
    private TMP_Text _resultText;

    [SerializeField]
    private ResultScriptableObject _resultScriptableObject;

    [SerializeField]
    private Button _closeButton;

    private void Start()
    {
        _closeButton.onClick.AddListener(() => BootstrapModalController.Instance.CloseNearestModal());
        _resultText.text = _resultScriptableObject.Result;
    }
}
