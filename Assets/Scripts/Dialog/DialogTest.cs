using System.Collections;
using UnityEngine;
using TMPro;

public class DialogTest : MonoBehaviour
{
	[SerializeField]
	private	DialogSystem dialogSystem01;
	[SerializeField]
	private	DialogSystem dialogSystem02;
	[SerializeField]
	private	DialogSystem dialogSystem03;
	[SerializeField]
	private	DialogSystem dialogSystem04;
	[SerializeField]
	private	DialogSystem dialogSystem05;
	public int dialogIndex = 0;
	public GymEquip[] gymEquips;

	private IEnumerator Start()
	{
		dialogIndex = PlayerPrefs.GetInt("DialogIndex");
		switch(dialogIndex)
		{
			case 0:
				yield return new WaitUntil(()=>dialogSystem01.UpdateDialog());
				break;
			case 1:
				gymEquips[0].CanStart = true;
				yield return new WaitUntil(()=>dialogSystem02.UpdateDialog());
				break;
			case 2:
				gymEquips[0].CanStart = true;
				gymEquips[1].CanStart = true;
				yield return new WaitUntil(()=>dialogSystem03.UpdateDialog());
				break;
			case 3:
				gymEquips[0].CanStart = true;
				gymEquips[1].CanStart = true;
				gymEquips[2].CanStart = true;
				yield return new WaitUntil(()=>dialogSystem04.UpdateDialog());
				break;
			case 4:
				gymEquips[0].CanStart = true;
				gymEquips[1].CanStart = true;
				gymEquips[2].CanStart = true;
				yield return new WaitUntil(()=>dialogSystem05.UpdateDialog());
				break;
			default:
				yield return null;
				break;
		}
	}
}

