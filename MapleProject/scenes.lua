require("utils")
-- 单例，场景管理器
local M = {
    -- name -> scene
    scenes = {}
}

function M.add(name, scene)
    M.scenes[name] = scene
end

function M.switch_to(name)
    local scene = M.scenes[name]
    if scene then
        mp_state = scene.state
        mp_selection = scene.selection
        update = bind(scene.update, scene)
        mp_reload_selection()
    else
        print("no scene named "..tostring(name))
    end
end

function M.call_scene_func(scene_name, func_name, ...)
    local scene = M.scenes[scene_name]
    if scene then
        scene[func_name](scene, ...)
    else
        print("no scene named "..tostring(scene_name))
    end
end

scenes = M