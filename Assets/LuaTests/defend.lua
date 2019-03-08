





function distance(obj1,obj2)
  local path = Tiles.pathBetween(obj1.tile,obj2.tile)
  --print("dis:"..tostring(#path))
  if path then return #path end
  return 1000
end

function getClosestTarget(robot)
  local distances = {}
  distances[0] = 999
  
  local baseMultiplier = 2
  
  for _,t in pairs(enemy.robots()) do
    distances[t] = distance(robot,t)
  end
  
  distances[enemy.base()] = baseMultiplier * distance(robot,enemy.base())
  
  local target = 0
  
  for t,d in pairs(distances) do
    if d < distances[target] then
      target = t
    end
  end
  
  if target == 0 then
    return nil
  else
    return target
  end
end

function makeClaimable()
  local tile = player.base().tile
  local cur = {}
  local i = 3
  while #cur < 10 do
    --print(i)
    for _,t in pairs(Tiles.withRange(tile,i)) do
      cur[#cur+1] = t
    end
    i = i + 1
  end
  return cur
end

ClaimableTiles = makeClaimable()

function robotWrapper(robot)
  local wrapper = setmetatable({robot = robot},{__index = robot})
  
  local i = #ClaimableTiles
  wrapper.claimed = ClaimableTiles[i]
  ClaimableTiles[i] = nil
  
  
  function wrapper.moveToClaimed()    
    
    --print("To claim")
    local path = Tiles.pathBetween(robot.tile,wrapper.claimed)
    
    if debug then
      for _,v in pairs(path) do
        v.highlight = true
        v.color = Color(1,1,1)
      end
    end
    
    
    
    if path == nil then return end
    
    for _,v in pairs(path) do
      if robot.steps <= 0 then
        return
      end
      robot.move(v.pos - robot.pos)
    end
  end
  
  function wrapper.attackTarget(target)
    local path = Tiles.pathBetween(robot.tile,target.tile)
    if path == nil then return end
    
    for _,v in pairs(path) do
      if robot.steps <= 0 then
        return
      end
      robot.move(v.pos - robot.pos)
    end
    
    robot.attack(target.pos - robot.pos)
  end
  
  function wrapper.closestTarget()
    return getClosestTarget(robot)
  end
  
  function wrapper.distance(obj)
    return distance(robot,obj)
  end
  
  function wrapper.path(tile)
    return Tiles.pathBetween(robot.tile,tile)
  end
  
  return wrapper
end

RobotInfo = setmetatable({},{__index = function(t,k) t[k] = robotWrapper(k); return t[k] end })













function run()
  
  for i,v in pairs(ClaimableTiles) do
    v.highlight = true
  end
  
  
  for i,rob in pairs(player.robots()) do
    local robot = RobotInfo[rob]
    
    --print("Robot: "..tostring(i))
   
    local closest = robot.closestTarget()
    
    --print("Closest: "..tostring(closest))
    --print("Distance: "..robot.distance(closest))
    
    if debug then
      closest.tile.color = Color(1,1,0)
      closest.tile.highlight = true
      robot.tile.color = Color(0,1,1)
      robot.tile.highlight = true
    end
    
    
    
    
    if closest != nil and robot.distance(closest) <= robot.range then
      for _,v in pairs(robot.path(closest.tile)) do
        if v == closest.tile then
          robot.attack(closest.pos - robot.pos)
        else
          robot.move(v.pos - robot.pos)
        end
      end
    else
      --print("ToClaimed")
      robot.moveToClaimed()
    end
    
    robot.claimed.highlight = true
    robot.claimed.color = Color(0,1,0)
    
    
  end

end














