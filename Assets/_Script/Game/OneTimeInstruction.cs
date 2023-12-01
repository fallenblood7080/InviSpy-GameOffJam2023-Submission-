using UnityEngine;

public class OneTimeInstruction : MonoBehaviour
{
    [SerializeField] private int isInsturcted;
    [SerializeField] private AudioSource gameInstruction;
    void Start()
    {
        isInsturcted = PlayerPrefs.GetInt("isInstructed", 0);
        if (isInsturcted != 0)
        {
            return;
        }
        
        if (isInsturcted == 0)
        {
            Instruct();
        }
    }

    void Instruct()
    {
        gameInstruction.Play();
        PlayerPrefs.SetInt("isInstructed", 1);
    }
}
