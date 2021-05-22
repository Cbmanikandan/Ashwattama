using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager instance;
    private bool isReady;


    public Dictionary<string, string> localizedText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
       // PlayerPrefs.SetString("languageResx","localizedText_en");

        //LoadLocalizedData(PlayerPrefs.GetString("languageResx"));
    }

    public void LoadLocalizedData(string fileName)
    {
        localizedText = new Dictionary<string, string>();
        string filepath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(filepath))
        {
            string dataAsJson = File.ReadAllText(filepath);
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
            for (int i = 0; i < loadedData.items.Length; i++)
            {
                localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
            }
            Debug.Log("Data loaded, Dictionary contains " + localizedText.Count);
        }
        else
        {
            Debug.Log("Cannot find file");
        }
        isReady = true;
    }

    public bool GetIsReady()
    {
        return isReady;
    }
}
