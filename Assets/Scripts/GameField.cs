using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameField : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI Balance;

	int moneyMem;

	int stavka;

	[SerializeField] Colomn[] Colomns;

	[SerializeField] TMP_InputField InStavka;

	[SerializeField] TextMeshProUGUI WinStavka;

	[SerializeField] GameObject SpinBtn;

	bool CheckWin;

	int slot1;
	int slot2;
	int slot3;


    void Start()
    {
		Colomn.CheckResult += CheckResult;

		moneyMem = PlayerPrefs.GetInt("moneyMem", 100);

		Balance.text = moneyMem.ToString();

		if(moneyMem>5)
			UpdateStavka("5");
		else
			UpdateStavka(moneyMem.ToString());

		if(moneyMem == 0)
			SpinBtn.SetActive(false);
    }

	void OnDisable()
	{
		Colomn.CheckResult -= CheckResult;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit ();
	}

    public void UpdateStavka(string Stavka)
    {
		if(Stavka != "")
			stavka = int.Parse(Stavka);
		else
			stavka = 1;

		if(stavka > moneyMem)
			stavka = moneyMem;

		if(stavka <= 0 )
				stavka = 1;

		InStavka.text = stavka.ToString();

    }

	void CheckResult()
	{
		CheckWin = false;

		for(slot1 = 1; slot1 < Colomns[0].GeneratedObject.Count; slot1 ++)
		{
			for(slot2 = 1; slot2 < Colomns[1].GeneratedObject.Count; slot2 ++)
			{
				if(Colomns[0].GeneratedObject[slot1].name == Colomns[1].GeneratedObject[slot2].name)
				{
					for(slot3 = 1; slot3 < Colomns[2].GeneratedObject.Count; slot3 ++)
					{
						if(Colomns[0].GeneratedObject[slot1].name == Colomns[2].GeneratedObject[slot3].name && !CheckWin)
						{
							Result("win");

							Debug.Log("Result Win");

							CheckWin = true;

							Colomns[0].GeneratedObject[slot1].GetComponent<Outline>().enabled = true;
							Colomns[1].GeneratedObject[slot2].GetComponent<Outline>().enabled = true;
							Colomns[2].GeneratedObject[slot3].GetComponent<Outline>().enabled = true;
						}
					}
				}
			}
		}

		if(!CheckWin)
		{
			Result("fail");
		}
	}

	void Result(string res)
	{
		if(res == "win")
		{
			gameObject.GetComponent<Animator>().Play("Win");

			moneyMem += stavka;

			WinStavka.text = stavka.ToString();
		}
		else
		{
			gameObject.GetComponent<Animator>().Play("Fail");
			
			moneyMem -= stavka;

			if(moneyMem == 0)
				SpinBtn.SetActive(false);

			if(stavka > moneyMem && moneyMem >0)
				UpdateStavka(moneyMem.ToString());
		}

		PlayerPrefs.SetInt("moneyMem", moneyMem);

		Balance.text = moneyMem.ToString();
	}
}
