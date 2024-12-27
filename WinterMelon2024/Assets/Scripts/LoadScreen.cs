using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadScreen : MonoBehaviour
{
    [SerializeField] private Image loadImage;
    [SerializeField] private TextMeshProUGUI levelNameText;
    [SerializeField] private Image loadBar;

    public void SetEssentials(LevelData _levelData)
    {
        loadImage.sprite = _levelData.levelArt;
        levelNameText.text = _levelData.levelName;
    }

    public void SetProgress(float _progress)
    {
        loadBar.fillAmount = _progress;
    }
}
