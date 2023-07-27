using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] FootstepAudioClips;

    [SerializeField] private AudioClip LandingAudioClip;

    [Range(0, 1)]
    [SerializeField] private float FootstepAudioVolume = 0.3f;

    public void PlayFootStep(AnimationEvent animationEvent, Vector3 point)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f && FootstepAudioClips.Length > 0)
        {
            var index = Random.Range(0, FootstepAudioClips.Length);
            AudioSource.PlayClipAtPoint(FootstepAudioClips[index], point, FootstepAudioVolume);
        }
    }

    public void PlayOnLand(AnimationEvent animationEvent, Vector3 point)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            AudioSource.PlayClipAtPoint(LandingAudioClip, point, FootstepAudioVolume);
        }
    }
}