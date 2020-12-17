-- singleton
local T = {
    { id = 1, name = "水母", desc = "一只可爱的水母，会用电", speed = 0.8,  },
    { id = 2, name = "魔女", desc = "从其他世界穿越而来的魔女", speed = 1.5, },
    { id = 3, name = "伟哥", desc = "普通的硕士研究生", speed = 1.0, },
    { id = 2, name = "石猴", desc = "几乎就是块石头，但是长得像猴", speed = 0.5, },
}

HERO_SHUIMU = 1
HERO_MONV = 2
HERO_WG = 3
HERO_STONE = 4

HeroTable = T