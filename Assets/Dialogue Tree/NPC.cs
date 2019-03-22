﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    Node _currentNode;
    public UnityEngine.UI.Text outText;
    private List<Node> _nodes;
    
    public void Say(Node node)
    {
        _currentNode = node;
        outText.text = node.Line;
    }
    
    public void Say( )
    {
        outText.text = _currentNode.Line;
    }

    // Start is called before the first frame update
    void Start()
    {
        _nodes = Node.GetTree();
        _currentNode = _nodes[0];
        var nodeRoot = Solution1(_nodes[0], 10);

        Say(nodeRoot);

        var traces = Solution2(nodeRoot);
        Debug.Log(traces);
        Debug.Log(traces.Count);
    }
}
