﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForAnimation : CustomYieldInstruction
{
	private Animation animation;

	public WaitForAnimation(Animation animation)
	{
		this.animation = animation;
	}
		
	public override bool keepWaiting
	{
		get
		{
			return animation.isPlaying;
		}
	}
}
