var ModelsBaseObj_T6_Check_B1_ZongBiao = {	"Model" : {		"ID" : "",		"CID" : "",		"ZB1" : "",		"ZB2" : "",		"DW" : "",		"BYYS" : ""	},	"Fun_SetValue": function () {		Common_SetValue(this.Model);	},	"Fun_GetValue": function () {		this.Model = Common_GetValue(this.Model);	},	"Fun_Serialize": function () {		return Common_Serialize(this.Model);	},	"Fun_Serialize_All": function () {		return Common_Serialize_All(this.Model);	}}