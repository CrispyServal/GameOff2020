mp_state = {
}


mp_selection = {}

function update(delta, time_since_start)
end

require("scenes")
require("scene/main_scene")
function awake()
    scenes.add("main", MainScene:new())
    scenes.switch_to("main")
end