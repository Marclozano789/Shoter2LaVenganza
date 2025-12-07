using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    public TextMeshProUGUI healthText;
    //public Text healthText;

    public int health = 100; 


    private void Update()
    {
        healthText.text = health.ToString();



    }







}
