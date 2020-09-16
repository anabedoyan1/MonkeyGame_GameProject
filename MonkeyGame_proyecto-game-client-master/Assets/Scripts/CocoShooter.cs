using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CocoShooter : GameAgent
{

[SerializeField]GameObject coco;

    float cocoRate=2;
    string lado;

    Manager mg;


        void Start()
        {
            lado="RIGHT";
            mg = GameObject.FindGameObjectWithTag("MG").GetComponent<Manager>();
        }

    public override void ChangeSide(Commands _command)
    {
        Debug.Log(m_team + " " + m_rol + " " + " dice: mi dueño me ha movido para la " + _command);
        lado=_command.ToString();

    }

    void Update()
    {
        if(mg.play==true)
        {
        if(cocoRate>0)
        {
            cocoRate-=Time.deltaTime;
        }

        if(cocoRate<=0)
        {
            if(lado=="RIGHT")
        {
            Instantiate(coco,new Vector3(transform.position.x+1,transform.position.y,transform.position.z),transform.rotation);
        }

        if(lado=="LEFT")
        {
            Instantiate(coco,new Vector3(transform.position.x-1,transform.position.y,transform.position.z),transform.rotation);


        }
        cocoRate=2;
        }
        }
    }
}
