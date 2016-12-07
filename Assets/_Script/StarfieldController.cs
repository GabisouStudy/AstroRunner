using UnityEngine;
using System.Collections;

public class StarfieldController : MonoBehaviour {

    [SerializeField]
    private float zDistance = 1f;

    private float speed;

    [SerializeField]
    private ParticleSystem[] particleSystems;

    private Vector3 lastCameraPos;
    private bool isStart = true;

    private float calcR;

    private Transform camera;

    void Start()
    {
        camera = GameObject.Find("MultipurposeCameraRig").transform;
        lastCameraPos = new Vector3(-17.77999f, 1.32f, 0f);
        UpdateCalcR();
    }

    void Update()
    {
        speed = (camera.position.x - lastCameraPos.x) * 50;
        lastCameraPos = camera.position;
    }

    void UpdateCalcR()
    {
        calcR = 1 - (1 / zDistance);
    }

    void LateUpdate()
    {
        foreach (ParticleSystem particle in particleSystems)
        {
            if (particle == null)
                continue;
            ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particle.particleCount];
            Vector3 vec = new Vector3(speed * calcR, 0, 0);
            int count = particle.GetParticles(particles);
            for (int i = 0; i < count; i++)
            {
                particles[i].velocity = vec;
            }
            particle.SetParticles(particles, count);
        }
    }
}
