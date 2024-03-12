using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RailSelectorManagement : MonoBehaviour
{
    public List<Rail> railPrefabs;
    public List<Rail> typeSRail, typeARail, typeBRail, typeCRail;

    public int previousID = 0;
    public int maxTypeARandoms = 5, maxTypeBRandoms = 3, maxTypeCRandoms = 1;
    public int typeARandoms = 5, typeBRandoms = 3, typeCRandoms = 1;

    [Header("Pruebas")]
    public float timer = 0;
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
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 5f) {
            RailSelector();
            timer = 0;
        }
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
                            rail = typeARail[0];
                            //Pruebas
                            A_Curva90Der++;//
                            break;
                        case < 30:
                            rail = typeARail[1];
                            //Pruebas
                            A_Curva90Izq++;//
                            break;
                        case < 45:
                            rail = typeARail[2];
                            //Pruebas
                            A_Curva45Der++;//
                            break;
                        case < 60:
                            rail = typeARail[3];
                            //Pruebas
                            A_Curva45Izq++;//
                            break;
                        case < 65:
                            rail = typeARail[4];
                            //Pruebas
                            A_CambioSentidoDer++;//
                            break;
                        case < 70:
                            rail = typeARail[5];
                            //Pruebas
                            A_CambioSentidoIzq++;//
                            break;
                        case < 85:
                            rail = typeARail[6];
                            //Pruebas
                            A_SlalonDer++;//
                            break;
                        case > 84:
                            rail = typeARail[7];
                            //Pruebas
                            A_SlalonIzq++;//
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
                            rail = typeBRail[0];
                            //Pruebas
                            B_Up1Altura++;//
                            break;
                        case < 18:
                            rail = typeBRail[1];
                            //Pruebas
                            B_Up2Altura++;//
                            break;
                        case < 28:
                            rail = typeBRail[2];
                            //Pruebas
                            B_Down1Altura++;//
                            break;
                        case < 36:
                            rail = typeBRail[3];
                            //Pruebas
                            B_Down2Altura++;//
                            break;
                        case < 46:
                            rail = typeBRail[4];
                            //Pruebas
                            B_Caida1Altura++;//
                            break;
                        case < 53:
                            rail = typeBRail[5];
                            //Pruebas
                            B_Caida2Altura++;//
                            break;
                        case < 63:
                            rail = typeBRail[6];
                            //Pruebas
                            B_Subida1Altura++;//
                            break;
                        case < 70:
                            rail = typeBRail[7];
                            //Pruebas
                            B_Subida2Altura++;//
                            break;
                        case < 73:
                            rail = typeBRail[8];
                            //Pruebas
                            B_BacheSimple++;//
                            break;
                        case < 75:
                            rail = typeBRail[9];
                            //Pruebas
                            B_BacheDoble++;//
                            break;
                        case < 78:
                            rail = typeBRail[10];
                            //Pruebas
                            B_DepresionSimple++;//
                            break;
                        case < 80:
                            rail = typeBRail[11];
                            //Pruebas
                            B_DepresionDoble++;//
                            break;
                        case < 90:
                            rail = typeBRail[12];
                            //Pruebas
                            B_Valle++;//
                            break;
                        case > 89:
                            rail = typeBRail[13];
                            //Pruebas
                            B_Monte++;//
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
                            rail = typeCRail[0];
                            //Pruebas
                            C_Bifurcacion45++;//
                            break;
                        case < 40:
                            rail = typeCRail[1];
                            //Pruebas
                            C_Bifurcacion90++;//
                            break;
                        case < 60:
                            rail = typeCRail[2];
                            //Pruebas
                            C_JumpCorto++;//
                            break;
                        case < 68:
                            rail = typeCRail[3];
                            //Pruebas
                            C_JumpLargo++;//
                            break;
                        case < 73:
                            rail = typeCRail[4];
                            //Pruebas
                            C_LoopingSimple++;//
                            break;
                        case < 76:
                            rail = typeCRail[5];
                            //Pruebas
                            C_LoopingDoble++;//
                            break;
                        case < 84:
                            rail = typeCRail[6];
                            //Pruebas
                            C_CurlDer++;//
                            break;
                        case < 92:
                            rail = typeCRail[7];
                            //Pruebas
                            C_CurlIzq++;//
                            break;
                        case < 96:
                            rail = typeCRail[8];
                            //Pruebas
                            C_SacacorchosDer++;//
                            break;
                        case > 96:
                            rail = typeCRail[9];
                            //Pruebas
                            C_SacacorchosIzq++;//
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
}
