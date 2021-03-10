using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum codeAction
{
    HAUT,
    BAS,
    GAUCHE,
    DROITE
}

public struct Etat
{
    public int x, y;
    public float value;
    public codeAction strategie;

    public Etat(int x, int y, bool arrivee)
    {
        this.x = x;
        this.y = y;
        this.strategie = codeAction.HAUT;

        if (arrivee) value = 1000;
        else value = 0;
    }

    public Etat(int x, int y, bool arrivee, codeAction uneStrategie)
    {
        this.x = x;
        this.y = y;
        this.strategie = uneStrategie;

        if (arrivee) value = 1000;
        else value = 0;
    }
}




public class ValueIterationScript : MonoBehaviour
{
    [SerializeField] private int largeurGrille;
    [SerializeField] private int longueurGrille;

    [SerializeField] private int largeurArrivee;
    [SerializeField] private int longueurArrivee;

    [SerializeField] private float rewardDeplacement;
    [SerializeField] private float devaluation;

    public Etat[,] listeEtat;
    public List<Etat> etatsPieges = new List<Etat>();
    [SerializeField] private List<Vector2> coordonneePiege = new List<Vector2>();


    // Start is called before the first frame update
    public void Initialisation()
    {
        listeEtat = new Etat[largeurGrille, longueurGrille];

        for(int i = 0; i < longueurGrille; i++)
        {
            for(int j = 0; j < largeurGrille; j++)
            {
                if(i == largeurGrille-1 && j == longueurGrille-1)
                {
                    listeEtat[i, j] = new Etat(j, i, true);
                }
                else listeEtat[i, j] = new Etat(j, i, false);
            }
        }

        foreach (Vector2 coordonnee in coordonneePiege)
        {
            listeEtat[(int)coordonnee.x, (int)coordonnee.y].value = -1500;
            etatsPieges.Add(listeEtat[(int)coordonnee.x, (int)coordonnee.y]);
        }
    }

    //avoir la liste des actions possibles sur un etat donné
    public List<codeAction> getPossibleActions(Etat e)
    {
        List<codeAction> l = new List<codeAction>();

        if (e.x == largeurArrivee && e.y == longueurArrivee) return l;

        if (e.x != 0) l.Add(codeAction.GAUCHE);
        if (e.x < largeurGrille - 1) l.Add(codeAction.DROITE);
        if (e.y != 0) l.Add(codeAction.BAS);
        if (e.y < longueurGrille - 1) l.Add(codeAction.HAUT);

        return l;
    }

    public void ValueIteration()
    {
        float delta = 2.0f;
        float theta = 0.001f;

        while(delta > theta)
        {
            delta = 0.0f;

            for (int i = 0; i < longueurGrille; i++)
            {
                for (int j = 0; j < largeurGrille; j++)
                {
                    if (!(i == longueurGrille - 1 && j == largeurGrille - 1) && !(etatsPieges.Contains(listeEtat[i, j])) )
                    {

                        var actions = getPossibleActions(listeEtat[i, j]);
                        float[] scoresAction = new float[actions.Count];

                        var temp = listeEtat[i, j].value;

                        for (int z = 0; z < actions.Count; z++)
                        {
                            scoresAction[z] = GetStateValue(actions[z], listeEtat[i, j]);
                        }


                        listeEtat[i, j].value = Mathf.Max(scoresAction);


                        //Debug.Log("Valeur mise à jour sur la tuile [" + i + "," + j + "] : " + listeEtat[i, j].value);

                        delta = Mathf.Max(delta, Mathf.Abs(temp - listeEtat[i, j].value));
                    }
                }
            }
        }

        //affichage des résultats
        //DisplayResult();
    }

    public void policyEvaluation()
    {
        float delta = 2.0f;
        float theta = 0.001f;

        while (delta > theta)
        {
            delta = 0.0f;

            for (int i = 0; i < longueurGrille; i++)
            {
                for (int j = 0; j < largeurGrille; j++)
                {

                    var temp = listeEtat[i, j].value;

                    listeEtat[i, j].value = GetStateValue(listeEtat[i, j].strategie, listeEtat[i, j]);

                    delta = Mathf.Max(delta, Mathf.Abs(temp - listeEtat[i, j].value));
                }
            }
        }

        //affichage des résultats
        DisplayResult();
    }

    public Etat getEtatFromPos(int x, int y)
    {

        for (int i = 0; i < longueurGrille; i++)
        {
            for (int j = 0; j < largeurGrille; j++)
            {

                if (listeEtat[i,j].x == x && listeEtat[i, j].y == y)
                {
                    //Debug.Log("l'état est bien trouvé x : " + x + " y : " + y);
                    return listeEtat[i, j];
                }

            }
        }

        //Debug.Log("l'état n'a pas été trouvé x : " + x + " y : " + y);

        return listeEtat[0, 0];

    }

    public bool PolicyImprovement()
    {
        bool policyStable = true;

        for (int i = 0; i < longueurGrille; i++)
        {
            for (int j = 0; j < largeurGrille; j++)
            {

                codeAction temp = listeEtat[i, j].strategie;

                var actions = getPossibleActions(listeEtat[i, j]);
                List<float> scoresAction = new List<float>();

                for (int z = 0; z < actions.Count; z++)
                {
                    scoresAction[z] = GetStateValue(actions[z], listeEtat[i, j]);
                }

                float maxValue = Mathf.Max(scoresAction.ToArray()) ;

                listeEtat[i, j].strategie = actions[scoresAction.IndexOf(maxValue)];

                if (temp != listeEtat[i, j].strategie)
                {
                    policyStable = false;
                }

            }
        }

        return policyStable;

    }

    public void DisplayResult()
    {
        for(int i = 0; i < longueurGrille; i++)
        {
            string debug = "";

            for(int j = 0; j < largeurGrille; j++)
            {
                debug += (listeEtat[i, j].value + " ");
            }

            Debug.Log(debug);
        }
    }

    public float GetStateValue(codeAction a, Etat e)
    {
        

        switch(a)
        {
            case codeAction.BAS:

                return rewardDeplacement + devaluation*listeEtat[e.x, e.y - 1].value;

            case codeAction.DROITE:

                return rewardDeplacement + devaluation*listeEtat[e.x + 1,e.y].value;

            case codeAction.GAUCHE:

                return rewardDeplacement + devaluation*listeEtat[e.x - 1, e.y].value;

            case codeAction.HAUT:

                return rewardDeplacement + devaluation*listeEtat[e.x, e.y + 1].value;

            default:
                return 0;
        }
    }
}
