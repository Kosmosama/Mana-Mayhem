using Godot;
using Godot.Collections;
using System;
using System.IO;

public partial class SaveManager : Node
{
    public static SaveManager Instance { get; private set; }
    public override void _Ready() 
    { 
        Instance = this;
        SettingsSignalBus.Instance.SetSettingsDictionary += OnSettingsSave;
    }

    private const string SETTINGS_FILE_SAVE_PATH = "data/settings.json";

    public void OnSettingsSave(Dictionary data)
    {
        string json = Json.Stringify(data);

        try
        {
            File.WriteAllText(SETTINGS_FILE_SAVE_PATH, json);
        }
        catch (Exception e)
        {
            GD.Print(e.Message);
        }
    }

    public void LoadSettingsData()
    {
        if (!File.Exists(SETTINGS_FILE_SAVE_PATH))
            return;

        string data = null;

        try 
        {
            data = File.ReadAllText(SETTINGS_FILE_SAVE_PATH);
        }
        catch (Exception e)
        {
            GD.Print(e.Message);
        }

        Json jsonLoader = new Json();

        Error error = jsonLoader.Parse(data);

        if (error != Error.Ok)
        {
            GD.Print(error);
            return;
        }

        Dictionary settingsDictionary = (Dictionary) jsonLoader.Data;

        SettingsSignalBus.Instance.EmitOnLoadSettingsDictionary(settingsDictionary);
    }
}
