using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObj
{
    [CreateAssetMenu(fileName = "New Clips Data", menuName = "Custom/AudioClipsData")]
    public class AudioClipsData : ScriptableObject
    {
        public AudioClip bgm;
        public AudioClip hitBrickSFX, hitPlateSFX;
        //public AudioClip buttonHighlightedSFX, buttonPressedSFX;
        //public AudioClip stageClearSFX, gameFailSFX;
        //public AudioClip playerMove;
        //public AudioClip sampleSFX;
    }
}