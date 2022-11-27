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
    //DataPersistenceManager Allready inheriting singleton interface so no need to make one in awake
    //public static DataPersistenceManager instance { get; private set; }

    //private void Awake()
    //{
    //    if (instance != null)
    //    {
    //        Debug.LogError("more than one data persistance manager");
    //    }
    //    instance = this;
    //}
    private void Start()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        datapersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }
    public void NewGame()
    {
       gameData = new GameData();
    }
    public void LoadGame()
    {
        //load our savec files data to the gameData object
        gameData = dataHandler.Load();
        if (gameData == null)
        {
            Debug.LogError("No Data Found. Initializiing data to defaults");
            NewGame();
        }
        //set the data in each class that has saved data and implements IDataPersistence
        foreach (IDataPersistence dataPersistenceObj in datapersistenceObjects)
        {
            dataPersistenceObj.LoadData(ref gameData);
        }
   
    }
    public void SaveGame()
    {

        foreach (IDataPersistence dataPersistenceObj in datapersistenceObjects)
        {
            dataPersistenceObj.SaveData( gameData);
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

