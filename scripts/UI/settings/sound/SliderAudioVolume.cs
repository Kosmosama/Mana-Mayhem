using Godot;
using System;

public partial class SliderAudioVolume : Control
{

	[Export(PropertyHint.Enum, "Master,Music,SFX")]
	public string BusName { get; set; } = "Master";
	int busIndex = 0;

	Label labelAudioName;
	HSlider sliderAudio;

	public override void _Ready()
	{
		labelAudioName = GetNode<Label>("%LabelAudioTrackName");
		sliderAudio = GetNode<HSlider>("%HSliderAudio");
		
		SetNameLabelText();
	}

	private void SetNameLabelText()
	{
		labelAudioName.Text = $"{BusName} Volume";
	}

	private void GetBusNameByIndex()
	{
		busIndex = AudioServer.GetBusIndex(BusName);
	}

	private void SetSliderValue()
	{
		sliderAudio.Value = Mathf.DbToLinear(AudioServer.GetBusVolumeDb(busIndex));
	}

	private void OnSliderValueChanged(double value)
	{
		AudioServer.SetBusVolumeDb(busIndex, Mathf.DbToLinear((float) value));
	}
}
