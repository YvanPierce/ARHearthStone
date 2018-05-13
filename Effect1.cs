using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 增加己方英雄的生命值2，能量2,护盾2
/// </summary>
public class Effect1:Effect {

	private int changeOfHP;
	private int changeOfMP;
	private int ShieldOnPlayer;

	public Effect1() {
		setType (true);
		setName ("AddHPandAddMP");
		changeOfHP = 2;
		changeOfMP = 2;
	}

	public void CastEffectOnPlayer(Player target) {
		target.AlterHP (changeOfHP);
		target.AlterMP (changeOfMP);
	}


}
