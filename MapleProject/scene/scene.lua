require("class")

local M = class()

function M:init()
    self.state = {}
    self.selection = {}
end

function M:update(delta, time_since_start)
end

function M:refresh_selection()
    mp_selection = self.selection
    mp_reload_selection()
end

Scene = M