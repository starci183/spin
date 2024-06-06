
using System.Collections;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class AnimationUtility
{
	public static IEnumerator WaitForAnimationCompletion(Transform transform)
	{
		GameObjectUtility.SetInteractability(transform, false);

		var animator = transform.GetComponent<Animator>();

		yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1);

		GameObjectUtility.SetInteractability(transform, true);
	}

	public static IEnumerator ExecuteTriggerThenWait(Transform transform, TriggerName triggerName)
	{
		var animator = transform.GetComponent<Animator>();

		GameObjectUtility.SetInteractability(transform, false);

		animator.SetTrigger(EnumUtility.GetDescription(triggerName));

		yield return new WaitForSeconds(0.1f);

		yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1);

		GameObjectUtility.SetInteractability(transform, true);
	}
}

public enum TriggerName
{
	[Description("Fade In")]
	FadeIn,

	[Description("Fade Out")]
	FadeOut,

	[Description("Flip Self")]
	FlipSelf,

	[Description("End")]
	End,

	[Description("Flip In")]
	FlipIn,

	[Description("Flip Out")]
	FlipOut,

}
