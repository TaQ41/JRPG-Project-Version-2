using UnityEngine;

namespace UIUpdateSystem
{

public class UIDisplayer : MonoBehaviour
{
    [SerializeField]
    private Canvas m_playerInfoCanvas;
    public Canvas PlayerInfoCanvas { get { return m_playerInfoCanvas;} }

    public void Show(Canvas canvas)
    {
        if (canvas == null)
        {
            return;
        }

        canvas.enabled = true;
    }

    public void Hide(Canvas canvas)
    {
        if (canvas == null)
        {
            return;
        }

        canvas.enabled = false;
    }
}
}