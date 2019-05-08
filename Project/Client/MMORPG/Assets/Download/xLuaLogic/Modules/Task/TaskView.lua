TaskView = {};

local this = TaskView;

local transform;
local gameObject;

function TaskView.awake(obj)
	gameObject = obj;
	transform = obj.transform;
	this.InitView();
end

function TaskView.start()
	--TaskCtrl.Start()
end

function TaskView.update()
end

function TaskView.ondestroy()
end

function TaskView.InitView()
	this.content = transform:Find("Scroll View/Viewport/Content");
	this.detailContainer = transform:Find("detailContainer");
	this.detailContainer.gameObject:SetActive(false);
	
	this.txtTaskName = transform:Find("detailContainer/txtTaskName"):GetComponent("UnityEngine.UI.Text");
	this.txtTaskId = transform:Find("detailContainer/txtTaskId"):GetComponent("UnityEngine.UI.Text");
	this.txtTaskStatus = transform:Find("detailContainer/txtTaskStatus"):GetComponent("UnityEngine.UI.Text");
	this.txtTaskContent = transform:Find("detailContainer/txtTaskContent"):GetComponent("UnityEngine.UI.Text");

end