using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    private const int maxStats = 50;
    [SerializeField] CharacterStats stats; 
    [SerializeField] private Transform StatsContent; 
    TMPro.TextMeshProUGUI[] StatTexts;
    [SerializeField] private GameObject StatTextPrefab;
    // Start is called before the first frame update
    void Start()
    {
        StatTexts = new TMPro.TextMeshProUGUI[maxStats];
        for (int i = 0; i < StatTexts.Length; i++)
        {
            GameObject tmpItem = Instantiate(StatTextPrefab, StatsContent);
            StatTexts[i] = tmpItem.GetComponent<TMPro.TextMeshProUGUI>();
            tmpItem.SetActive(false);
        }
    }

    public void DisplayStast()
    {
        if (stats == null || StatTexts == null) return;
        Dictionary<StatType, Stat> statDictionary = stats.GetStats();

        ClearStats();

        int statTextIndex = 0;
        foreach (var item in statDictionary)
        {
            StatTexts[statTextIndex].gameObject.SetActive(true);
            StatTexts[statTextIndex].text = StatTypeExtensions.GetStatName(item.Key) + ": " + item.Value.GetValue();
            statTextIndex++;
        }
 
    }

    private void ClearStats()
    {
        if (StatTexts == null) return;
        foreach (var text in StatTexts)
        {
            text.text = "";
            text.gameObject.SetActive(false);
        }
    }
}
