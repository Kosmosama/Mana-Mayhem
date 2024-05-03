using Godot;
using System;

public partial class MainMenu : Control
{
	PackedScene mainScene;
	SettingsMenu settingsMenu;
	MarginContainer marginContainer;

	public override void _Ready()
	{   
		settingsMenu = GetNode<SettingsMenu>("SettingsMenu");
		marginContainer = GetNode<MarginContainer>("MarginContainer");

		mainScene = ResourceLoader.Load("res://scenes/world/World.tscn") as PackedScene;
	}

	private void OnButtonPlayPressed()
	{
		GetTree().ChangeSceneToPacked(mainScene);
	}

	private void OnButtonSettingsPressed()
	{
		settingsMenu.Visible = true;
		marginContainer.Visible = false;
	}

	private void OnButtonExitPressed()
	{
		GetTree().Quit();
	}

	public void OnSettingsMenuBack()
	{
		settingsMenu.Visible = false;
		marginContainer.Visible = true;
	}
}
