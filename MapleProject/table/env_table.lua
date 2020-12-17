-- NOTE: 每个env的默认值都是0，base是显示时候的基本值。实际值+base用来显示。实际值用来计算。这样的话实际值在0的左边或右边，方便定义出正作用和负作用
ENV_T = 1
ENV_W = 2
ENV_MF = 3
ENV_R = 4
ENV_O = 5
ENV_MG = 6

local T = {
    { id = ENV_T, name = "温度", base = 25,},
    { id = ENV_W, name = "湿度", base = 50,},
    { id = ENV_MF, name = "磁场", base = 0},
    { id = ENV_R, name = "射线", base = 10},
    { id = ENV_O, name = "氧气", base = 21,},
    { id = ENV_MG, name = "魔法", base = 0},
}

EnvTable = T