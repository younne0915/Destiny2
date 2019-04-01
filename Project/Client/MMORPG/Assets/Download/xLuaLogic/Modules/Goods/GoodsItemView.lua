GoodsItemView = {};

local this = GoodsItemView;

local transform;
local gameObject;

function GoodsItemView.awake(obj)
	gameObject = obj;
	transform = obj.transform;
	
	this.InitView();
end

function GoodsItemView.InitView()
	--�ҵ�UI���
	this.lblGoodsName = transform:Find("lblGoodsName"):GetComponent("UnityEngine.UI.Text"); --��Ʒ����
	this.lblLevel = transform:Find("lblLevel"):GetComponent("UnityEngine.UI.Text"); --��Ʒʹ�õȼ�
	this.lblGoodsType = transform:Find("lblGoodsType"):GetComponent("UnityEngine.UI.Text"); --��Ʒ��������
	this.imgIcon = transform:Find("GoodsIcon/imgIcon"):GetComponent("UnityEngine.UI.Image"); --��Ʒͼ��
	
	this.lblUseMethod = transform:Find("imgUseMethod/lblUseMethod"):GetComponent("UnityEngine.UI.Text"); --ʹ�÷���
	this.lblDesc = transform:Find("imgDesc/lblDesc"):GetComponent("UnityEngine.UI.Text"); --����
	this.lblSell = transform:Find("lblSell"):GetComponent("UnityEngine.UI.Text"); --�ۼ�
	this.btnUse = transform:Find("btnUse"):GetComponent("UnityEngine.UI.Button"); --ʹ�ð�ť
	this.btnSell = transform:Find("btnSell"):GetComponent("UnityEngine.UI.Button"); --���۰�ť
end

function GoodsItemView.start()

end

function GoodsItemView.update()

end

function GoodsItemView.ondestroy()
	GoodsItemCtrl.OnDestroy();
end