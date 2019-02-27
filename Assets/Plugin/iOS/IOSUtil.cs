using System.Runtime.InteropServices;

public static class IOSUtil
{
#if UNITY_IOS && !UNITY_EDITOR
        [DllImport ("__Internal")]
		static extern void _playSystemSound(int n);
#endif

	public static void PlaySystemSound(int n)
	{
#if UNITY_IOS && !UNITY_EDITOR
			_playSystemSound(n);
#endif
	}
}
