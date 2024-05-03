using Godot;
using System;
using System.Text.Json;

public partial class ButtonBindHotKey : Control
{
	[Export]
	string actionName = "";

	public string ActionName
	{
		get { return actionName; }
		set 
		{
			actionName = value;
			_Rename();
		}
	}

	Label label;
	Button button;

	public override void _Ready()
	{
		SetProcessUnhandledKeyInput(false);

		label = GetNode<Label>("%LabelHotKey");
		button = GetNode<Button>("%ButtonBindHotKey");

		_Rename();
	}

	private void _Rename()
	{
		SetActionName();
		SetBoundKeyName();
	}

	private void SetBoundKeyName()
	{	
		InputEvent actionEvent = InputMap.ActionGetEvents(actionName)[0];

		button.Text = GetInputEventPhysicalKeycode(actionEvent);
	}

	private string GetInputEventPhysicalKeycode(InputEvent @event)
	{
		// Returns None in case it does not find a PhysicalKeycode

		string x = "None";

		if (@event is InputEventKey keyEvent)
		{
			x = keyEvent.PhysicalKeycode.ToString();
		}

		return x;
	}

	private void SetActionName()
	{
		label.Text = actionName.Capitalize();
	}

	private void OnButtonBindHotKeyToggled(bool toggled_on)
	{
		if (toggled_on)
		{
			button.Text = "Press any key...";
			SetProcessUnhandledKeyInput(toggled_on);

			foreach (ButtonBindHotKey i in GetTree().GetNodesInGroup("hotkeyButton")) // close other buttons in case another one is pressed
			{
				if(i.ActionName != this.actionName)
				{
					i.button.ToggleMode = false;
					i.SetProcessUnhandledKeyInput(false);
				}
			}
		}
		else
		{
			foreach (ButtonBindHotKey i in GetTree().GetNodesInGroup("hotkeyButton"))
			{
				if(i.ActionName != actionName)
				{
					i.button.ToggleMode = true;
					i.SetProcessUnhandledKeyInput(false);
				}
			}

			SetBoundKeyName();
		}
	}

	public override void _UnhandledKeyInput(InputEvent @event)
	{
		RebindActionKey(@event);
		button.ButtonPressed = false;
	}

	public void RebindActionKey(InputEvent @event)
	{
		InputMap.ActionEraseEvent(actionName, InputMap.ActionGetEvents(actionName)[0]);
		InputMap.ActionAddEvent(actionName, @event);

		SetBoundKeyName();
	}
}
