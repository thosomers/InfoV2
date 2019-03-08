local robot = player.Robots()[1]
local sPos = robot.tile
local target = enemy.Robots()[1] or enemy.base()
local ePos = target.tile

local path = Tiles.pathBetween(sPos,ePos)



function stepsNeeded(path,i)
  return #path - i
end

function shouldMove(path,i,steps,fullrange)
  if (stepsNeeded(path,i) <= steps) then
    return true
  end
  if stepsNeeded(path,i+1) <= fullrange then
    return false
  end
  return true
end

function shouldMove2()
  return true
end




function resumeHighlight(path,i)
  local nextTile = path[i]
  if nextTile == nil then return end
  
  nextTile.color = Color(0,0,1)
  
  if (i == #path) then
    nextTile.color = Color(1,0,0)
  end
  
  nextTile.highlight = true
  
  resumeHighlight(path,i+1)
end
  

function runNext(path,i)
  
  local nextTile = path[i]
  if nextTile == nil then return end
  
  if nextTile == ePos and robot.steps > 0 and robot.attacks > 0 then
    print("SUCCESS")
    
    robot.attack(nextTile.pos - robot.pos)
    
    nextTile.color = Color(1,0,0)
    nextTile.highlight = true
    return
  end
  
  if shouldMove2(path,i-1,robot.steps,target.range) then
    local ret = robot.move(nextTile.pos - robot.pos)
    if ret >= 0 then
      nextTile.color = Color(0,1,0)
      nextTile.highlight = true
      runNext(path,i+1)
      return
    end
  end
  
  resumeHighlight(path,i)
end

runNext(path,1)