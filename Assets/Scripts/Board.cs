using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public Tilemap tilemap { get; private set; }

    // controls tetronimoes
    public List<Transform> _blocks;
    public Transform blockPrefab;

    private void Awake()
    {
        this.tilemap = GetComponentInChildren<Tilemap>();

        // loop through list of data and initialize data
        // for (int i = 0; i < this.tetronimoes.length; i++) {
        //     this.tetrominoes[i].Initialize();
        // }
    }

    public void Set()
    {
        
    }
}