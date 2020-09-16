using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public float time;
    public Text timeText;
    public Text timeStart;
    public GameObject timeStartO;
    public Text ganadores;
    public GameObject ganadoresO;
    float timeBase;

    public bool play;

    string ganador;

    float startTime;

    [SerializeField]GameObject blueM,redM;
    
    // Start is called before the first frame update
    void Start()
    {
        timeBase=time;
        startTime=3;
        play=false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(blueM.transform.position.y>redM.transform.position.y)
        {
            ganador = "Azul";
        }
        else if(blueM.transform.position.y<redM.transform.position.y)
        {
            ganador = "Rojo";

        }
        else if(blueM.transform.position.y==redM.transform.position.y)
        {
            ganador = "Empate";

        }

if(startTime>0)
{
    startTime-=Time.deltaTime;

}
timeStart.text=startTime.ToString("f0");
if(startTime<=0&&timeBase>0)
{
    play=true;
        timeStartO.gameObject.SetActive(false);

}



        timeText.text=timeBase.ToString("f0");

        if(timeBase>0&&play==true)
        {
            timeBase-=Time.deltaTime;
        }

        if(timeBase<=0||blueM.transform.position.y>=9||redM.transform.position.y>=9)
        {
            play=false;
            timeBase=0;

            switch (ganador)
            {
                case "Azul":
                ganadores.text="Gana el equipo Azul!";
                ganadores.color=Color.blue;
                ganadoresO.gameObject.SetActive(true);

                break;

                case "Rojo":

                ganadores.text="Gana el equipo Rojo!";
                ganadores.color=Color.red;
                ganadoresO.gameObject.SetActive(true);



                break;


                case "Empate":

                ganadores.text="Es un empate!";
                ganadores.color=Color.white;
                ganadoresO.gameObject.SetActive(true);



                break;


                
            }
        }


    }

    public void Reset()
    {
        blueM.transform.position=new Vector3(blueM.transform.position.x,-5f,blueM.transform.position.z);
        redM.transform.position=new Vector3(redM.transform.position.x,-5f,redM.transform.position.z);
        startTime=3;
        timeBase=time;
        ganadoresO.gameObject.SetActive(false);
        timeStartO.gameObject.SetActive(true);


        
        
    }

    
}
