var ModelsBaseObj_T1_User = {	"Model" : {		"ID" : "",		"Name" : "",		"LoginName" : "",		"Password" : "",		"OrgCode" : "",		"PRoleID" : "",		"RRoleCode" : "",		"DRoleType" : "",		"JobCode" : "",		"UserKey" : "",		"Del" : ""	},	"Fun_SetValue": function () {		Common_SetValue(this.Model);	},	"Fun_GetValue": function () {		this.Model = Common_GetValue(this.Model);	},	"Fun_Serialize": function () {		return Common_Serialize(this.Model);	},	"Fun_Serialize_All": function () {		return Common_Serialize_All(this.Model);	}}