using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score;
    public Text scoreText;

    void Start()
    { score = 0;
        scoreText.text = score.ToString();}
    public void updateScore()
    { score++;
     scoreText.text = score.ToString();}}