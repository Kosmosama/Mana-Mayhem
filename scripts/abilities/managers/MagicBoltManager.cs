using Godot;
using System;
using System.Security.Cryptography;

public partial class MagicBoltManager : Node2D
{
	// private void _on_ice_spear_timer_attack_timer_timeout()
	// {
	// 	if (iceSpearAmmo > 0)
	// 	{
	// 		ice_spear iceSpearAttack = (ice_spear)iceSpear.Instantiate();
	// 		iceSpearAttack.Position = Position;
	// 		iceSpearAttack.targetLocation = GetClosestTarget();
	// 		iceSpearAttack.level = iceSpearLevel;
	// 		AddChild(iceSpearAttack);
	// 		iceSpearAmmo --;
	// 		if (iceSpearAmmo > 0)
	// 		{
	// 			iceSpearTimerAttackTimer.Start();
	// 		}
	// 		else
	// 		{
	// 			iceSpearTimerAttackTimer.Stop();
	// 		}
	// 	}
	// }

	int penetration; // number of enemies that a magic bolt can hit, more with level | to::child
	float magicCooldown; // seconds to await for instancing another magic bolt batch
	float magicDuration; // seconds to await to de-instance a magic bolt if it hasnt hit anything | to::child
	float magicSize; // multiplier of how big the hitbox and sprite are in comparison to base size | to::child
	int magicNumber; // number of magic bolt instances per batch
	float damage; // damage each magic bolt does | to::child

	PackedScene magicBolt;
	Timer magicBoltCooldown;

	public override void _Ready()
	{
		magicBolt = (PackedScene)ResourceLoader.Load("res://scenes/abilities/instantiable/MagicBolt.tscn");
		magicBoltCooldown = GetNode<Timer>("MagicBoltCooldown");

		Initialize();
	}

	public override void _Process(double delta)
	{
	}

	private void Initialize()
	{
		penetration = 1;
		magicCooldown = 1f;
		magicDuration = 0.5f;
		magicSize = 1f;
		magicNumber = 1;
		damage = 5f;
	}

	public void UpdateCooldown(float waitTime)
	{
		magicBoltCooldown.WaitTime = waitTime;
	}

	// func: updateDuration
	// ...

	private void OnMagicBoltCooldownTimeout()
	{
		for (int p = 1; p <= penetration; p++)
		{
			MagicBolt magicBoltAttack = (MagicBolt) magicBolt.Instantiate();

	//		ice_spear iceSpearAttack = (ice_spear)iceSpear.Instantiate();
	// 		iceSpearAttack.Position = Position;
	// 		iceSpearAttack.targetLocation = GetClosestTarget();
	// 		iceSpearAttack.level = iceSpearLevel;
	// 		AddChild(iceSpearAttack);
	// 		iceSpearAmmo --;
	// 		if (iceSpearAmmo > 0)
	// 		{
	// 			iceSpearTimerAttackTimer.Start();
	// 		}
	// 		else
	// 		{
	// 			iceSpearTimerAttackTimer.Stop();
	// 		}
			AddChild(magicBoltAttack);
		}
	}
}
