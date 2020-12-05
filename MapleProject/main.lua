mp_state = {
}


mp_selection = {}

global = {}

function update(delta, time_since_start)
end

require("scenes")
require("scene/main_scene")
require("scene/game_scene")
function awake()
    scenes.add("main", MainScene:new())
    scenes.add("game", GameScene:new())
    scenes.switch_to("main")
end