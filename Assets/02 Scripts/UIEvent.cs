using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivalShooter.Event
{
    public static class UIEvent
    {
        // Sound Delegates
        public delegate void MasterSoundDelegate(float volume);
        public delegate void MusicSoundDelegate(float volume);
        public delegate void EffectsSoundDelegate(float volume);

        // ScoreBoard Delegates
        public delegate void ScoreDelegate(int score);
        public delegate void RoundDelegate(int round);

        // Sound Setting Events
        public static MasterSoundDelegate MasterSoundEvent;
        public static MusicSoundDelegate MusicSoundEvent;
        public static EffectsSoundDelegate EffectsSoundEvent;

        // ScoreBoard Events
        public static ScoreDelegate ScoreEvent;
        public static RoundDelegate RoundEvent;
    }
}
