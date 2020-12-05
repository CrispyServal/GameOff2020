require("scene/scene")

local M = Scene:extend()

local EUiState = {
    SelectCharacter = 1,
    SelectAiCharacter = 2,
    WaitForEnter = 3,
}

function M:init()
    self.ui_state = EUiState.SelectCharacter

    self.state = {
        "上帝赌马单机lua版",
        "请选择你喜欢的角色",
    }
    self.selection = self:make_selection()
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
    self.ui_state = EUiState.SelectCharacter
    self.state.selected = {
        name = CharacterTable[index].name,
        desc = CharacterTable[index].desc,
    }
    self.selection = self:make_ai_selection()
    self:refresh_selection()
end

function M:make_ai_selection()
    local s = {}
    return s
end

function M:update(...)
    -- self.state.debug = mp_selection
end

MainScene = M