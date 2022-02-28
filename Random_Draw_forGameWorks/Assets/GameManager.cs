using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI FirstPrize_Text;
    [SerializeField] TextMeshProUGUI SecondPrize_Text;
    [SerializeField] TextMeshProUGUI ThirdPrize_Text;

    [SerializeField] TMP_InputField Participants;

    [SerializeField] TMP_InputField FirstPrize_Awards;
    [SerializeField] TMP_InputField SecondPrize_Awards;
    [SerializeField] TMP_InputField ThirdPrize_Awards;

    [SerializeField] TMP_InputField FirstPrize_Count_Input;
    [SerializeField] TMP_InputField SecondPrize_Count_Input;
    [SerializeField] TMP_InputField ThirdPrize_Count_Input;

    [SerializeField] GameObject SettingObjects;
    [SerializeField] GameObject DrawingObjects;
    [SerializeField] GameObject winningComfirmText;

    [SerializeField] TextMeshProUGUI this_Award_Count_Text;

    [SerializeField] List<Participant> All_Participants;

    [SerializeField] TextMeshProUGUI thisRotate_Name;
    [SerializeField] Image backgroundImage;

    public int firstPrize_Count;
    public int secondPrize_Count;
    public int thirdPrize_Count;

    bool isRotating;
    bool isSettingDone;

    System.Random random;

    public class Participant
    {
        public string name;
        public Color color;
        public Color nameColor;

        public Participant(string _name, Color _color)
        {
            name = _name;

            color = _color;
            nameColor = new Color(1, 1, 1);
        }
    }


    void Start()
    {
        All_Participants = new List<Participant>();
        random = new System.Random();
        isSettingDone = false;
        isRotating = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isSettingDone && !isRotating && All_Participants.Count > 0)
        {
            if(thirdPrize_Count > 0)
            {
                StartCoroutine(Draw_Name(3));
            }
            else if(secondPrize_Count > 0)
            {
                StartCoroutine(Draw_Name(2));
            }
            else if(firstPrize_Count > 0)
            {
                StartCoroutine(Draw_Name(1));
            }
        }
    }

    public void Shuffle()
    {
        int n = All_Participants.Count;

        while(n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            Participant value = All_Participants[k];
            All_Participants[k] = All_Participants[n];
            All_Participants[n] = value;
        }
    }

    IEnumerator Draw_Name(int count)
    {
        isRotating = true;
        winningComfirmText.SetActive(false);

        if (count == 1)
            this_Award_Count_Text.text = FirstPrize_Awards.text + "ÃßÃ· Áß..";
        else if (count == 2)
            this_Award_Count_Text.text = SecondPrize_Awards.text + "ÃßÃ· Áß..";
        else
            this_Award_Count_Text.text = ThirdPrize_Awards.text + "ÃßÃ· Áß..";

        for (int i = 0; i < 50; i++)
        {
            Shuffle();

            thisRotate_Name.text = All_Participants[0].name;
            backgroundImage.color = All_Participants[0].color;
            yield return new WaitForSeconds(0.1f);
        }

        Shuffle();
        thisRotate_Name.text = All_Participants[0].name;
        backgroundImage.color = All_Participants[0].color;
        yield return new WaitForSeconds(0.2f);

        Shuffle();
        thisRotate_Name.text = All_Participants[0].name;
        backgroundImage.color = All_Participants[0].color;
        yield return new WaitForSeconds(0.25f);

        Shuffle();
        thisRotate_Name.text = All_Participants[0].name;
        backgroundImage.color = All_Participants[0].color;
        yield return new WaitForSeconds(0.4f);

        switch (count)
        {
            case 1:
                FirstPrize_Text.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text += All_Participants[0].name;
                firstPrize_Count--;
                break;
            case 2:
                SecondPrize_Text.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text += All_Participants[0].name;
                secondPrize_Count--;
                break;
            case 3:
                ThirdPrize_Text.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text += All_Participants[0].name;
                thirdPrize_Count--;
                break;
        }

        All_Participants.RemoveAt(0);

        winningComfirmText.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        isRotating = false;
    }

    public void Start_Button()
    {
        string[] tmp = Participants.text.Split(' ');

        firstPrize_Count = Convert.ToInt32(FirstPrize_Count_Input.text);
        secondPrize_Count = Convert.ToInt32(SecondPrize_Count_Input.text);
        thirdPrize_Count = Convert.ToInt32(ThirdPrize_Count_Input.text);

        Debug.Log(firstPrize_Count);
        Debug.Log(secondPrize_Count);
        Debug.Log(thirdPrize_Count);

        foreach(string s in tmp)
        {
            Color color_tmp = new Color((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
            All_Participants.Add(new Participant(s, color_tmp));
        }

        FirstPrize_Text.text = FirstPrize_Awards.text + " ´çÃ·ÀÚ";
        SecondPrize_Text.text = SecondPrize_Awards.text + " ´çÃ·ÀÚ";
        ThirdPrize_Text.text = ThirdPrize_Awards.text + " ´çÃ·ÀÚ";

        SettingObjects.SetActive(false);
        DrawingObjects.SetActive(true);

        isSettingDone = true;
    }
}
