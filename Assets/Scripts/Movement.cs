using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System;
public enum Selections { 
    Level1,
    Level2,
    Level3,
    Level4,
    Level5
}
public class Movement : MonoBehaviour
{

    public Selections selections;
    private float _boundrey = 0.34f;
    private float _moveX;
    private float _lastPos;
    [SerializeField] public float _speedX;
    [SerializeField] public float _speedZ;
    [SerializeField] private float _growthRate;
    [SerializeField] private float _growthLerpTime;
    [SerializeField] Transform benchPosition;
    [SerializeField] Transform barPosition;
    [SerializeField] GameObject bar;
    private Vector3 vec;
    Animator anim;

 
    private void Start()
    {
        vec = transform.localScale;
        anim = GetComponent<Animator>();

    }
    void Update()
    {

        InputSystem();
        MoveSystem();

        GrowthLerp();

        anim.SetFloat("Speed", 5f);
    }

    private void InputSystem()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastPos = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            _moveX = _lastPos - Input.mousePosition.x;
            _lastPos = Input.mousePosition.x;
        }
        else
        {
            _moveX = 0;
        }

        float xBondrey = Mathf.Clamp(transform.position.x, -_boundrey, _boundrey);
        transform.position = new Vector3(xBondrey, transform.position.y, transform.position.z);
    }

    private void MoveSystem()
    {
        float swipe = Time.fixedDeltaTime * _moveX * _speedX;
        transform.Translate(swipe, 0, -_speedZ * Time.deltaTime);
    }

    public void DumbleCollectUp()
    {
        float maxGrowth = 1.75f;
        if (transform.localScale.x <= maxGrowth && transform.localScale.y <= maxGrowth)
        {
            vec += new Vector3(_growthRate, _growthRate, _growthRate);
        }
    }
    public void DumbleCollectDown()
    {
        float minGrowth = 0.90f;
  
        if (transform.localScale.x > minGrowth && transform.localScale.y > minGrowth)
        {
            vec -= new Vector3(_growthRate, _growthRate, _growthRate);
        }
    }
    
    public void PlayBenchAnimation()
    {
        if (selections == Selections.Level1)
        {
            anim.SetBool("IsFinished", true);
            transform.DOMove(benchPosition.position, 1.2f);
            _speedZ = 0f;
            _speedX = 0f;
        }
       
    }
    public void ProteinCollect()
    {
        float _proteinGrowthValue = 0.1f;
        vec += new Vector3(_proteinGrowthValue, _proteinGrowthValue, _proteinGrowthValue);
    }
    public IEnumerator DelayAction()
    {
        //bar.transform.position = barPosition.position;
        yield return new WaitForSeconds(0.5f);
    }
    public void BenchTrigger()
    {
        
        bar.transform.parent = barPosition.transform;
        bar.transform.position = barPosition.position;
        //bar.transform.DOMove(barPosition.position, 5f);


        if (transform.localScale.x < 1f)
        {
            bar.transform.localScale -= transform.localScale;
        }
        if (transform.localScale.x > 1f)
        {
            bar.transform.localScale += transform.localScale;
        }

    

    }
    
    private void GrowthLerp()
    {
        transform.localScale = new Vector3(
          Mathf.Lerp(transform.localScale.x, vec.x, _growthLerpTime * Time.deltaTime),
           Mathf.Lerp(transform.localScale.y, vec.y, _growthLerpTime * Time.deltaTime),
            Mathf.Lerp(transform.localScale.z, vec.z, _growthLerpTime * Time.deltaTime)

          );
    }

   
}
