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









function run()
    for _,r in pairs(player.robots()) do
        local target = getClosestTarget(r)
    
    
        local path = nil
        if target == nil then goto continue end
        path = Tiles.pathBetween(r.tile,target.tile)
        for _,v in pairs(path) do
            if v == target.tile then
                r.attack(v.pos - r.pos)
            elseif r.steps > 0 then
                r.move(v.pos-r.pos)
            end
        end
        ::continue::
    end
end
