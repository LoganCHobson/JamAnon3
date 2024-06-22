using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace SolarStudios
{
    [System.Serializable]
    public class Sound
    {
        public string soundName;
        public AudioClip clip;
        public AudioMixerGroup mixer;
        public bool mute;
        public bool playOnAwake;
        public bool Loop;


        [Range(0f, 256f)]
        public int priority;
        [Range(0f, 1f)]
        public float volume;
        [Range(-3f, 3f)]
        public float pitch;
        [Range(-1f, 1f)]
        public float stereoPan;
        [Range(0f, 1f)]
        public float spatialBlend;
        [Range(0f, 1.1f)]
        public float reverbZoneMix;
    }

    public class AudioManager : MonoBehaviour
    {
        private bool canPool;
        private object objectPool; //Incase the user does not intend to add the object pools cript to the project.
        public static AudioManager instance;

        public Queue<Sound> soundQueue;


        [SerializeField]
        private List<Sound> soundList = new List<Sound>();


        private void Awake()
        {
           
            soundQueue = new Queue<Sound>();
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);

              
            }
        }

        private void Start()
        {
            Type objectPoolType = Type.GetType("ObjectPool"); //Essentially checking if this is a thing in the project.

            if (objectPoolType != null)
            {
                canPool = true;
                objectPool = GetComponent<ObjectPool>();
            }
            else
            {
                canPool = false;
                Debug.Log("The SolarStudios Objectpool Script cannot be found in project. Objectpooling has been disabled.");
            }
        }

        private void Update()
        {
        }

        public void PlayAudioClip(string _soundName, GameObject _callerObject, bool _useObjectPool = true, bool _playQueued = false)
        {
            Sound sound = soundList.Find(s => s.soundName == _soundName);

            if (sound != null)
            {
                if (_playQueued)
                {
                    Transform queuedAudioTransform = _callerObject.transform.Find("Queued AudioSource");
                    if (queuedAudioTransform == null)
                    {
                        ObjectPool objectPoolComponent = (ObjectPool)objectPool;
                        GameObject queuedAudioSource = objectPoolComponent.Spawn(_callerObject.transform.position, transform.rotation);
                        queuedAudioSource.name = "Queued AudioSource";

                        soundQueue.Enqueue(sound);

                        KickOffQueue(_callerObject);
                    }
                    else
                    {
                        soundQueue.Enqueue(sound);
                    }

                }
                else
                {
                    if (canPool && _useObjectPool)
                    {
                        PlayPooled(_callerObject, sound);
                    }
                    else
                    {
                        PlayNormal(_callerObject, sound);
                    }

                }
            }
            else
            {
                Debug.LogError($"Sound with name '{_soundName}' not found in the sound list.");
            }
        }

        void SourceSetup(AudioSource _source, Sound _sound)
        {
            _source.clip = _sound.clip;
            _source.outputAudioMixerGroup = _sound.mixer;
            _source.mute = _sound.mute;
            _source.playOnAwake = _sound.playOnAwake;
            _source.loop = _sound.Loop;
            _source.priority = _sound.priority;
            _source.volume = _sound.volume;
            _source.pitch = _sound.pitch;
            _source.panStereo = _sound.stereoPan;
            _source.spatialBlend = _sound.spatialBlend;
            _source.reverbZoneMix = _sound.reverbZoneMix;
        }

        void PlayPooled(GameObject _callerObject, Sound _sound)
        {

            ObjectPool objectPoolComponent = (ObjectPool)objectPool;
            GameObject audioSource = objectPoolComponent.Spawn(_callerObject.transform.position, _callerObject.transform.rotation);
            AudioSource source = audioSource.GetComponent<AudioSource>();
            SourceSetup(source, _sound);

            audioSource.name = _sound.soundName + " Audio clip";


            source.Play();
            objectPoolComponent.Recycle(audioSource, _sound.clip.length);

        }

        void PlayNormal(GameObject _callerObject, Sound _sound)
        {
            GameObject audioSource = new GameObject("AudioSource");
            audioSource.transform.position = _callerObject.transform.position;
            audioSource.transform.parent = _callerObject.transform;
            audioSource.name = _sound.soundName + " Audio clip";

            AudioSource source = audioSource.AddComponent<AudioSource>();
            SourceSetup(source, _sound);


            source.Play();
            Destroy(audioSource, _sound.clip.length);
        }
        void KickOffQueue(GameObject _callerObject)
        {
            ObjectPool objectPoolComponent = (ObjectPool)objectPool;
            AudioSource source = _callerObject.transform.Find("Queued AudioSource").GetComponent<AudioSource>();
            //PlayNext();
        }
        /*
        void PlayNext()
        {
            Sound nextSound = soundQueue.Dequeue();
            SourceSetup(source, nextSound);
            source.Play();
            StartCoroutine(WaitForClip(nextSound.clip.length, PlayNext));
        }
            if(soundQueue.Count <= 0)
            {
                objectPoolComponent.Recycle(source.gameObject, soundQueue.LastOrDefault()?.clip.length ?? 0);

            }

    IEnumerator WaitForClip(float duration)
    {
        yield return new WaitForSeconds(duration);
        PlayNext();
    }
        */
    }
}
