using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [Header("Displays")]
    public GameObject startingDisplay;
    public GameObject gameplayDisplay;
    public GameObject endDisplay;

    [Header("Starting display")]
    public TextMeshProUGUI startingNameText;
    public TextMeshProUGUI startingGoalText;
    public Image startingGoalIcon;

    [Header("Gameplay Display")]
    public Image gameplayGoalIcon;
    public TextMeshProUGUI gameplayGoalText;

    [Header("")]
    public RectTransform addingObjectText;

    void Awake()
    {
        Instance = this;
    }

    public void UpdateGoalIcon(Sprite icon) 
    {
        gameplayGoalIcon.sprite = icon;
    }

    public void UpdateGoalText(int current, int goal) 
    {
        gameplayGoalText.text = $"{current}/{goal}";
    }

    public void Show(GameObject value) 
    {
        value.SetActive(true);
    }

    public void Hide(GameObject value) 
    {
        value.SetActive(false);
    }

    public void SetStartingDisplay(string itemName, Sprite icon, int count) 
    {
        startingNameText.text = $"Collect {itemName}";
        startingGoalText.text = $"{count}";
        startingGoalIcon.sprite = icon;
        UpdateGoalIcon(icon);
        UpdateGoalText(0, count);
        startingDisplay.SetActive(true);
    }

    public void AddingObject(Vector3 value) 
    {
        RectTransform rt = Instantiate(addingObjectText, gameplayDisplay.transform);
        rt.localPosition = Camera.main.transform.InverseTransformPoint(value);
        rt.gameObject.SetActive(true);
        StartCoroutine(AddingObjectChange(rt, rt.GetComponent<TextMeshProUGUI>()));
    }

    IEnumerator AddingObjectChange(RectTransform rtValue, TextMeshProUGUI value) 
    {
        yield return new WaitForSeconds(Time.deltaTime);
        rtValue.localPosition = rtValue.localPosition + Vector3.up * 2;
        value.color = new Color(value.color.r, value.color.g, value.color.b, value.color.a - Time.deltaTime);
        if (value.color.a > 0)
            StartCoroutine(AddingObjectChange(rtValue, value));
        else
            Destroy(value.gameObject);
    }
}