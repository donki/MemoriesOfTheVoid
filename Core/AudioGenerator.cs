using System;
using System.IO;

namespace MemoriesOfTheVoid.Tools
{
    public static class AudioGenerator
    {
        private const int SampleRate = 44100;
        private const short BitsPerSample = 16;
        private const short Channels = 1;

        public static void GenerateAllAudio()
        {
            GenerateDoorOpenSound();
            GeneratePanelBeepSound();
            GenerateStepSound();
            GenerateWhisperSound();
        }

        private static void GenerateDoorOpenSound()
        {
            float duration = 2.0f; // 2 seconds
            int samples = (int)(SampleRate * duration);
            short[] audioData = new short[samples];

            for (int i = 0; i < samples; i++)
            {
                float t = (float)i / SampleRate;
                
                // Mechanical door sound: low frequency sweep with noise
                float frequency = 80 + (t * 40); // 80Hz to 120Hz sweep
                float amplitude = 0.3f * (1.0f - t / duration); // Fade out
                
                // Add some mechanical noise
                float noise = (float)(new Random(i).NextDouble() - 0.5) * 0.1f;
                
                float sample = amplitude * (float)Math.Sin(2 * Math.PI * frequency * t) + noise;
                audioData[i] = (short)(sample * short.MaxValue);
            }

            WriteWavFile("Content/door_open.wav", audioData);
        }

        private static void GeneratePanelBeepSound()
        {
            float duration = 0.5f; // 0.5 seconds
            int samples = (int)(SampleRate * duration);
            short[] audioData = new short[samples];

            for (int i = 0; i < samples; i++)
            {
                float t = (float)i / SampleRate;
                
                // Electronic beep: 800Hz tone with quick fade
                float frequency = 800f;
                float amplitude = 0.4f * (float)Math.Exp(-t * 8); // Quick exponential decay
                
                float sample = amplitude * (float)Math.Sin(2 * Math.PI * frequency * t);
                audioData[i] = (short)(sample * short.MaxValue);
            }

            WriteWavFile("Content/panel_beep.wav", audioData);
        }

        private static void GenerateStepSound()
        {
            float duration = 0.8f; // 0.8 seconds
            int samples = (int)(SampleRate * duration);
            short[] audioData = new short[samples];

            Random random = new Random(42);

            for (int i = 0; i < samples; i++)
            {
                float t = (float)i / SampleRate;
                
                // Footstep: filtered noise with envelope
                float noise = (float)(random.NextDouble() - 0.5);
                float envelope = (float)Math.Exp(-t * 5) * (1.0f - t / duration);
                
                // Low-pass filter effect (simple)
                float filteredNoise = noise * 0.3f;
                
                float sample = envelope * filteredNoise;
                audioData[i] = (short)(sample * short.MaxValue * 0.6f);
            }

            WriteWavFile("Content/step_hallway.wav", audioData);
        }

        private static void GenerateWhisperSound()
        {
            float duration = 3.0f; // 3 seconds
            int samples = (int)(SampleRate * duration);
            short[] audioData = new short[samples];

            Random random = new Random(123);

            for (int i = 0; i < samples; i++)
            {
                float t = (float)i / SampleRate;
                
                // Whisper: low-frequency filtered noise with modulation
                float noise = (float)(random.NextDouble() - 0.5);
                float modulation = (float)Math.Sin(2 * Math.PI * 0.5f * t); // 0.5Hz modulation
                float envelope = 0.2f * (1.0f + 0.3f * modulation);
                
                // Very low frequency content
                float lowFreq = (float)Math.Sin(2 * Math.PI * 60 * t) * 0.1f;
                
                float sample = envelope * (noise * 0.1f + lowFreq);
                audioData[i] = (short)(sample * short.MaxValue);
            }

            WriteWavFile("Content/whisper_loop.wav", audioData);
        }

        private static void WriteWavFile(string filename, short[] audioData)
        {
            using (var fileStream = new FileStream(filename, FileMode.Create))
            using (var writer = new BinaryWriter(fileStream))
            {
                // WAV Header
                writer.Write("RIFF".ToCharArray());
                writer.Write(36 + audioData.Length * 2); // File size - 8
                writer.Write("WAVE".ToCharArray());
                
                // Format chunk
                writer.Write("fmt ".ToCharArray());
                writer.Write(16); // Chunk size
                writer.Write((short)1); // Audio format (PCM)
                writer.Write(Channels);
                writer.Write(SampleRate);
                writer.Write(SampleRate * Channels * BitsPerSample / 8); // Byte rate
                writer.Write((short)(Channels * BitsPerSample / 8)); // Block align
                writer.Write(BitsPerSample);
                
                // Data chunk
                writer.Write("data".ToCharArray());
                writer.Write(audioData.Length * 2); // Data size
                
                // Audio data
                foreach (short sample in audioData)
                {
                    writer.Write(sample);
                }
            }
        }
    }
}