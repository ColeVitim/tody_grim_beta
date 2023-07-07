using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    public static int _map;
    public static int _area;


    [SerializeField] GameObject Map1;
    [SerializeField] GameObject Map2;
    [SerializeField] GameObject Map3;
    [SerializeField] GameObject Map4;
    [SerializeField] GameObject Map5;
    [SerializeField] GameObject Map6;
    [SerializeField] GameObject Map7;
    [SerializeField] GameObject Map8;
    [SerializeField] GameObject Map9;
    [SerializeField] GameObject Map10;
    [SerializeField] GameObject Map11;
    [SerializeField] GameObject Map12;
    [SerializeField] GameObject Map13;
    [SerializeField] GameObject Map14;
    [SerializeField] GameObject Map15;
    [SerializeField] GameObject Map16;
    [SerializeField] GameObject Map17;
    [SerializeField] GameObject Map18;
    [SerializeField] GameObject Map19;
    [SerializeField] GameObject Map20;
    [SerializeField] GameObject Map21;
    [SerializeField] GameObject Map22;
    [SerializeField] GameObject Map23;
    [SerializeField] GameObject Map24;
    [SerializeField] GameObject Map25;
    [SerializeField] GameObject Map26;
    [SerializeField] GameObject Map27;
    [SerializeField] GameObject Map28;
    [SerializeField] GameObject Map29;
    [SerializeField] GameObject Map30;

    [SerializeField] GameObject A1;
    [SerializeField] GameObject A2;
    [SerializeField] GameObject A3;
    [SerializeField] GameObject A4;
    [SerializeField] GameObject A5;
    [SerializeField] GameObject A6;
    [SerializeField] GameObject A7;
    [SerializeField] GameObject A8;
    [SerializeField] GameObject A9;
    [SerializeField] GameObject A10;
    [SerializeField] GameObject A11;
    [SerializeField] GameObject A12;
    [SerializeField] GameObject A13;
    [SerializeField] GameObject A14;
    [SerializeField] GameObject A15;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    public void MapUpdate()
    {
        if (Map1 !=null) Map1.SetActive(false);
        if (Map2 !=null) Map2.SetActive(false);
        if (Map3 !=null) Map3.SetActive(false);
        if (Map4 !=null) Map4.SetActive(false);
        if (Map5 !=null) Map5.SetActive(false);
        if (Map6 !=null) Map6.SetActive(false);
        if (Map7 !=null) Map7.SetActive(false);
        if (Map8 !=null) Map8.SetActive(false);
        if (Map9 !=null) Map9.SetActive(false);
        if (Map10 !=null) Map10.SetActive(false);
        if (Map11 !=null) Map11.SetActive(false);
        if (Map12 !=null) Map12.SetActive(false);
        if (Map13 !=null) Map13.SetActive(false);
        if (Map14 !=null) Map14.SetActive(false);
        if (Map15 !=null) Map15.SetActive(false);
        if (Map16 !=null) Map16.SetActive(false);
        if (Map17 !=null) Map17.SetActive(false);
        if (Map18 !=null) Map18.SetActive(false);
        if (Map19 !=null) Map19.SetActive(false);
        if (Map20 !=null) Map20.SetActive(false);
        if (Map21 !=null) Map21.SetActive(false);
        if (Map22 !=null) Map22.SetActive(false);
        if (Map23 !=null) Map23.SetActive(false);
        if (Map24 !=null) Map24.SetActive(false);
        if (Map25 !=null) Map25.SetActive(false);
        if (Map26 !=null) Map26.SetActive(false);
        if (Map27 !=null) Map27.SetActive(false);
        if (Map28 !=null) Map28.SetActive(false);
        if (Map29 !=null) Map29.SetActive(false);
        if (Map30 !=null) Map30.SetActive(false);

        if (A1 !=null) A1.SetActive(false);
        if (A2 !=null) A2.SetActive(false);
        if (A3 !=null) A3.SetActive(false);
        if (A4 !=null) A4.SetActive(false);
        if (A5 !=null) A5.SetActive(false);
        if (A6 !=null) A6.SetActive(false);
        if (A7 !=null) A7.SetActive(false);
        if (A8 !=null) A8.SetActive(false);
        if (A9 !=null) A9.SetActive(false);
        if (A10 !=null) A10.SetActive(false);
        if (A11 !=null) A11.SetActive(false);
        if (A12 !=null) A12.SetActive(false);
        if (A13 !=null) A13.SetActive(false);
        if (A14 !=null) A14.SetActive(false);
        if (A14 !=null) A14.SetActive(false);
   

        if (_map == 1) Map1.SetActive(true);
        else if (_map == 2) Map2.SetActive(true);
        else if (_map == 3) Map3.SetActive(true);
        else if (_map == 4) Map4.SetActive(true);
        else if (_map == 5) Map5.SetActive(true);
        else if (_map == 6) Map6.SetActive(true);
        else if (_map == 7) Map7.SetActive(true);
        else if (_map == 8) Map8.SetActive(true);
        else if (_map == 9) Map9.SetActive(true);
        else if (_map == 10) Map10.SetActive(true);
        else if (_map == 11) Map11.SetActive(true);
        else if (_map == 12) Map12.SetActive(true);
        else if (_map == 13) Map13.SetActive(true);
        else if (_map == 14) Map14.SetActive(true);
        else if (_map == 15) Map15.SetActive(true);
        else if (_map == 16) Map16.SetActive(true);
        else if (_map == 17) Map17.SetActive(true);
        else if (_map == 18) Map18.SetActive(true);
        else if (_map == 19) Map19.SetActive(true);
        else if (_map == 20) Map20.SetActive(true);
        else if (_map == 21) Map21.SetActive(true);
        else if (_map == 22) Map22.SetActive(true);
        else if (_map == 23) Map23.SetActive(true);
        else if (_map == 24) Map24.SetActive(true);
        else if (_map == 25) Map25.SetActive(true);
        else if (_map == 26) Map26.SetActive(true);
        else if (_map == 27) Map27.SetActive(true);
        else if (_map == 28) Map28.SetActive(true);
        else if (_map == 29) Map29.SetActive(true);
        else if (_map == 30) Map30.SetActive(true);

        if (_area == 1) A1.SetActive(true);
        else if (_area == 2) A2.SetActive(true);
        else if (_area == 3) A3.SetActive(true);
        else if (_area == 4) A4.SetActive(true);
        else if (_area == 5) A5.SetActive(true);
        else if (_area == 6) A6.SetActive(true);
        else if (_area == 7) A7.SetActive(true);
        else if (_area == 8) A8.SetActive(true);
        else if (_area == 9) A9.SetActive(true);
        else if (_area == 10) A10.SetActive(true);
        else if (_area == 11) A11.SetActive(true);
        else if (_area == 12) A12.SetActive(true);
        else if (_area == 13) A13.SetActive(true);
        else if (_area == 14) A14.SetActive(true);
        else if (_area == 15) A15.SetActive(true);
    
    }

}                                                       

