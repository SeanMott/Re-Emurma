using System;
using System.Linq;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using NAudio.CoreAudioApi;
using NAudio.Extras;
using NAudio.Vorbis;

//namespace MinorShift._Library
//{
//	internal class Sound
//	{
//		private const int sampleRate = 44100;
//
//		private static WasapiOut output;
//		private static MixingSampleProvider mixer;
//
//		private string filename;
//		private float volume = 1.0f;
//
//		private WaveStream stream;
//		private VolumeSampleProvider volumeProvider;
//
//		static Sound()
//		{
//			output = new WasapiOut(AudioClientShareMode.Shared, 50);
//
//			mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, 2));
//			mixer.ReadFully = true;
//
//			output.Init(mixer);
//			output.Play();
//		}
//
//		public void play(string filename, bool loop = false)
//		{
//			if (stream != null && filename == this.filename)
//			{
//				stop();
//			}
//			else
//			{
//				this.filename = filename;
//				if (stream != null) stream.Dispose();
//				if (this.filename.EndsWith(".wav", StringComparison.OrdinalIgnoreCase))
//				{
//					stream = new WaveFileReader(this.filename);
//				}
//				else if (this.filename.EndsWith(".ogg", StringComparison.OrdinalIgnoreCase))
//				{
//					stream = new VorbisWaveReader(this.filename);
//				}
//				else
//				{
//					// NOTE: MediaFoundationReader currently seems to only support 16 bit audio files on wine
//					var settings = new MediaFoundationReader.MediaFoundationReaderSettings();
//					settings.RequestFloatOutput = true;
//					stream = new MediaFoundationReader(this.filename, settings);
//				}
//			}
//
//			// MediaFoundationResampler is faster and possibly higher quality than WdlResamplingSampleProvider but currently doesn't seem to work on wine
//			// var resampler = new MediaFoundationResampler(loop ? new LoopStream(stream) : stream, WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, 2));
//			// volumeProvider = new VolumeSampleProvider(resampler.ToSampleProvider());
//
//			ISampleProvider sampleProvider = (loop ? new LoopStream(stream) : stream).ToSampleProvider();
//			if (sampleProvider.WaveFormat.SampleRate != sampleRate) sampleProvider = new WdlResamplingSampleProvider(sampleProvider, sampleRate);
//			if (sampleProvider.WaveFormat.Channels == 1) sampleProvider = sampleProvider.ToStereo();
//			volumeProvider = new VolumeSampleProvider(sampleProvider);
//
//			volumeProvider.Volume = volume;
//			mixer.AddMixerInput(volumeProvider);
//		}
//
//		public void stop()
//		{
//			if (volumeProvider != null) mixer.RemoveMixerInput(volumeProvider);
//			if (stream != null) stream.Position = 0;
//		}
//
//		public void close()
//		{
//			stop();
//			filename = null;
//			volumeProvider = null;
//			if (stream != null)
//			{
//				stream.Dispose();
//				stream = null;
//			}
//		}
//
//		public bool isPlaying()
//		{
//			if (volumeProvider == null) return false;
//			lock (mixer.MixerInputs) { return mixer.MixerInputs.Contains(volumeProvider); }
//		}
//
//		public void setVolume(int volume)
//		{
//			this.volume = Math.Clamp(volume, 0, 100) / 100.0f;
//			if (volumeProvider != null) volumeProvider.Volume = this.volume;
//		}
//	}
//}
