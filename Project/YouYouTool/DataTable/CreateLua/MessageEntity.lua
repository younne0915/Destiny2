MessageEntity = { Id = 0, Msg = "", Module = "", Description = "" }

--这句是重定义元表的索引，就是说有了这句，这个才是一个类
MessageEntity.__index = MessageEntity;

function MessageEntity.New(Id, Msg, Module, Description)
    local self = { }; --初始化self
    setmetatable(self, MessageEntity); --将self的元表设定为Class

    self.Id = Id;
    self.Msg = Msg;
    self.Module = Module;
    self.Description = Description;

    return self;
end