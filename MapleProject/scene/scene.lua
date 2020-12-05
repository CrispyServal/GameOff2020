require("class")

local M = class()

function M:init()
    self.state = {}
    self.selection = {}
end

function M:update(delta, time_since_start)
end

function M:refresh()
    self:refresh_selection()
    self:refresh_state()
end

function M:refresh_state()
    mp_state = self.state
end

function M:refresh_selection()
    mp_selection = self.selection
    mp_reload_selection()
end

require("scenes")
function M:switch_to(name)
    scenes.switch_to(name)
end

function M:call_scene_func(scene_name, func_name, ...)
    scenes.call_scene_func(scene_name, func_name, ...) 
end

Scene = M