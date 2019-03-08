﻿/**
* Provide simple path-finding algorithm with support in penalties.
* Heavily based on code from this tutorial: https://www.youtube.com/watch?v=mZfyt03LDH4
* This is just a Unity port of the code from the tutorial + option to set penalty + nicer API.
*
* Original Code author: Sebastian Lague.
* Modifications & API by: Ronen Ness.
* Since: 2016.
*/

using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace PathFind
{
    /**
    * Main class to find the best path from A to B.
    * Use like this:
    * Grid grid = new Grid(width, height, tiles_costs);
    * List<Point> path = Pathfinding.FindPath(grid, from, to);
    */
    public class Pathfinding
    {
        // The API you should use to get path
        // grid: grid to search in.
        // startPos: starting position.
        // targetPos: ending position.
        public static List<Point> FindPath(Grid grid, Point startPos, Point targetPos)
        {
            // find path
            List<Node> nodes_path = _ImpFindPath(grid, startPos, targetPos);

            // convert to a list of points and return
            List<Point> ret = new List<Point>();
            if (nodes_path != null)
            {
                foreach (Node node in nodes_path)
                {
                    ret.Add(new Point(node.GridX, node.GridY));
                }
            }
            return ret;
        }

        // internal function to find path, don't use this one from outside
        private static List<Node> _ImpFindPath(Grid grid, Point startPos, Point targetPos)
        {
            Node startNode = grid.nodes[startPos.x, startPos.y];
            Node targetNode = grid.nodes[targetPos.x, targetPos.y];

            List<Node> openSet = new List<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet[0];
                for (int i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                    {
                        currentNode = openSet[i];
                    }
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    return RetracePath(grid, startNode, targetNode);
                }

                foreach (Node neighbour in grid.GetNeighbours(currentNode))
                {
                    if (neighbour == targetNode)
                    {
                        neighbour.parent = currentNode;
                        return RetracePath(grid, startNode, targetNode);
                    }
                    
                    if (!neighbour.Walkable() || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour) * (int)(10.0f * neighbour.Penalty());
                    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }
            }

            return null;
        }

        private static List<Node> RetracePath(Grid grid, Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();
            Node currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }
            path.Reverse();
            return path;
        }

        private static int GetDistance(Node nodeA, Node nodeB)
        {
            int dstX = Mathf.Abs(nodeA.GridX - nodeB.GridX);
            int dstY = Mathf.Abs(nodeA.GridY - nodeB.GridY);

            if (dstX > dstY)
                return 14 * dstY + 10 * (dstX - dstY);
            return 14 * dstX + 10 * (dstY - dstX);
        }



























        public static List<Tile> withDistance(Tile begin, int range)
        {
            HashSet<Tile> ClosedSet = new HashSet<Tile>();
    
            Queue<Tile> OpenSet = new Queue<Tile>();
            
            OpenSet.Enqueue(begin);

            for (int i = 0; i < range; i++)
            {
                Queue<Tile> newSet = new Queue<Tile>();

                while (OpenSet.Count > 0)
                {
                    Tile next = OpenSet.Dequeue();
                    ClosedSet.Add(next);

                    for (int x = -1; x <= 1; x++)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            if (Math.Abs(x) + Math.Abs(y) != 1) continue;
                            
                            var neighbor = Board.getTile(next.X + x, next.Y + y);

                            if (neighbor == null ||  ClosedSet.Contains(neighbor) || OpenSet.Contains(neighbor) || newSet.Contains(neighbor))
                            {
                                continue;
                            }

                            if (neighbor.Walkable && neighbor.Object == null)
                            {
                                newSet.Enqueue(neighbor);
                            }
                        }
                    }
                }

                OpenSet = newSet;

            }

            return OpenSet.ToList();
        }
        
        
        
        
        
        
    }

}