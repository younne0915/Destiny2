require "Download/xLuaLogic/Data/Create/TaskEntity"
require "Download/xLuaLogic/Data/Create/TaskDBModel"

DBModelMgr = {}

local this = DBModelMgr
local dbModelList = {}
local isInit = false

function DBModelMgr.Init()
	dbModelList[DBModelNames.TaskDBModel] = TaskDBModel.New();
	dbModelList[DBModelNames.TaskDBModel].Init()
	
	return this
end

function DBModelMgr.GetDBModel(dbModelName)
	if (isInit == false) then
		this.Init()
		isInit = true
	end
	return dbModelList[dbModelName]
end