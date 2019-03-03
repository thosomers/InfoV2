N = Vector(0,1)
E = Vector(1,0)
S = -N
W = -E

Directions = {N,E,S,W}

function clearPaths()
  for i,tile in pairs(Tiles.enabled) do
    tile.highlight = false
  end
end
    

function colorPaths(robot)
  
  local Store = setmetatable({},{__index = function(t,k) t[k] = {moveLeft = -2, attackLeft = -2} return t[k] end})
  
  function MoveToPos(vec,moveLeft,attackLeft)
    print("MoveToPos: "..tostring(vec).." "..tostring(moveLeft).." "..tostring(attackLeft))
    
    if attackLeft < 0 then return end
    
    local tile = Tiles[vec]
    if tile == nil then return end
    
    local info = Store[vec]
    
    if info.moveLeft >= moveLeft then return end
    
    if info.attackLeft >= attackLeft then return end
    
    local mustAttack = (tile.Object != nil and tile.Object != robot) or moveLeft < 0
    
    if mustAttack then
      info.moveLeft = -1
      attackLeft = attackLeft - 1
      info.attackLeft = attackLeft
    else
      info.moveLeft = moveLeft
      info.attackLeft = attackLeft
    end
    
    for _,dir in pairs(Directions) do
      local nvec = vec + dir
      if mustAttack then
        MoveToPos(nvec,-1,attackLeft - 1)
      else
        MoveToPos(nvec,moveLeft - 1, attackLeft)
      end
    end
  end
  
  MoveToPos(robot.pos,robot.steps,robot.attacks)
  
  for vec,info in pairs(Store) do
    local tile = Tiles[vec]
    
    if info.moveLeft >= 0 then
      tile.highlight = true
      tile.color = Color(0,0,1)
    elseif info.attackLeft >= 0 then
      tile.highlight = true
      tile.color = Color(1,0,0)
    end
  end
end

clearPaths()
colorPaths(player.robots()[1])

