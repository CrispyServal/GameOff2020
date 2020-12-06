require("class")

local M = class()

require("table/hero_table")
function M:init(index, is_ai)
    self.table_item = HeroTable[index]
    -- [0, 100]
    self.position = 0
    self.is_ai = is_ai
end

function M:update(delta)
    self.position = self.position + self.table_item.speed * delta
end

function M:get_table_item()
    return self.table_item
end

Hero = M