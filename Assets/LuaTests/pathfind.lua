function table.contains(tbl,val)
  for i,v in pairs(tbl) do
    if v == val then return true end
  end
  return false
end

function table.insertSort(tbl,val,func)
  
  local myscore = func(val)
  
  for i=1, #tbl do
    if (myscore < func(tbl[i])) then
      table.insert(tbl,i,val)
      return
    end
  end
  
  table.insert(tbl,val)
end


Directions = {}
Directions.N = Vector(0,1)
Directions.E = Vector(1,0)
Directions.S = -Directions.N
Directions.W = -Directions.E



local Point = {}

Point.dict = {}

Point.mt = {}
Point.mt.__eq = function(a,b)
  return a.x == b.x and a.y == b.y
end






function Point.fromTile(tile)
  
  if Point.dict[tile] then
    return Point.dict[tile]
  end
  
  
  
  local point = setmetatable({},Point.mt)
  point.x = tile.x
  point.y = tile.y
  point.tile = tile
  
  point.neighbors = function()
    local ns = {}
    for i,dir in pairs(Directions) do
      local ntile = Tiles[tile.pos+dir]
      if ntile and ntile.Walkable then
        table.insert(ns,Point.fromTile(ntile))
      end
    end
    return ns
  end
  
  point.score = function()
    if tile.Object then
      return 100
    else
      return 1
    end
  end
  
  Point.dict[tile] = point
  return point
end

function Point.fromXY(x,y)
  return Point.fromTile(Tiles[Vector(x,y)])
end

function Point.fromPos(vec)
  return Point.fromTile(Tiles[vec])
end

function reconstruct_path(cameFrom,current)
  local total_path = {current}
  while cameFrom[current] do
    current = cameFrom[current]
    table.insert(total_path,1,current)
  end
  return total_path
end

function heuristic_cost_estimate(a,b)
  return math.pow(math.abs(a.x - b.x)^2+math.abs(a.y - b.y)^2,0.5)
end


function A_Star(start,goal)
  local closedSet = setmetatable({},{__index=table})
  
  local openSet = setmetatable({start},{__index=table})
  
  local cameFrom = {}
  
  local gScore = setmetatable({},{__index = function(t,k) t[k] = 1000; return 1000 end})
  
  gScore[start] = 0
  
  fScore = setmetatable({},{__index = function(t,k) t[k] = 1000; return 1000 end})
  
  fScore[start] = heuristic_cost_estimate(start,goal)
  
  while #openSet > 0 do
    current = openSet[1]
    --print("Checking: "..tostring(current.x)..","..tostring(current.y))
    --current.tile.highlight = true
    
    if current == goal then
      return reconstruct_path(cameFrom,current)
    end
    
    openSet:remove(1)
    closedSet:insert(current)
    
    for _,neighbor in pairs(current.neighbors()) do
      if closedSet:contains(neighbor) then
        goto continue
      end
      
      tentative_gScore = gScore[current] + neighbor.score()
      
      if not openSet:contains(neighbor) then
        openSet:insertSort(neighbor,function(v) return fScore[v] end)
      elseif tentative_gScore >= gScore[neighbor] then
        goto continue
      end
      
      cameFrom[neighbor] = current
      gScore[neighbor] = tentative_gScore
      fScore[neighbor] = tentative_gScore + heuristic_cost_estimate(neighbor,goal)
      
      if neighbor == goal then
        return reconstruct_path(cameFrom,neighbor)
      end
      
      ::continue::
    end
  end
end
    
    
    
    
    


local b1 = player.Robots()[1]
local b2 = enemy.Base()

for i,v in pairs(A_Star(Point.fromPos(b1.pos),Point.fromPos(b2.pos))) do
  print(b1.steps)
  if b1.steps > 0 then
    b1.move(v.tile.pos - b1.pos)
  end  
  v.tile.highlight = true
end
    
  
  
  
  
  
  
