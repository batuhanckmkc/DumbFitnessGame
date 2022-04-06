using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject[] dumbleType;
    //[SerializeField] GameObject gDumble;
    //[SerializeField] GameObject rDumble;
    [SerializeField] public List<GameObject> dumbleGenerator;
    [SerializeField] int _dumbleGenerateNumber;
    [SerializeField] float minXRange;
    [SerializeField] float maxXRange;
    [SerializeField] Vector3 dumbleDistance;
    private float minZRange; /*3f
    private float maxZRange; /*20f*/
    Vector3 randomVec;

    void Start()
    {

    }

    void Update()
    {
        DumbleGenerator();
    }

    private void DumbleGenerator()
    {
        if (_dumbleGenerateNumber > dumbleGenerator.Count)
        {
            for (int i = 0; i < _dumbleGenerateNumber; i++)
            {
                int randomXDumble = Random.Range(0, 2);
                dumbleDistance.z += 0.5f;
                //dumbleDistance.z += Random.Range(0.5f, 2f);
                //float randomZRange = Random.Range(3f, 7f);
                if (randomXDumble == 0)
                {
                    randomVec = new Vector3(minXRange, 0.51f, dumbleDistance.z);
                }
                if (randomXDumble == 1)
                {
                    randomVec = new Vector3(maxXRange, 0.51f, dumbleDistance.z);
                }

                int randomDumbleColor = Random.Range(0, 2);
                GameObject dumble = Instantiate(dumbleType[randomDumbleColor], randomVec, Quaternion.identity, transform);
                dumbleGenerator.Add(dumble);
            }
        }
    }
}
