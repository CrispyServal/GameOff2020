require("scene/scene")

local M = Scene:extend()

function M:init()
    Scene.init(self)
end

function M:start(hero_index)
    print("start")
    print(hero_index)
    self.state = {
        ch = hero_index,
    }
    self:init_heros(hero_index)
    self:refresh()
end

require("hero/hero")
function M:init_heros(hero_index)
    self.heros = {}
    self.state.heros = {}
    for i = 1, 4 do
        local hero = Hero:new(i, i ~= hero_index)
        table.insert(self.heros, hero)
        self.state.heros[i] = {}
    end
end

function M:update(delta, time_since_start)
    if not self.heros then
        return
    end
    for i, hero in ipairs(self.heros) do
        hero:update(delta)
        self.state.heros[i].name = hero:get_table_item().name
        self.state.heros[i].position = string.format("%.2f", hero.position)
    end
end

GameScene = M