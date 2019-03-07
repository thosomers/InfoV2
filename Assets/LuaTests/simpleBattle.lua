math.sign = function(v)
  return v / math.abs(v)
end


local robot = player.robots()[1]

local target = enemy.robots()[1]




while true do
  local delta = target.pos - robot.pos
  
  local mrange = robot.steps
  local arange = robot.attacks
  
  local targetAttackRange = target.steps or 0 + target.attacks or 0
  
  if (delta.length <= arange) then
    robot.attack(delta)
    break
  end
  
  if (mrange > 0) then
    local doMoveX = math.abs(delta.x) > math.abs(delta.y)
    local ret = nil
    if doMoveX then
      ret = robot.move(Vector(math.sign(delta.x),0))
    else
      ret = robot.move(Vector(0,math.sign(delta.y)))
    end
    if ret < 0 then
      break
    end
  else
    break
  end
end