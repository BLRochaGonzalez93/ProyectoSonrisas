using System.Collections.Generic;
using UnityEngine;

public class RailSelectorManagement : MonoBehaviour
{
    public List<Rail> railPrefabs;
    public List<Rail> typeSRail, typeARail, typeBRail, typeCRail;

    public int previousID = 0;
    public int maxTypeARandoms = 5, maxTypeBRandoms = 3, maxTypeCRandoms = 1;
    public int typeARandoms = 5, typeBRandoms = 3, typeCRandoms = 1;

    /*
    private float timer = 0;
    private float totalTimer = 0;
    */

    [Header("Pruebas")]
    public string time = "";
    public int S_Recta, A_Curva90Der, A_Curva90Izq, A_Curva45Der, A_Curva45Izq, A_CambioSentidoDer, A_CambioSentidoIzq, A_SlalonDer, A_SlalonIzq,
        B_Up1Altura, B_Up2Altura, B_Down1Altura, B_Down2Altura, B_Caida1Altura, B_Caida2Altura, B_Subida1Altura, B_Subida2Altura, B_BacheSimple, B_BacheDoble, B_DepresionSimple, B_DepresionDoble, B_Valle, B_Monte,
        C_Bifurcacion45, C_Bifurcacion90, C_JumpCorto, C_JumpLargo, C_LoopingSimple, C_LoopingDoble, C_CurlDer, C_CurlIzq, C_SacacorchosDer, C_SacacorchosIzq;


    // Start is called before the first frame update
    void Start()
    {
        railPrefabs.Sort(Rail.SortByID);
        for (int i = 0; i < railPrefabs.Count; i++)
        {
            if (railPrefabs[i].type == RailType.TYPE_S) typeSRail.Add(railPrefabs[i]);
            if (railPrefabs[i].type == RailType.TYPE_A) typeARail.Add(railPrefabs[i]);
            if (railPrefabs[i].type == RailType.TYPE_B) typeBRail.Add(railPrefabs[i]);
            if (railPrefabs[i].type == RailType.TYPE_C) typeCRail.Add(railPrefabs[i]);

            railPrefabs[i].canBeSelected = true;
            railPrefabs[i].cooldown = 0;
        }
    }

    private void Update()
    {
        /*
        timer += Time.deltaTime;
        totalTimer += Time.deltaTime;
        time = "PRUEBA: " + string.Format("{0}:{1:00}", (int)totalTimer / 60, (int)totalTimer % 60);
        if (timer > .5f)
        {
            RailSelector();
            timer = 0;
        }
        */
    }

    public Rail RailSelector()
    {
        int randomType = Random.Range(0, 100);
        Rail rail = new();
        switch (randomType)
        {
            case < 10:
                rail = typeSRail[0];
                //Pruebas
                S_Recta++;//

                typeARandoms = maxTypeARandoms;
                typeBRandoms = maxTypeBRandoms;
                typeCRandoms = maxTypeCRandoms;
                break;
            case < 65:
                if (typeARandoms == 0)
                {
                    rail = typeSRail[0];
                    //Pruebas
                    S_Recta++;//

                    typeARandoms = maxTypeARandoms;
                    typeBRandoms = maxTypeBRandoms;
                    typeCRandoms = maxTypeCRandoms;
                }
                else
                {
                    int randomPrefabTypeA = Random.Range(0, 100);
                    switch (randomPrefabTypeA)
                    {
                        case < 15:

                            if (previousID == typeARail[0].ID || typeARail[0].canBeSelected == false)
                            {
                                rail = RailSelector();
                            }
                            else
                            {
                                rail = CheckRailSelected(typeARail[0]);

                                //Pruebas
                                A_Curva90Der++;//
                            }
                            break;
                        case < 30:
                            if (previousID == typeARail[1].ID || typeARail[1].canBeSelected == false)
                            {
                                rail = RailSelector();
                            }
                            else
                            {
                                rail = CheckRailSelected(typeARail[1]);

                                //Pruebas
                                A_Curva90Izq++;//
                            }
                            break;
                        case < 45:
                            if (previousID == typeARail[2].ID || typeARail[2].canBeSelected == false)
                            {
                                rail = RailSelector();
                            }
                            else
                            {
                                rail = CheckRailSelected(typeARail[2]);

                                //Pruebas
                                A_Curva45Der++;//
                            }
                            break;
                        case < 60:
                            if (previousID == typeARail[3].ID || typeARail[3].canBeSelected == false)
                            {
                                rail = RailSelector();
                            }
                            else
                            {
                                rail = CheckRailSelected(typeARail[3]);

                                //Pruebas
                                A_Curva45Izq++;//
                            }
                            break;
                        case < 65:
                            if (previousID == typeARail[4].ID || typeARail[4].canBeSelected == false)
                            {
                                rail = RailSelector();
                            }
                            else
                            {
                                rail = CheckRailSelected(typeARail[4]);

                                //Pruebas
                                A_CambioSentidoDer++;//
                            }
                            break;
                        case < 70:
                            if (previousID == typeARail[5].ID || typeARail[5].canBeSelected == false)
                            {
                                rail = RailSelector();
                            }
                            else
                            {
                                rail = CheckRailSelected(typeARail[5]);

                                //Pruebas
                                A_CambioSentidoIzq++;//
                            }
                            break;
                        case < 85:
                            if (previousID == typeARail[6].ID || typeARail[6].canBeSelected == false)
                            {
                                rail = RailSelector();
                            }
                            else
                            {
                                rail = CheckRailSelected(typeARail[6]);

                                //Pruebas
                                A_SlalonDer++;//
                            }
                            break;
                        case > 84:
                            if (previousID == typeARail[7].ID || typeARail[7].canBeSelected == false)
                            {
                                rail = RailSelector();
                            }
                            else
                            {
                                rail = CheckRailSelected(typeARail[7]);

                                //Pruebas
                                A_SlalonIzq++;//
                            }
                            break;
                    }
                    typeARandoms--;
                    typeBRandoms = maxTypeBRandoms;
                    typeCRandoms = maxTypeCRandoms;
                }
                break;
            case < 90:
                if (typeBRandoms == 0)
                {
                    rail = typeSRail[0];
                    //Pruebas
                    S_Recta++;//

                    typeARandoms = maxTypeARandoms;
                    typeBRandoms = maxTypeBRandoms;
                    typeCRandoms = maxTypeCRandoms;
                }
                else
                {
                    int randomPrefabTypeB = Random.Range(0, 100);
                    switch (randomPrefabTypeB)
                    {
                        case < 10:
                            if (previousID == typeBRail[0].ID || typeBRail[0].canBeSelected == false)
                            {
                                rail = RailSelector();
                            }
                            else
                            {
                                rail = CheckRailSelected(typeBRail[0]);

                                //Pruebas
                                B_Up1Altura++;//
                            }
                            break;
                        case < 18:
                            if (previousID == typeBRail[1].ID || typeBRail[1].canBeSelected == false)
                            {
                                rail = RailSelector();
                            }
                            else
                            {
                                rail = CheckRailSelected(typeBRail[1]);

                                //Pruebas
                                B_Up2Altura++;//
                            }
                            break;
                        case < 28:
                            if (previousID == typeBRail[2].ID || typeBRail[2].canBeSelected == false)
                            {
                                rail = RailSelector();
                            }
                            else
                            {
                                rail = CheckRailSelected(typeBRail[2]);

                                //Pruebas
                                B_Down1Altura++;//
                            }
                            break;
                        case < 36:
                            if (previousID == typeBRail[3].ID || typeBRail[3].canBeSelected == false)
                            {
                                rail = RailSelector();
                            }
                            else
                            {
                                rail = CheckRailSelected(typeBRail[3]);

                                //Pruebas
                                B_Down2Altura++;//
                            }
                            break;
                        case < 46:
                            if (previousID == typeBRail[4].ID || typeBRail[4].canBeSelected == false)
                            {
                                rail = RailSelector();
                            }
                            else
                            {
                                rail = CheckRailSelected(typeBRail[4]);

                                //Pruebas
                                B_Caida1Altura++;//
                            }
                            break;
                        case < 53:
                            if (previousID == typeBRail[5].ID || typeBRail[5].canBeSelected == false)
                            {
                                rail = RailSelector();
                            }
                            else
                            {
                                rail = CheckRailSelected(typeBRail[5]);

                                //Pruebas
                                B_Caida2Altura++;//
                            }
                            break;
                        case < 63:
                            if (previousID == typeBRail[6].ID || typeBRail[6].canBeSelected == false)
                            {
                                rail = RailSelector();
                            }
                            else
                            {
                                rail = CheckRailSelected(typeBRail[6]);

                                //Pruebas
                                B_Subida1Altura++;//
                            }
                            break;
                        case < 70:
                            if (previousID == typeBRail[7].ID || typeBRail[7].canBeSelected == false)
                            {
                                rail = RailSelector();
                            }
                            else
                            {
                                rail = CheckRailSelected(typeBRail[7]);

                                //Pruebas
                                B_Subida2Altura++;//
                            }
                            break;
                        case < 73:
                            if (previousID == typeBRail[8].ID || typeBRail[8].canBeSelected == false)
                            {
                                rail = RailSelector();
                            }
                            else
                            {
                                rail = CheckRailSelected(typeBRail[8]);

                                //Pruebas
                                B_BacheSimple++;//
                            }
                            break;
                        case < 75:
                            if (previousID == typeBRail[9].ID || typeBRail[9].canBeSelected == false)
                            {
                                rail = RailSelector();
                            }
                            else
                            {
                                rail = CheckRailSelected(typeBRail[9]);

                                //Pruebas
                                B_BacheDoble++;//
                            }
                            break;
                        case < 78:
                            if (previousID == typeBRail[10].ID || typeBRail[10].canBeSelected == false)
                            {
                                rail = RailSelector();
                            }
                            else
                            {
                                rail = CheckRailSelected(typeBRail[10]);

                                //Pruebas
                                B_DepresionSimple++;//
                            }
                            break;
                        case < 80:
                            if (previousID == typeBRail[11].ID || typeBRail[11].canBeSelected == false)
                            {
                                rail = RailSelector();
                            }
                            else
                            {
                                rail = CheckRailSelected(typeBRail[11]);

                                //Pruebas
                                B_DepresionDoble++;//
                            }
                            break;
                        case < 90:
                            if (previousID == typeBRail[12].ID || typeBRail[12].canBeSelected == false)
                            {
                                rail = RailSelector();
                            }
                            else
                            {
                                rail = CheckRailSelected(typeBRail[12]);

                                //Pruebas
                                B_Valle++;//
                            }
                            break;
                        case > 89:
                            if (previousID == typeBRail[13].ID || typeBRail[13].canBeSelected == false)
                            {
                                rail = RailSelector();
                            }
                            else
                            {
                                rail = CheckRailSelected(typeBRail[13]);

                                //Pruebas
                                B_Monte++;//
                            }
                            break;
                    }
                    typeARandoms = maxTypeARandoms;
                    typeBRandoms--;
                    typeCRandoms = maxTypeCRandoms;
                }
                break;
            case > 89:
                if (typeCRandoms == 0)
                {
                    rail = typeSRail[0];
                    //Pruebas
                    S_Recta++;//

                    typeARandoms = maxTypeARandoms;
                    typeBRandoms = maxTypeBRandoms;
                    typeCRandoms = maxTypeCRandoms;
                }
                else
                {
                    int randomPrefabTypeC = Random.Range(0, 100);
                    switch (randomPrefabTypeC)
                    {
                        case < 20:
                            if (previousID == typeCRail[0].ID || typeCRail[0].canBeSelected == false)
                            {
                                rail = RailSelector();
                            }
                            else
                            {
                                rail = CheckRailSelected(typeCRail[0]);

                                //Pruebas
                                C_Bifurcacion45++;//
                            }
                            break;
                        case < 40:
                            if (previousID == typeCRail[1].ID || typeCRail[1].canBeSelected == false)
                            {
                                rail = RailSelector();
                            }
                            else
                            {
                                rail = CheckRailSelected(typeCRail[1]);

                                //Pruebas
                                C_Bifurcacion90++;//
                            }
                            break;
                        case < 60:
                            if (previousID == typeCRail[2].ID || typeCRail[2].canBeSelected == false)
                            {
                                rail = RailSelector();
                            }
                            else
                            {
                                rail = CheckRailSelected(typeCRail[2]);

                                //Pruebas
                                C_JumpCorto++;//
                            }
                            break;
                        case < 68:
                            if (previousID == typeCRail[3].ID || typeCRail[3].canBeSelected == false)
                            {
                                rail = RailSelector();
                            }
                            else
                            {
                                rail = CheckRailSelected(typeCRail[3]);

                                //Pruebas
                                C_JumpLargo++;//
                            }
                            break;
                        case < 73:
                            if (previousID == typeCRail[4].ID || typeCRail[4].canBeSelected == false)
                            {
                                rail = RailSelector();
                            }
                            else
                            {
                                rail = CheckRailSelected(typeCRail[4]);

                                //Pruebas
                                C_LoopingSimple++;//
                            }
                            break;
                        case < 76:
                            if (previousID == typeCRail[5].ID || typeCRail[5].canBeSelected == false)
                            {
                                rail = RailSelector();
                            }
                            else
                            {
                                rail = CheckRailSelected(typeCRail[5]);

                                //Pruebas
                                C_LoopingDoble++;//
                            }
                            break;
                        case < 84:
                            if (previousID == typeCRail[6].ID || typeCRail[6].canBeSelected == false)
                            {
                                rail = RailSelector();
                            }
                            else
                            {
                                rail = CheckRailSelected(typeCRail[6]);

                                //Pruebas
                                C_CurlDer++;//
                            }
                            break;
                        case < 92:
                            if (previousID == typeCRail[7].ID || typeCRail[7].canBeSelected == false)
                            {
                                rail = RailSelector();
                            }
                            else
                            {
                                rail = CheckRailSelected(typeCRail[7]);

                                //Pruebas
                                C_CurlIzq++;//
                            }
                            break;
                        case < 96:
                            if (previousID == typeCRail[8].ID || typeCRail[8].canBeSelected == false)
                            {
                                rail = RailSelector();
                            }
                            else
                            {
                                rail = CheckRailSelected(typeCRail[8]);

                                //Pruebas
                                C_SacacorchosDer++;//
                            }
                            break;
                        case > 96:
                            if (previousID == typeCRail[9].ID || typeCRail[9].canBeSelected == false)
                            {
                                rail = RailSelector();
                            }
                            else
                            {
                                rail = CheckRailSelected(typeCRail[9]);

                                //Pruebas
                                C_SacacorchosIzq++;//
                            }
                            break;
                    }
                    typeARandoms = maxTypeARandoms;
                    typeBRandoms = maxTypeBRandoms;
                    typeCRandoms--;
                }
                break;
        }
        Debug.Log("Ha aparecido el rail: " + rail.name);
        previousID = rail.ID;
        return rail;
    }

    private Rail CheckRailSelected(Rail railSelected)
    {
        Rail rail = railSelected;
        railSelected.canBeSelected = false;
        for (int i = 0; i < railPrefabs.Count; i++)
        {
            if (railPrefabs[i].canBeSelected == false)
            {
                railPrefabs[i].cooldown++;
            }

            if (railPrefabs[i].cooldown > 3)
            {
                railPrefabs[i].canBeSelected = true;
                railPrefabs[i].cooldown = 0;
            }
        }

        return rail;
    }
}
