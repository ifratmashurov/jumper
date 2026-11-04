using UnityEngine;
using TMPro;

public class SurvivalTimer : MonoBehaviour
{
    public float goal = 60f;
    public Animator gateAnimator;
    public TextMeshProUGUI timerText;
    
    private float t;
    private bool won;
    
    // БАСҚА СКРИПТТЕР КӨРЕ АЛАТЫН ЖАЛАУША
    public static bool isGateOpen = false; 

    void Start()
    {
        isGateOpen = false; // Ойын басталғанда қақпа әрқашан жабық
    }

    void Update()
    {
        if (won) return;

        t += Time.deltaTime;

        if (timerText != null)
        {
            timerText.text = $"УАҚЫТ: {Mathf.FloorToInt(t)} / {goal}";
        }

        if (t >= goal)
        {
            won = true;
            isGateOpen = true; // Уақыт бітті, ҚАҚПА АШЫЛДЫ деп белгілейміз
            
            if (timerText != null)
            {
                timerText.text = "ЖЕҢІС! ҚАҚПАҒА КІР!";
                timerText.color = Color.green;
            }
            
            if (gateAnimator != null)
            {
                gateAnimator.SetBool("Open", true); 
            }
        }
    }
}