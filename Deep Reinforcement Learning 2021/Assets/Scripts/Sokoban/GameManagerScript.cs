using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> levels = new List<GameObject>();

    private GameObject joueur;
    private int currentLevel;
    private bool ingame = false;

    void LoadLevel(int ind)
    {
        currentLevel = ind;

        if (currentLevel >= levels.Count) Debug.Log("FIN DU JEU");
        else
        {
            for (int i = 0; i < levels.Count; i++)
            {
                if (i == ind)
                {
                    levels[i].SetActive(true);

                    foreach (Transform child in levels[i].transform)
                    {
                        if (child.tag == "joueur") joueur = child.gameObject;
                    }
                }
                else levels[i].SetActive(false);
            }

            ingame = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadLevel(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(ingame)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && VerifDirection(joueur.transform.forward))
            {
                var pos = joueur.transform.position;
                pos.z += 1;
                joueur.transform.position = pos;

                if (VerifVictoire())
                {
                    Debug.Log("Victoire");
                    ingame = false;
                    LoadLevel(currentLevel+1);
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && VerifDirection(-joueur.transform.forward))
            {
                var pos = joueur.transform.position;
                pos.z -= 1;
                joueur.transform.position = pos;

                if (VerifVictoire())
                {
                    Debug.Log("Victoire");
                    ingame = false;
                    LoadLevel(currentLevel+1);
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && VerifDirection(-joueur.transform.right))
            {
                var pos = joueur.transform.position;
                pos.x -= 1;
                joueur.transform.position = pos;

                if (VerifVictoire())
                {
                    Debug.Log("Victoire");
                    ingame = false;
                    LoadLevel(currentLevel+1);
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && VerifDirection(joueur.transform.right))
            {
                var pos = joueur.transform.position;
                pos.x += 1;
                joueur.transform.position = pos;

                if (VerifVictoire())
                {
                    Debug.Log("Victoire");
                    ingame = false;
                    LoadLevel(currentLevel+1);
                }
            }
        } 
    }

    public bool VerifDirection(Vector3 dir)
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        RaycastHit hit;

        //si le joueur est à coté d'un ojet
        if (Physics.Raycast(joueur.transform.position, dir, out hit, 1.1f, layerMask))
        {
            //si c'est un mur, le déplacement est impossible
            if (hit.collider.gameObject.tag == "mur") return false;
            else
            {
                if (hit.collider.gameObject.tag == "caisse")
                {
                    //si c'est une caisse, on vérifie la présence d'objet sur la case d'à côté
                    var colTrans = hit.collider.transform;
                    layerMask = 1 << 8;
                    layerMask = ~layerMask;

                    //s'il y a un objet, le déplacement est impossible
                    if (Physics.Raycast(colTrans.position, dir, out hit, 1.1f, layerMask))
                    {
                        return false;
                    }
                    else
                    {
                        colTrans.position = colTrans.position + dir;
                        return true;
                    }
                }
                else return false;
            }
        }
        else return true;
    }

    public bool VerifVictoire()
    {
        //pour chaque slot du level, vérifier si
        foreach (Transform child in levels[currentLevel].transform)
        {
            if (child.tag == "slot")
            {
                RaycastHit hit;
                if (Physics.Raycast(child.position + new Vector3(0.0f, 0.6f, 0.0f), Vector3.up, out hit, 1.0f))
                {
                    if (hit.collider.tag != "caisse") return false;
                }
                //s'il y a rien, alors un slot n'a pas de caisse donc faux
                else return false;
            }
        }

        return true;
    }
}
