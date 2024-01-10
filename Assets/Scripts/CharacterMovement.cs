using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;

public class CharacterMovement : MonoBehaviour
{
    private float speed = 5f;
    private float maxStamina = 100f;
    private float stamina;
    private float agility = 60f;
    [SerializeField]
    private NavMeshAgent agent;
    private Vector3 destination;

    public void SetStats()
    { 
        //set random stats
        speed = Random.Range(1f, 10f);
        maxStamina = Random.Range(60f, 100f);
        stamina = maxStamina;
        agility = Random.Range(1f, 6f)*60;
        agent.speed = speed;
        agent.angularSpeed = agility;
    }

    public async Task MoveTo(Vector3 destination)
    {
        this.destination = destination;
        //move to destination point
        agent.SetDestination(destination);
        //check if destination point is reached
        while (Vector3.Distance(transform.position, destination) > 0.1f)
        {
            //check if character is moving
            if (agent.velocity.magnitude > 0.1f)
            {
                //reduce stamina
                stamina -= 10f;
                //check if stamina is not empty
                if (stamina <= 0)
                {
                    //stop character
                    agent.isStopped = true;
                    stamina = 0;
                    //wait for stamina to regenerate
                    await RegenStamina();
                    //start moving again
                    agent.isStopped = false;
                }
            }
            await Task.Delay(500);
        }
    }

    //find destination point around given point
    public Vector3 FollowTo(Vector3 destination, float maxRange)
    {
        //get furthest point around destination point
        for (float x = destination.x - maxRange; x < destination.x + maxRange; x+=1.5f)
        {
            for (float z = destination.z - maxRange; z < destination.z + maxRange; z+=1.5f)
            {
                //check if in this place is anything else then terrain
                if (Physics.Raycast(new Vector3(x, 100, z), Vector3.down, out RaycastHit hit))
                {
                    if (hit.collider.gameObject.tag == "Ground")
                    {
                        //get all charcters
                        CharacterControl[] characters = GameObject.FindObjectsOfType<CharacterControl>();
                        //check if any character have this destination point
                        bool isFree = true;
                        foreach (CharacterControl character in characters)
                        {
                            Debug.Log("CharacterMovement loop");
                            Debug.Log("CharacterMovement:" + hit.point);
                            Debug.Log(character.GetDestination());
                            if (character.GetDestination() == hit.point)
                            {
                                
                                if(character.gameObject != gameObject)
                                {
                                    isFree = false;
                                    break;
                                }
                            }
                        }
                        if(isFree)
                        { 
                            Debug.Log("CharacterMovement: Choosen DestinationPoint = " + hit.point);
                            return hit.point; 
                        }
                    }
                }
            }
        }
        return destination;
    }   

    //get destination point
    public Vector3 GetDestination()
    {
        return destination;
    }

    //regenerating stamina
    private async Task RegenStamina()
    {
        while (stamina < maxStamina)
        {
            ChangeStamina(maxStamina/10);
            await Task.Delay(500);
        }
        return;
    }

    //change stamina value
    public void ChangeStamina(float value)
    {
        stamina += value;
        //check if stamina is not over max value
        if (stamina > maxStamina)
        {
            stamina = maxStamina;
        }
    }

    //get stamina value
    public float GetStamina()
    {
        return stamina;
    }

    //get speed value
    public float GetSpeed()
    {
        return speed;
    }

    //get agility value
    public float GetAgility()
    {
        return agility;
    }
}
