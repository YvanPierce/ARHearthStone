using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect {
	Player targetOfPlayer;
	Hero targetOfHero;

	private string name;



	/// <summary>
	/// 1作用在玩家上,0作用在英雄上
	/// </summary>
	private bool typeOfEffect;

	public void CastEffectOnPlayer(Player target) {}

	public void CastEffectOnHero(Hero target) {}

	/// <summary>
	/// 根据不同效果牌类型确定实施的对象，暂定为对英雄和对玩家两类，之后可考虑合并
	/// </summary>
	public void CastEffect(Player target) {
		CastEffectOnPlayer (target);
	}

	public void CastEffect(Hero target) {
		CastEffectOnHero (target);
	}


	public void setType(bool s) {
		typeOfEffect = s;
	}

	public void setName(string s) {
		name = s;
	}

}
