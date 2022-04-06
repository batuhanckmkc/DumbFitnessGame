using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollisionPlayer : MonoBehaviour
{
    [SerializeField] float sliderIncreaseValue;
    Movement movement;
    PowerBarController powerBarController;
    public bool IsFinished;
    public ParticleSystem proteinParticleEffect;
    public ParticleSystem wallParticleEffect;
    [SerializeField] VirtualCam virtualCam;
    [SerializeField] Transform _cam;
    [SerializeField] Transform finishLineCamPos;

    Animator stickmanAnim;

    private void Start()
    {
        movement = GetComponent<Movement>();
        powerBarController = GameObject.FindGameObjectWithTag("PowerBar").GetComponent<PowerBarController>();
        stickmanAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GreenDumbell")
        {
            movement.DumbleCollectUp();
            powerBarController.SliderUp(sliderIncreaseValue);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "RedDumbell")
        {
            movement.DumbleCollectDown();
            powerBarController.SliderDown(sliderIncreaseValue);
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "FinishLine")
        {
            movement.PlayBenchAnimation();
            IsFinished = true;
            virtualCam.FinishCamAngle(finishLineCamPos);
            GameManager.gameManager.SuccessScreen();
        }
        if (other.gameObject.tag == "BenchTrigger")
        {
            movement.DelayAction();
            movement.BenchTrigger();
        }
        if (other.gameObject.tag == "ProteinPowder")
        {
            Instantiate(proteinParticleEffect, other.gameObject.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            movement.ProteinCollect();
            powerBarController.SliderUp(sliderIncreaseValue / 3);
        }
        if(other.gameObject.tag == "BrickWall")
        {
            Instantiate(wallParticleEffect, other.gameObject.transform.position, Quaternion.identity);
            Vector3 vec = new Vector3(5, 2, 5);
            virtualCam.ForceCameraPosition(vec, Quaternion.identity);
            if (powerBarController.slider.value >= 0.2f)
            {
                other.GetComponent<RayFire.RayfireRigid>().Demolish();
            }
            else if (powerBarController.slider.value <= 0.2f)
            {
                stickmanAnim.SetBool("IsDead", true);
                movement._speedZ = 0;
                movement._speedX = 0;
                GameManager.gameManager.GameOver();
            }
  
        }
    }
}
