require("scene/scene")

local M = Scene:extend()

function M:init()
    self:reset()
end

function M:reset()
    self.state = {
        "上帝赌马单机lua版",
        "请选择你喜欢的角色",
    }
    self.selection = self:make_selection()
    self:refresh()
end

function M:make_selection()
    require("table/character_table")
    local s = {}
    for i, table_item in ipairs(CharacterTable) do
        s[i] = {
            text = table_item.name,
            callback = function()
                self:on_select_character(i)
            end,
        }
    end
    return s
end

function M:on_select_character(index)
    print(index)
    self.selected_index = index
    self.state = {
        title = "您已选择一个英雄，确认开始游戏吗？",
        selected = {
            name = CharacterTable[index].name,
            desc = CharacterTable[index].desc,
        }
    }
    self.selection = {
        {
            text = "开始",
            callback = bind(self.start_game, self)
        },
        {
            text = "重新选择",
            callback = bind(self.reset, self)
        }
    }
    self:refresh()
end

function M:start_game()
    self:switch_to("game")
    self:call_scene_func("game", "start", self.selected_index)
end

function M:update(...)
    -- self.state.debug = mp_selection
end

MainScene = M