using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TileSwap : MonoBehaviour {

    RaycastHit2D hit1, hit2, hit3, hit4;
    Ray2D leftHit, rightHit, upHit, downHit;

    public Sprite[] tileSprites;
    public SpriteRenderer currentSprite;

    public float tileDistance;
    public int up, down, left, right, tilePosistion, id = 0;
    public GameObject upObj, downObj, leftObj, rightObj;

    public List<GameObject> lista = new List<GameObject>();

    public LayerMask tiles;

    void Start()
    {
        curSprite = GetComponent<SpriteRenderer>();
        UpdateTile();
    }

    /// <summary>
    /// Atualiza a sprite do GO a partir de uma operação bitwise utilizando raycast para verificar 
    /// colisão e adicionar na lista de GO
    /// </summary>
    public void UpdateTile()
    {
        CheckTheList();

        downHit = new Ray2D(downObj.transform.position, -Vector2.up);
        upHit = new Ray2D(upObj.transform.position, Vector2.up);
        rightHit = new Ray2D(rightObj.transform.position, Vector2.right);
        leftHit = new Ray2D(leftObj.transform.position, -Vector2.right);

        hit1 = Physics2D.Raycast(upHit.origin, upHit.direction, tileDistance, tiles);
        hit2 = Physics2D.Raycast(downHit.origin, downHit.direction, tileDistance, tiles);
        hit3 = Physics2D.Raycast(rightHit.origin, rightHit.direction, tileDistance, tiles);
        hit4 = Physics2D.Raycast(leftHit.origin, leftHit.direction, tileDistance, tiles);

        if (hit1.collider == true)
        {
            if (hit1.collider.gameObject.name == "Dirt")
            {
                up = 1;
                if (id == 0)
                {
                    lista.Add(hit1.collider.gameObject);
                }
            }
        }
        else
        {
            up = 0;
        }
        if (hit2.collider == true)
        {
            if (hit2.collider.gameObject.name == "Dirt")
            {
                down = 1;
                if (id == 0)
                {
                    lista.Add(hit2.collider.gameObject);
                }
            }
        }
        else
        {
            down = 0;
        }
        if (hit3.collider == true)
        {
            if (hit3.collider.gameObject.name == "Dirt")
            {
                right = 1;
                if (id == 0)
                {
                    lista.Add(hit3.collider.gameObject);
                }
            }
        }
        else
        {
            right = 0;
        }
        if (hit4.collider == true)
        {
            if (hit4.collider.gameObject.name == "Dirt")
            {
                left = 1;
                if (id == 0)
                {
                    lista.Add(hit4.collider.gameObject);
                }
            }
        }
        else
        {
            left = 0;
        }
        tilePosistion = (1 * up) + (2 * left) + (4 * right) + (8 * down);
        currentSprite.sprite = tileSprites[tilePosistion];
        id++;
    }

    /// <summary>
    /// Verifica se possui algum vazio na lista e remove
    /// </summary>
    public void CheckTheList()
    {
        for (int i = 0; i < lista.Count; i++)
        {
            if (lista[i] == null)
            {
                lista.Remove(lista[i]);
            }
        }
    }

    /// <summary>
    /// Quando o atual GO for destruído, o método UpdateTile das tiles que estão ao redor será chamado.
    /// </summary>
    public void OnDestroy()
    {
        CheckTheList();
        if (lista.Count>0)
        {
            for (int i = 0; i < lista.Count; i++)
            {
                if(lista[i].GetComponent<BitTile>())
                {
                    lista[i].GetComponent<BitTile>().UpdateTile();
                }
                else
                {
                    return;
                }
            }
        }
    }
}
