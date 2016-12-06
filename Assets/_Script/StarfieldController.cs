using UnityEngine;
using System.Collections;

public class StarfieldController : MonoBehaviour {
    private static StarfieldController instance;
    private float speed;

    [SerializeField]
    private ParticleSystem[] particleSystems;

    private Vector3 lastCameraPos;
    private bool isStart = true;

    private Transform camera;
	
    // Use this for initialization
	void Awake () {
        instance = this;
	}

    void Start()
    {
        camera = GameObject.Find("MultipurposeCameraRig").transform;
        lastCameraPos = new Vector3(-17.77999f, 1.32f, 0f);
    }

    public static void SetSpeed(float speed)
    {
        if (instance != null)
            instance._SetSpeed(speed);
    }

    private void _SetSpeed(float speed)
    {
        this.speed = speed;
    }

    void Update()
    {
        speed = (camera.position.x - lastCameraPos.x) * 50;
        lastCameraPos = camera.position;
    }

    void LateUpdate()
    {
        foreach (ParticleSystem particle in particleSystems)
        {
            if (particle == null)
                continue;
            ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particle.particleCount];
            Vector3 vec = new Vector3(speed * .95f, 0, 0);
            int count = particle.GetParticles(particles);
            for (int i = 0; i < count; i++)
            {
                particles[i].velocity = vec;
            }
            particle.SetParticles(particles, count);
        }
    }

    void OnDestroy()
    {
        instance = null;
    }
}
