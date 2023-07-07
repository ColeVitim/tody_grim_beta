using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour
{

    public bool openfromOther;
    public bool closeKey;
    public bool isFacingLeft;
    public bool isFacingRight;
    public int _mapV;
    public int _areaV;

    [SerializeField]
    private GameObject destino;
  

    public Transform GetDestino()
    {
        destino.GetComponent<Porta>().openfromOther = false;        
        return destino.transform;
    }


}
