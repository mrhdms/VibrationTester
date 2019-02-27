using NativeUtil;
using UnityEngine;
using UnityEngine.UI;

public class VibrationTester : MonoBehaviour
{
	[SerializeField] private Transform _buttonParent = null;
	[SerializeField] private GameObject _buttonPrefab = null;
	[SerializeField] private TextAsset _buttonSettings = null;

	[SerializeField] private Slider slider = null;
	[SerializeField] private Text sliderText = null;
	[SerializeField] private Button playButton = null;

	private void Start()
	{
#if UNITY_IOS
		InitializePlaySystemSoundButtons();
#else
		InitializeAndroidVibrationTest();
#endif
	}

	private void InitializeAndroidVibrationTest()
	{
		_buttonParent.gameObject.SetActive(false);
		_buttonPrefab.gameObject.SetActive(false);

		slider.onValueChanged.AddListener(OnValueChanged);
		playButton.onClick.AddListener(OnClickButton);
	}

	private void OnClickButton()
	{
		AndroidUtil.Vibrate((long)(slider.value * 1000f));
	}

	private void OnValueChanged(float value)
	{
		sliderText.text = (value * 1000).ToString() + "ms";
	}


	private void InitializePlaySystemSoundButtons()
	{
		slider.gameObject.SetActive(false);
		sliderText.gameObject.SetActive(false);
		playButton.gameObject.SetActive(false);


		var lines = _buttonSettings.text.Split('\n');
		foreach (var line in lines)
		{
			if (string.IsNullOrEmpty(line))
			{
				continue;
			}
			var buttonData = line.Split(' ');
			var text = buttonData[0];
			var id = int.Parse(buttonData[1]);
			CreateButton(text, id);
		}
		_buttonPrefab.SetActive(false);
	}

	private void CreateButton(string text, int index)
	{
		var button = Instantiate(_buttonPrefab);
		button.transform.SetParent(_buttonParent);
		button.GetComponent<Button>().onClick.AddListener(() => { IOSUtil.PlaySystemSound(index); });
		button.GetComponentInChildren<Text>().text = $"{text} : {index}";
	}
}
