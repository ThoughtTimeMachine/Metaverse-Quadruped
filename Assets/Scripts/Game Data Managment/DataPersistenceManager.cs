using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class DataPersistenceManager : Singleton<DataPersistenceManager>
{
    private GameData gameData;
    //name the file we want to save
   [SerializeField] private string fileName;
    private List<IDataPersistence> datapersistenceObjects;
    //class that handles the dataSaving process to a folder location
    private FileDataHandler dataHandler;
    public static DataPersistenceManager instance { get; private set; }
    
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("more than one data persistance manager");
        }
        instance = this;
    }
    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.datapersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }
    public void NewGame()
    {
        this.gameData = new GameData();
    }
    public void LoadGame()
    {
        //load our savec files data to the gameData object
        this.gameData = dataHandler.Load();
        if (this.gameData == null)
        {
            Debug.LogError("No Data Found. Initializiing data to defaults");
            NewGame();
        }
        //set the data in each class that has saved data and implements IDataPersistence
        foreach (IDataPersistence dataPersistenceObj in datapersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
        Debug.Log("flavor test: "+ gameData.flavor);
    }
    public void SaveGame()
    {

        foreach (IDataPersistence dataPersistenceObj in datapersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }
        dataHandler.Save(gameData);
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }

}

