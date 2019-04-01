require "Download/XLuaLogic/Data/Create/MessageEntity"

--数据访问
MessageDBModel = { }

local this = MessageDBModel;

local messageTable = { }; --定义表格

function MessageDBModel.New()
    return this;
end

function MessageDBModel.Init()

    --这里从C#代码中获取一个数组

    local gameDataTable = CS.LuaHelper.Instance:GetData("Message.data");
    --表格的前三行是表头 所以获取数据时候 要从 3 开始
    --print("行数"..gameDataTable.Row);
    --print("列数"..gameDataTable.Column);

    for i = 3, gameDataTable.Row - 1, 1 do
        messageTable[#messageTable+1] = MessageEntity.New( tonumber(gameDataTable.Data[i][0]), gameDataTable.Data[i][1], gameDataTable.Data[i][2], gameDataTable.Data[i][3] );
    end

end

function MessageDBModel.GetList()
    return messageTable;
end

function MessageDBModel.GetEntity(id)
    local ret = nil;
    for i = 1, #messageTable, 1 do
        if (messageTable[i].Id == id) then
            ret = messageTable[i];
            break;
        end
    end
    return ret;
end