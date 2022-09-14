using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
    GameManager gM;
    MeshRenderer meshRenderer;

    public GameObject contains;
    public GameManager.State currentState;
    public List<Material> dead;
    public List<Material> live;
    public int fossilSpawnRate;

    // Start is called before the first frame update
    void Start()
    {
        gM = GameManager.instance;
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Refresh();
    }

    void Refresh(){
        switch (currentState){
            case GameManager.State.dead:
                meshRenderer.material = dead[0/*Random.Range(0, dead.Count)*/];
                break;
            case GameManager.State.live:
                meshRenderer.material = live[Random.Range(0, live.Count)];
                break;
        }
    }
}
