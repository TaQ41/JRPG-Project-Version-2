using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MinisceneSystem.MainComponentBlocks
{

/// <summary>
/// Wait for a specified amount of seconds before triggering the end response.
/// </summary>
public class WaitForSecondsComponent : MonoBehaviour
{
    public MinisceneAutoProcess WrapperProcess;
    public TextMeshProUGUI Text;

    public float delay = 5.0f;
    public bool IsSkippable = false;

    public ProjectInputActionAsset controls;
    private InputAction m_skipAction;

    private void OnEnable()
    {
        controls = new();
        m_skipAction = controls.Player.SkipCutsceneRequest;
        m_skipAction.performed += Skip;
        m_skipAction.Enable();

        StartCoroutine(WaitForSeconds());
    }

    private void OnDisable()
    {
        m_skipAction.performed -= Skip;
        m_skipAction.Disable();
    }

    private IEnumerator WaitForSeconds()
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < delay)
        {
            elapsedTime += Time.deltaTime;
            Text.text = elapsedTime.ToString();

            yield return null;
        }

        WrapperProcess.TriggerEndAction();
    }

    private void Skip(InputAction.CallbackContext context)
    {
        if (IsSkippable == false)
        {
            return;
        }

        StopCoroutine(WaitForSeconds());
        WrapperProcess.TriggerEndAction();
    }
}
}