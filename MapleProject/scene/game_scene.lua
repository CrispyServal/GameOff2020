require("scene/scene")

local M = Scene:extend()

function M:init()
    Scene.init(self)
end

function M:start(character_index)
    print("start")
    print(character_index)
    self.state = {
        ch = character_index,
    }
    self:refresh()
end

GameScene = M