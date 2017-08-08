using UnityEngine;

/// <summary>
/// The Interactible class flags a Game Object as being "Interactible".
/// Determines what happens when an Interactible is being gazed at.
/// </summary>
public class Interactible : MonoBehaviour
{
    [Tooltip("Audio clip to play when interacting with this hologram.")]
    public AudioClip TargetFeedbackSound;
	public GameObject oVoiceCommandObj;
	private AudioSource audioSource;

    private Material[] defaultMaterials;

    void Start()
    {
        defaultMaterials = GetComponent<Renderer>().materials;

        // Add a BoxCollider if the interactible does not contain one.
        Collider collider = GetComponentInChildren<Collider>();
        if (collider == null)
        {
            gameObject.AddComponent<BoxCollider>();
        }
		//disable all voice comand on start
		oVoiceCommandObj.GetComponent<Renderer> ().enabled = false;
        EnableAudioHapticFeedback();
    }

    private void EnableAudioHapticFeedback()
    {
        // If this hologram has an audio clip, add an AudioSource with this clip.
        if (TargetFeedbackSound != null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            audioSource.clip = TargetFeedbackSound;
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 1;
            audioSource.dopplerLevel = 0;
        }
    }

    /* TODO: DEVELOPER CODING EXERCISE 2.d */

    void GazeEntered()
    {
        for (int i = 0; i < defaultMaterials.Length; i++)
        {
            // 2.d: Uncomment the below line to highlight the material when gaze enters.
			Debug.Log("Enter");
			Vector3 dirToCamera = Camera.main.transform.position - this.transform.position;
			float dirToCameraMag = dirToCamera.magnitude;
			//making "click" like feeling to the button by moving it a bit towards the camera
			Vector3 temp = dirToCamera / dirToCameraMag *0.02f;
			this.transform.position += temp;
			defaultMaterials [i].color = Color.gray;
			audioSource.Play ();

        }
		//show command after 1.5 seconds
		Invoke ("ShowVoiceCommand", 1.5f);
    }

    void GazeExited()
    {
        for (int i = 0; i < defaultMaterials.Length; i++)
        {
            // 2.d: Uncomment the below line to remove highlight on material when gaze exits.
			Debug.Log("Exoited");
			Vector3 dirToCamera = Camera.main.transform.position - this.transform.position;
			float dirToCameraMag = dirToCamera.magnitude;
			//making "click" like feeling to the button by moving it a bit towards the camera
			Vector3 temp = dirToCamera / dirToCameraMag *-0.02f;
			this.transform.position += temp;
			defaultMaterials [i].color = Color.white;
        }
		CancelInvoke ();
		oVoiceCommandObj.GetComponent<Renderer> ().enabled = false;

    }

	void ShowVoiceCommand(){
		oVoiceCommandObj.GetComponent<Renderer> ().enabled = true;
	}
		
}