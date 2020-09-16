using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : GameAgent
{
    public float posF;
    public float timeC=3;

    public GameObject golpe;

    Manager mg;

        float scaleL,scaleR;
        string lado;
        void Start()
        {
            scaleL=this.transform.localScale.x*-1;
            scaleR=this.transform.localScale.x;
            lado="RIGHT";
            mg = GameObject.FindGameObjectWithTag("MG").GetComponent<Manager>();
            baja=false;

        }

    public override void ChangeSide(Commands _command)
    {
        Debug.Log(m_team + " " + m_rol + " " + " dice: mi dueño me ha movido para la " + _command);
         lado=_command.ToString();



        if(mg.play==true)
        {
            if(lado=="RIGHT")
        {
            gameObject.transform.localScale = new Vector3(scaleR, transform.localScale.y, transform.localScale.z);
        }

        if(lado=="LEFT")
        {
            
            gameObject.transform.localScale = new Vector3(scaleL, transform.localScale.y, transform.localScale.z);

        }
        }

        

    }

    void Update()
    {
        if(mg.play==true)
        {
        if(transform.position.y<posF)
        {
            transform.position = new Vector3(transform.position.x,transform.position.y+0.01f,transform.position.z);

        }
        }

       
        

        // if(timeC>0)
        // {
        //     timeC-=Time.deltaTime;

        //     if(transform.position.y<posF&&timeC<=0)
        //     {
        //         transform.position = new Vector3(transform.position.x,transform.position.y+0.1f,transform.position.z);
        //         timeC=3;
        //     }
        
        
        // }
        if(baja==true&&cantidad>0)
        {
            cantidad-=0.1f;
            transform.position = new Vector3(transform.position.x,transform.position.y-0.1f,transform.position.z);
            if(cantidad<=0)
            {
                baja=false;
            }
            

        }
        
    }

bool baja;
float cantidad;
void OnTriggerEnter(Collider col) 
{
    {
        if (col.gameObject.CompareTag("coco"))
        {
            if(transform.position.y>-5f)
            {
                baja=true;
                cantidad=1;
                Instantiate(golpe,transform.position,transform.rotation);
            }
            Destroy(col.gameObject);
        } 
    }


 
    
}
}
