using System.Collections;
using TMPro;
using UnityEngine;

namespace MinisceneSystem.MainComponentBlocks
{

/// <summary>
/// Wait for a specified amount of seconds before triggering the end response.
/// </summary>
public class WaitForSecondsComponent : MonoBehaviour
{
    public MinisceneAutoProcess WrapperProcess;
    public TextMeshProUGUI Text;

    public float delay = 0.0f;

    void Awake()
    {
        StartCoroutine(WaitForSeconds());
    }

    private IEnumerator WaitForSeconds()
    {
        float elapsedTime = 0f;
        while (elapsedTime < delay)
        {
            elapsedTime += Time.deltaTime;
            Text.text = elapsedTime.ToString();
            yield return null;
        }

        WrapperProcess.TriggerEndAction();
    }
}
}