using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionPromptUI : MonoBehaviour
{
    private Camera mainCam;
    [SerializeField] private TMP_Text prompText;

    public bool IsDisplayed;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        mainCam = Camera.main;
    }

    void LateUpdate()
    {
        var rotation = mainCam.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }

    public void Setup(string _promptText)
    {
        prompText.text = _promptText;
        gameObject.SetActive(true);
        IsDisplayed = true;
    }

    public void Close()
    {
        gameObject.SetActive(false);
        IsDisplayed = false;
    }
}
