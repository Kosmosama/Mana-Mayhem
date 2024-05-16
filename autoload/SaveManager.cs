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

    Dictionary settingsDict = new Dictionary();

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
}
