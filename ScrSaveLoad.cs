using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrSaveLoad : MonoBehaviour
{
    public GameObject Player;
    public GameObject PlayerCamera;
    private ScrGameMaster GameMaster;
    public void Start()
    {
        GameMaster = GameObject.FindWithTag("GameMaster").GetComponent<ScrGameMaster>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SaveGameJSON();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            LoadGameJSON();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            DeleteSaveJSON();
        }
    }

    private void SaveGameJSON()
    {

        ClassUserSave SaveState = new ClassUserSave();

        // player rotation and position
        SaveState.xPos = Player.transform.position.x;
        SaveState.yPos = Player.transform.position.y;
        SaveState.zPos = Player.transform.position.z;

        SaveState.xRot = PlayerCamera.transform.rotation.eulerAngles.x;
        SaveState.yRot = Player.transform.rotation.eulerAngles.y;
        SaveState.zRot = PlayerCamera.transform.rotation.eulerAngles.z;

        // game flags
        SaveState.greenAcquired = GameMaster.greenAcquired;
        SaveState.redAcquired = GameMaster.redAcquired;
        SaveState.blueAcquired = GameMaster.blueAcquired;
        SaveState.yellowAcquired = GameMaster.yellowAcquired;


        string json = "";

        string filePath = Path.Combine(Application.streamingAssetsPath, "SaveState.json");

        print(filePath);

        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }
        if (!File.Exists(filePath))
        {
            json = JsonUtility.ToJson(SaveState);

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
            {
                file.WriteLine(json);
            }
        }
        else
        {
            print("SaveState já existe. Sobrescrevendo save antigo.");

            File.Delete(filePath);

            json = JsonUtility.ToJson(SaveState);

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
            {
                file.WriteLine(json);
            }
        }
    }


    private void LoadGameJSON()
    {

        string filePath = Path.Combine(Application.streamingAssetsPath, "SaveState.json");

        if (!File.Exists(filePath))
        {
            print("savefile does not exist");
        }
        else
        {
            string json = System.IO.File.ReadAllText(Application.streamingAssetsPath + "/SaveState.json");

            ClassUserSave SaveState = JsonUtility.FromJson<ClassUserSave>(json);

            // player rotation and position
            // Setting the position
            Player.transform.position = new Vector3(SaveState.xPos, SaveState.yPos, SaveState.zPos);

            // Setting the rotation
            Player.transform.rotation = Quaternion.Euler(0, SaveState.yRot, 0);
            PlayerCamera.transform.rotation = Quaternion.Euler(SaveState.xRot, 0, SaveState.zRot);

            // game flags
            GameMaster.greenAcquired = SaveState.greenAcquired;
            GameMaster.redAcquired = SaveState.redAcquired;
            GameMaster.blueAcquired = SaveState.blueAcquired;
            GameMaster.yellowAcquired = SaveState.yellowAcquired;
        }
    }

    private void DeleteSaveJSON()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "SaveState.json");

        if (Directory.Exists(Application.streamingAssetsPath))
        {
            if (!File.Exists(filePath))
            {
                print("File already does not exist");
            }
            else
            {
                File.Delete(filePath);
                print("File deleted succesfully");
            }
        }
    }
}