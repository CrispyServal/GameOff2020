require("class")
require("table/env_table")

local M = class()

function M:init()
    self.env = {}
    for _, env_table_item in ipairs(EnvTable) do
        self.env[env_table_item.id] = 0
    end

    self.show_env = {}
    self:update_show_env()
end

function M:update_show_env()
    for id, value in ipairs(self.env) do
        local env_table_item =EnvTable[id]
        self.show_env[env_table_item.name] = value + env_table_item.base
    end
end

Env = M
