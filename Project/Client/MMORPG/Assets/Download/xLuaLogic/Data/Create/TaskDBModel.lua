require "download/xLuaLogic/Data/Create/TaskEntity"

--数据访问
TaskDBModel = { }

local this = TaskDBModel;

local taskTable = { }; --定义表格

function TaskDBModel.New()
    return this;
end

function TaskDBModel.Init()

    --这里从C#代码中获取一个数组

    local gameDataTable = CS.LuaHelper.Instance:GetData("Task.data");
    --表格的前三行是表头 所以获取数据时候 要从 3 开始
    --print("行数"..gameDataTable.Row);
    --print("列数"..gameDataTable.Column);

    for i = 3, gameDataTable.Row - 1, 1 do
        taskTable[#taskTable+1] = TaskEntity.New( tonumber(gameDataTable.Data[i][0]), gameDataTable.Data[i][1], tonumber(gameDataTable.Data[i][2]), gameDataTable.Data[i][3] );
    end

end

function TaskDBModel.GetList()
    return taskTable;
end