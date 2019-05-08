UIRootView = {};

local this = UIRootView;

local transform;
local gameObject;

function UIRootView.awake(obj)
	gameObject = obj;
	transform = obj.transform;
	
	this.btnOpenTask = transform:Find("Canvas/ContainerBottomRight/btnOpenTask"):GetComponent("UnityEngine.UI.Button");
	print('UIRootView : awake');
end

function UIRootView.start()
end

function UIRootView.update()
end

function UIRootView.ondestroy()
end

function UIRootView.InitView()
end