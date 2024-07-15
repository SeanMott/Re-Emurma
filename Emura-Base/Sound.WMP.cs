/*
このファイルはSFMTアルゴリズムによって擬似乱数を作成するためのクラスライブラリです。
このファイルはRei HOBARAさんが
http://www.rei.to/random.html
において公開しているC#向けのSFMTライブラリを改変したものです。

さらに大元のSFMTアルゴリズムについては
http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/SFMT/
を参照してください。

2009/7/2 MinorShift
*/

using WMPLib;

namespace MinorShift._Library
{
	internal class Sound
	{
		private WindowsMediaPlayer player = new WindowsMediaPlayer();
		public void play(string filename, bool loop = false)
		{
			player.URL = filename;
			player.settings.setMode("loop", loop);
			player.controls.play();
		}

		public void stop()
		{
			player.controls.stop();
		}

		public void close()
		{
			player.close();
		}

		public bool isPlaying()
		{
			switch (player.playState)
			{
				case WMPPlayState.wmppsUndefined: return false;
				case WMPPlayState.wmppsStopped: return false;
				case WMPPlayState.wmppsPlaying: return true;
				case WMPPlayState.wmppsPaused: return false;
				case WMPPlayState.wmppsScanForward: return false;
				case WMPPlayState.wmppsScanReverse: return false;
				case WMPPlayState.wmppsBuffering: return true;
				case WMPPlayState.wmppsWaiting: return true;
				case WMPPlayState.wmppsMediaEnded: return false;
				case WMPPlayState.wmppsTransitioning: return true;
				case WMPPlayState.wmppsReady: return false;
				case WMPPlayState.wmppsReconnecting: return true;
				default: return false;
			}
		}

		public void setVolume(int volume)
		{
			player.settings.volume = volume;
		}
	}
}
