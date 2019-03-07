function makeAStar()
  local obj = {}

  ----------------------------------------------------------------
  -- local variables
  ----------------------------------------------------------------

  local INF = 1/0
  local cachedPaths = nil

  ----------------------------------------------------------------
  -- local functions
  ----------------------------------------------------------------

  function dist ( x1, y1, x2, y2 )
    
    return math.sqrt ( math.pow ( x2 - x1, 2 ) + math.pow ( y2 - y1, 2 ) )
  end

  function dist_between ( nodeA, nodeB )

    return dist ( nodeA.x, nodeA.y, nodeB.x, nodeB.y )
  end

  function heuristic_cost_estimate ( nodeA, nodeB )

    return dist ( nodeA.x, nodeA.y, nodeB.x, nodeB.y )
  end

  function is_valid_node ( node, neighbor )

    return true
  end

  function lowest_f_score ( set, f_score )

    local lowest, bestNode = INF, nil
    for _, node in ipairs ( set ) do
      local score = f_score [ node ]
      if score < lowest then
        lowest, bestNode = score, node
      end
    end
    return bestNode
  end

  function neighbor_nodes ( theNode, nodes )

    local neighbors = {}
    for _, node in ipairs ( nodes ) do
      if theNode ~= node and is_valid_node ( theNode, node ) then
        table.insert ( neighbors, node )
      end
    end
    return neighbors
  end

  function not_in ( set, theNode )

    for _, node in ipairs ( set ) do
      if node == theNode then return false end
    end
    return true
  end

  function remove_node ( set, theNode )

    for i, node in ipairs ( set ) do
      if node == theNode then 
        set [ i ] = set [ #set ]
        set [ #set ] = nil
        break
      end
    end	
  end

  function unwind_path ( flat_path, map, current_node )

    if map [ current_node ] then
      table.insert ( flat_path, 1, map [ current_node ] ) 
      return unwind_path ( flat_path, map, map [ current_node ] )
    else
      return flat_path
    end
  end

  ----------------------------------------------------------------
  -- pathfinding functions
  ----------------------------------------------------------------

  function a_star ( start, goal, nodes, valid_node_func )

    local closedset = {}
    local openset = { start }
    local came_from = {}

    if valid_node_func then is_valid_node = valid_node_func end

    local g_score, f_score = {}, {}
    g_score [ start ] = 0
    f_score [ start ] = g_score [ start ] + heuristic_cost_estimate ( start, goal )

    while #openset > 0 do
    
      local current = lowest_f_score ( openset, f_score )
      if current == goal then
        local path = unwind_path ( {}, came_from, goal )
        table.insert ( path, goal )
        return path
      end

      remove_node ( openset, current )		
      table.insert ( closedset, current )
      
      local neighbors = neighbor_nodes ( current, nodes )
      for _, neighbor in ipairs ( neighbors ) do 
        if not_in ( closedset, neighbor ) then
        
          local tentative_g_score = g_score [ current ] + dist_between ( current, neighbor )
           
          if not_in ( openset, neighbor ) or tentative_g_score < g_score [ neighbor ] then 
            came_from 	[ neighbor ] = current
            g_score 	[ neighbor ] = tentative_g_score
            f_score 	[ neighbor ] = g_score [ neighbor ] + heuristic_cost_estimate ( neighbor, goal )
            if not_in ( openset, neighbor ) then
              table.insert ( openset, neighbor )
            end
          end
        end
      end
    end
    return nil -- no valid path
  end

  ----------------------------------------------------------------
  -- exposed functions
  ----------------------------------------------------------------

  function clear_cached_paths ()

    cachedPaths = nil
  end
  obj.clear_cached_paths = clear_cached_paths

  function distance ( x1, y1, x2, y2 )
    
    return dist ( x1, y1, x2, y2 )
  end
  obj.distance = distance

  function path ( start, goal, nodes, ignore_cache, valid_node_func )

    if not cachedPaths then cachedPaths = {} end
    if not cachedPaths [ start ] then
      cachedPaths [ start ] = {}
    elseif cachedPaths [ start ] [ goal ] and not ignore_cache then
      return cachedPaths [ start ] [ goal ]
    end

        local resPath = a_star ( start, goal, nodes, valid_node_func )
        if not cachedPaths [ start ] [ goal ] and not ignore_cache then
                cachedPaths [ start ] [ goal ] = resPath
        end

    return resPath
  end
  obj.path = path

  return obj
end

if (_G.astar == nil) then
  _G.astar = makeAStar()
end


--MyCode

function findPath(startTile,endTile)
  
  local valid_node_func = function ( node, neighbor )
    -- helper function in the a-star module, returns distance between points
    if math.abs(neighbor.x - node.x) + math.abs(neighbor.y - node.y) <= 1 and node.walkable then
      if node.Object == nil or (node == startTile or neighbor == endTile) then
        return true
      end
    end
    return false
  end
  
  local nodes = Tiles.toTable()

  local path = astar.path( startTile, endTile, nodes, true, valid_node_func )
  
  return path
end




local robot = player.Robots()[1]
local sPos = robot.tile
local ePos = enemy.Base().tile

local path = Tiles.pathBetween(sPos,ePos)
print(path)


for i,v in pairs(path) do
  print(robot.steps)
  if robot.steps > 0 then
    robot.move(v.pos - robot.pos)
  end  
  v.highlight = true
end
    
  
  
  
  
  
  
