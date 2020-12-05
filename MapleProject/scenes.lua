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
    end
end

scenes = M