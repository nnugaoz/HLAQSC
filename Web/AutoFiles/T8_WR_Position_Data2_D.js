var ModelsBaseObj_T8_WR_Position_Data2_D = {	"Model" : {		"ID" : "",		"WRID" : "",		"PositionCode" : "",		"EquipmentID" : "",		"FKey" : "",		"FType" : "",		"Fvalue0" : "",		"FUnit0" : "",		"FValue1" : "",		"FUnit1" : ""	},	"Fun_SetValue": function () {		Common_SetValue(this.Model);	},	"Fun_GetValue": function () {		this.Model = Common_GetValue(this.Model);	},	"Fun_Serialize": function () {		return Common_Serialize(this.Model);	},	"Fun_Serialize_All": function () {		return Common_Serialize_All(this.Model);	}}