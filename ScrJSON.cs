using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScrJSON : MonoBehaviour
{
    public Text nome;
    public Text senha;

    public void gravar()
    {
        ClassUserSave user = new ClassUserSave();

        user.login = nome.text;
        user.senha = senha.text;

        print("Login: " + user.login);
        print("Senha: " + user.senha);

        string json = "";

        string filePath = Path.Combine(Application.streamingAssetsPath, user.login + ".json");

        print(filePath);

        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }
        if (!File.Exists(filePath))
        {
            json = JsonUtility.ToJson(user);

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
            {
                file.WriteLine(json);
            }
        }
        else
        {
            print("Usuário já existe");
        }
    }

    public void ler()
    {
        try
        {
            string json = System.IO.File.ReadAllText(Application.streamingAssetsPath + "/" + nome.text + ".json");

            ClassUserSave user = JsonUtility.FromJson<ClassUserSave>(json);

            if (user.senha == senha.text)
            {
                print("Bem vindo");
            }
            else
            {
                print("Senha não confere!");
            }
        }
        catch (Exception ex)
        {
            print("Não encontrado");
        }
    }


    public void lerDiretorio()
    {
        DirectoryInfo di = new DirectoryInfo(Application.streamingAssetsPath);

        foreach(FileInfo file in di.GetFiles())
        {
            print(file.Name);
        }
    }

    public void apagar()
    {
        File.Delete(Application.streamingAssetsPath + "/" + nome.text + ".json");
    }
}
