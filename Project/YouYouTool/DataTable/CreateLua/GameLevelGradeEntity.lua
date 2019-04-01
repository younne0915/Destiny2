GameLevelGradeEntity = { Id = 0, GameLevelId = 0, Grade = 0, Desc = "", Type = 0, Parameter = "", ConditionDesc = "", Exp = 0, Gold = 0, CommendFighting = 0, TimeLimit = 0, Star1 = 0, Star2 = 0, Equip = "", Item = "", Material = "" }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
GameLevelGradeEntity.__index = GameLevelGradeEntity;

function GameLevelGradeEntity.New(Id, GameLevelId, Grade, Desc, Type, Parameter, ConditionDesc, Exp, Gold, CommendFighting, TimeLimit, Star1, Star2, Equip, Item, Material)
    local self = { }; --初始化self
    setmetatable(self, GameLevelGradeEntity); --将self的元表设定为Class

    self.Id = Id;
    self.GameLevelId = GameLevelId;
    self.Grade = Grade;
    self.Desc = Desc;
    self.Type = Type;
    self.Parameter = Parameter;
    self.ConditionDesc = ConditionDesc;
    self.Exp = Exp;
    self.Gold = Gold;
    self.CommendFighting = CommendFighting;
    self.TimeLimit = TimeLimit;
    self.Star1 = Star1;
    self.Star2 = Star2;
    self.Equip = Equip;
    self.Item = Item;
    self.Material = Material;

    return self;
end