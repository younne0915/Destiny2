require "Download/xLuaLogic/Data/DBModelMgr"
require "Download/xLuaLogic/Proto/Task_SearchTaskProto"
require "Download/xLuaLogic/Proto/Task_SearchTaskReturnProto"

TaskCtrl = {};

local this = TaskCtrl;

local root;
local transform;
local gameObject;



function TaskCtrl.New()
	return this;
end

function TaskCtrl.Awake()
	print('任务 启动了');
	--CS.LuaHelper.ViewUtil:LoadUIRoot(this.OnCreate);
    CS.LuaHelper.Instance.uiViewUtil:LoadWindowForLua("TaskView", this.OnCreate, "Download/Prefab/xLuaUIPrefab/pan_TaskView");
end

function TaskCtrl.Start()
	print('任务 Start');
end

function TaskCtrl.OnCreate(obj)
	
	CS.LuaHelper.Instance:AddListener(15002, this.OnTaskSearchReturnCallback);
	
	local proto = Task_SearchTaskProto.New()
	Task_SearchTaskProto.SendProto(proto)
	print("给服务器发协议")
	
end

function TaskCtrl.OnTaskSearchReturnCallback(buffer)
	print("OnTaskSearchReturnCallback")
	CS.LuaHelper.Instance.loaderMgr:LoadOrDownloadForLua("Download/Prefab/xLuaUIPrefab/TaskItemView", "TaskItemView", 
		function (obj)
			
			local proto = Task_SearchTaskReturnProto.GetProto(buffer)
			print("TaskCount = "..proto.TaskCount)
			for i=1,proto.TaskCount,1 do
			local item = CS.UnityEngine.Object.Instantiate(obj);
			item.transform.parent = TaskView.content;
			item.transform.localPosition = Vector3.zero;
			item.transform.localScale = Vector3.one;
		
			local task = proto.CurrTaskItemTable[i]
			local txtTitle = item.transform:Find("txtTitle"):GetComponent("UnityEngine.UI.Text");
			local txtStatus = item.transform:Find("txtStatus"):GetComponent("UnityEngine.UI.Text");
			txtTitle.text = task.Name
			txtStatus.text = this.GetTaskStatusName(task.Status)
		
			item.transform:GetComponent("UnityEngine.UI.Button").onClick:AddListener
			(
				function ()
					TaskView.detailContainer.gameObject:SetActive(true);
					TaskView.txtTaskName.text = task.Name;
					TaskView.txtTaskId.text = tostring(task.Id);
					TaskView.txtTaskStatus.text = this.GetTaskStatusName(task.Status);
					TaskView.txtTaskContent:DOText(task.Content, 0.5)
				end
			);
			end
		end
	);
end

function TaskCtrl.GetTaskStatusName(status)
	if (status == 0) then
		return "<color=#06C300FF>未接</color>"
	elseif(status == 1)then
		return "<color=#00CAFFFF>已接</color>"
	elseif(status == 2)then
		return "<color=#FFAE00FF>已完成</color>"
	end
end