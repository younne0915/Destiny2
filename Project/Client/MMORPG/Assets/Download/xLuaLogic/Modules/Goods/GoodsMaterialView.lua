GoodsMaterialView = {};

local this = GoodsMaterialView;

local transform;
local gameObject;

function GoodsMaterialView.awake(obj)
	gameObject = obj;
	transform = obj.transform;
	
	this.InitView();
end

function GoodsMaterialView.InitView()
	--�ҵ�UI���
	this.lblGoodsName = transform:Find("lblGoodsName"):GetComponent("UnityEngine.UI.Text"); --��Ʒ����
	this.lblLevel = transform:Find("lblLevel"):GetComponent("UnityEngine.UI.Text"); --��Ʒʹ�õȼ�
	this.lblGoodsType = transform:Find("lblGoodsType"):GetComponent("UnityEngine.UI.Text"); --��Ʒ��������
	this.imgIcon = transform:Find("GoodsIcon/imgIcon"):GetComponent("UnityEngine.UI.Image"); --��Ʒͼ��
	
	this.lblDesc = transform:Find("imgDesc/lblDesc"):GetComponent("UnityEngine.UI.Text"); --����
	this.lblSell = transform:Find("lblSell"):GetComponent("UnityEngine.UI.Text"); --�ۼ�
	this.btnSell = transform:Find("btnSell"):GetComponent("UnityEngine.UI.Button"); --���۰�ť
end

function GoodsMaterialView.start()

end

function GoodsMaterialView.update()

end

function GoodsMaterialView.ondestroy()
	GoodsMaterialCtrl.OnDestroy();
end