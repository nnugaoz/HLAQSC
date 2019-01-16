var CurrPage = CommonFun_QueryString("CurrPage");
// ============================================================
// 路径跳转
// begin
// ============================================================
function CommonFun_Href(url) {
    var index = url.indexOf("?");
    if (index >= 0) {
        url += "&r=" + Math.random();
    } else {
        url += "?r=" + Math.random();
    }
    window.location.href = url;
}
// ============================================================
// end
// 路径跳转
// ============================================================

// ============================================================
// 获取Url参数
// begin
// ============================================================
function CommonFun_QueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) {
        return unescape(r[2]);
    }
    return "";
}
// ============================================================
// end
// 获取Url参数
// ============================================================

// ============================================================
// 替换特殊字符
// begin
// ============================================================
function CommonFun_ReplaceSpecialChar(str) {
    var special = ["'", "\"", "=", "&", "@", ",", "*", ";", "%"];
    for (var i = 0 ; i < special.length; i++) {
        while (str.indexOf(special[i]) != -1) {
            str = str.replace(special[i], "");
        }
    }
    return str;
}
// ============================================================
// end
// 替换特殊字符
// ============================================================

// ============================================================
// 面包屑
// begin
// ============================================================
var Crumbs = {
    "Data": [{ "Name": "", "Url": "" }, { "Name": "", "Url": "" }, { "Name": "", "Url": "" }, { "Name": "", "Url": "" }, { "Name": "", "Url": "" }],
    "Fun_SetCrumbs": function () {
        if (this.Data != null && this.Data.length > 0) {
            var html = "";

            for (var i = 0 ; i < this.Data.length; i++) {
                if (this.Data[i].Name != "") {
                    html += "<a href='" + this.Data[i].Url + "' class='tip-bottom'><i class='icon-double-angle-right'></i> " + this.Data[i].Name + "</a>";
                }
            }

            $("#breadcrumb").html(html);
        }
    }
}
// ============================================================
// 面包屑
// end
// ============================================================

// ============================================================
// 数据处理
// begin
// ============================================================
function CommonFun_Ajax() {
    return {
        "type": "Get",
        "url": "",
        "data": {
            "Guid": ""
        },
        "getData": function (fun_success, fun_error) {
            // 跨域
            jQuery.support.cors = true;

            // 本次请求的GUID
            this.data.Guid = Math.random();

            var obj = $.ajax({
                "timeout": 60000, //超时时间设置，单位毫秒
                "type": this.type,
                "url": this.url + "?r=" + Math.random(),
                "data": this.data,
                "success": function (ret) {
                    if (ret != null && ret != "") {
                        var obj;
                        eval("(obj=" + ret + ")");
                        fun_success(obj);
                        return;
                    }
                    fun_error();
                },
                "complete": function (XMLHttpRequest, status) { //请求完成后最终执行参数
                    if (status == "timeout" || status == "error") {
                        fun_error();
                    }
                }
            });
        }
    }
}
// ============================================================
// end
// 数据处理
// ============================================================

// ============================================================
// 页面通用函数
// begin
// ============================================================
function CommonFun_Data() {
    return {
        "Data": new Array(),
        "Fun_Data_Init": function (obj) {
            if (obj == null || obj == "") {
                return;
            }

            // 如果传入的是数据集，则取第一个数据
            while (true) {
                var len = obj.length;
                if (len != null && len > 0) {
                    obj = obj[0];
                    break;
                }
                break;
            }

            var mydata = new Array();
            $.each(obj, function (val1, val2) {
                // Type 1 td/2 text等
                // Mode 0 初始/1 强制赋值
                mydata.push({ "Key": val1, "Key_Change": val1, "Value": val2, "Type": "", "Mode": "0" });
            });
            this.Data = mydata;
        },
        "Fun_Data_Add": function (val1, val2) {
            for (var i = 0; i < this.Data.length; i++) {
                var obj = this.Data[i];
                if (obj.Key == val1) {
                    obj.Value = val2;
                    obj.Mode = "1";
                    return false;
                }
            }

            this.Data.push({ "Key": val1, "Key_Change": val1, "Value": val2, "Type": "", "Mode": "1" });
        },
        "Fun_Change_Key": function (change_l, change_r) {
            for (var i = 0; i < this.Data.length; i++) {
                var obj = this.Data[i];

                var id = obj.Key;
                if (change_l != null && change_l != "") {
                    id = change_l + id;
                }
                if (change_r != null && change_r != "") {
                    id = id + change_r;
                }

                obj.Key_Change = id;
            }
        },
        "Fun_Set_Html": function () {
            for (var i = 0; i < this.Data.length; i++) {
                var obj = this.Data[i];
                var id = obj.Key_Change;

                if ($("#" + id).is("td")) {
                    $("#" + id).html(obj.Value);
                    obj.Type = "1";
                } else {
                    var _type = $("#" + id).prop("type");
                    if (_type != null) {
                        _type = _type.toLowerCase();

                        if (_type == "text" || _type == "hidden" || _type == "textarea" || _type == "password" || _type == "select-one") {
                            $("#" + id).val(obj.Value);
                            obj.Type = "2";
                        }
                    }
                }
            }
        },
        "Fun_Get_Html": function () {
            for (var i = 0; i < this.Data.length; i++) {
                var obj = this.Data[i];
                var id = obj.Key_Change;

                if ($("#" + id).is("td")) {
                    obj.Value = $("#" + id).html();
                    obj.Type = "1";
                } else {
                    var _type = $("#" + id).prop("type");
                    if (_type != null) {
                        _type = _type.toLowerCase();

                        if (_type == "text" || _type == "hidden" || _type == "textarea" || _type == "password" || _type == "select-one") {
                            obj.Value = $("#" + id).val();
                            obj.Type = "2";
                        }
                    }
                }
            }
        },
        "Fun_Serialize_All": function () {
            var ret = "";
            for (var i = 0; i < this.Data.length; i++) {
                var obj = this.Data[i];

                if (obj.Type == "1") {
                    if (ret != "") {
                        ret += "&";
                    }

                    ret += obj.Key + "=" + $("#" + obj.Key_Change).html();
                } else if (obj.Type == "2") {
                    if (ret != "") {
                        ret += "&";
                    }

                    ret += obj.Key + "=" + $("#" + obj.Key_Change).val();
                } else if (obj.Mode == "1") {
                    if (ret != "") {
                        ret += "&";
                    }

                    ret += obj.Key + "=" + obj.Value;
                } else {
                    if (ret != "") {
                        ret += "&";
                    }

                    ret += obj.Key + "=" + obj.Value;
                }
            }

            return ret;
        },
        "Fun_Serialize": function () {
            var ret = "";
            for (var i = 0; i < this.Data.length; i++) {
                var obj = this.Data[i];

                if (obj.Type == "1") {
                    if (ret != "") {
                        ret += "&";
                    }

                    ret += obj.Key + "=" + $("#" + obj.Key_Change).html();
                } else if (obj.Type == "2") {
                    if (ret != "") {
                        ret += "&";
                    }

                    ret += obj.Key + "=" + $("#" + obj.Key_Change).val();
                } else if (obj.Mode == "1") {
                    if (ret != "") {
                        ret += "&";
                    }

                    ret += obj.Key + "=" + obj.Value;
                }
            }

            return ret;
        },
        "Fun_Serialize_Para": function (ids) {
            var list = ids.split(',');
            var ret = "";

            for (var i = 0; i < this.Data.length; i++) {
                for (var j = 0 ; j < list.length; j++) {
                    if (ret != "") {
                        ret += "&";
                    }

                    if (this.Data[i].Key == list[j]) {
                        ret += this.Data[i].Key + "=" + this.Data[i].Value;
                    }
                }
            }

            return ret;
        },
        "Fun_Get_OneValue": function (key) {
            for (var i = 0 ; i < this.Data.length; i++) {
                if (this.Data[i].Key == key) {
                    return this.Data[i].Value;
                }
            }
            return "";
        },
        "Fun_Change_OneValue": function (key, val) {
            for (var i = 0 ; i < this.Data.length; i++) {
                if (this.Data[i].Key == key) {
                    this.Data[i].Value = val;
                }
            }
            return "";
        },
        "Fun_Set_OneCheckInfo": function (key, name, checkType) {
            var tf = false;
            if (this.Data == null || this.Data.length == 0) {
                this.Data = new Array();
            }
            for (var i = 0 ; i < this.Data.length; i++) {
                if (this.Data[i].Key == key) {
                    this.Data[i].Name = name;
                    this.Data[i].CheckType = checkType;
                    tf = true;
                    return;
                }
            }
            if (!tf) {
                this.Data.push({ "Key": key, "Key_Change": key, "Value": "", "Type": "", "Mode": "0", "Name": name, "CheckType": checkType });
                return;
            }
            return;
        },
        "Fun_Check": function () {
            for (var i = 0; i < this.Data.length; i++) {
                var obj = this.Data[i];

                if (obj.Name == null || obj.Name == "") {
                    continue;
                }

                var checkType = obj.CheckType.split(',');
                for (var j = 0 ; j < checkType.length; j++) {
                    var val = checkType[j];
                    val = CommonFun_ReplaceAll(val, " ", "").toLowerCase();
                    if (val == "null") {
                        if (obj.Value == null || obj.Value == "") {
                            throw obj.Name + "不能为空！";
                        }
                    }
                }
            }
        }
    };
}
// ============================================================
// end
// 页面通用函数
// ============================================================

// ============================================================
// select赋值
// begin
// ============================================================
function CommonFun_Select_Init(id, obj, selectID, nullName) {
    var html = "";
    var first = "";

    if (nullName != null && nullName != "") {
        html += "<option value=''>" + nullName + "</option>";
    }

    if (obj != null && obj.length > 0) {
        first = obj[0].SelectVal;
        for (var i = 0 ; i < obj.length; i++) {
            var row = obj[i];
            html += "<option value='" + row.SelectVal + "'>" + row.SelectTitle + "</option>";
        }
    }

    $("#" + id).html(html);

    if (selectID != null) {
        $("#" + id).val(selectID);
    } else {
        $("#" + id).val(first);
    }
}
// ============================================================
// end
// select赋值
// ============================================================

// ============================================================
// layer
// begin
// ============================================================
function CommonFun_OpenLayer(obj) {
    var para = "";
    if (obj.Data != null) {
        $.each(obj.Data, function (val1, val2) {
            para += "&" + val1 + "=" + val2;
        });
    }

    var _area;
    var _list = obj.Type.split("*");
    _area = [_list[0] + "px", _list[1] + "px"];

    var index = layer.open({
        type: 2,
        title: obj.Title,
        area: _area,
        fixed: true, //不固定
        maxmin: true,
        content: obj.Url + "?r=" + Math.random() + para,
        end: function () {
            obj.Fun_End();
        }
    });

    layer.iframeAuto(index);
}
// ============================================================
// end
// layer
// ============================================================

// ============================================================
// 替换
// begin
// ============================================================
function CommonFun_ReplaceAll(str, p1, p2) {
    while (str.indexOf(p1) >= 0) {
        str = str.replace(p1, p2);
    }
    return str;
}
// ============================================================
// end
// 替换
// ============================================================

// ============================================================
// 禁止退格
// begin
// ============================================================
document.onkeydown = CommonFun_BackSpace;
function CommonFun_BackSpace(e) {
    var ev = e || window.event;//获取event对象 
    var obj = ev.target || ev.srcElement;//获取事件源 
    var t = obj.type || obj.getAttribute('type');//获取事件源类型 
    //获取作为判断条件的事件类型 
    var vReadOnly = obj.getAttribute('readonly');
    var vEnabled = obj.getAttribute('enabled');
    //处理null值情况 
    if (vReadOnly == null) {
        vReadOnly = false;
    } else if (vReadOnly == "readonly" || vReadOnly == "True" || vReadOnly == "true") {
        vReadOnly = true;
    } else {
        vReadOnly = false;
    }
    vEnabled = (vEnabled == null) ? true : vEnabled;
    //当敲Backspace键时，事件源类型为密码或单行、多行文本的， 
    //并且readonly属性为true或enabled属性为false的，则退格键失效 
    var flag1 = (ev.keyCode == 8 && (t == "password" || t == "text" || t == "textarea") && (vReadOnly == true || vEnabled != true)) ? true : false;
    //当敲Backspace键时，事件源类型非密码或单行、多行文本的，则退格键失效 
    var flag2 = (ev.keyCode == 8 && t != "password" && t != "text" && t != "textarea") ? true : false;
    //判断 
    if (flag2) {
        return false;
    }
    if (flag1) {
        return false;
    }
}
// ============================================================
// end
// 禁止退格
// ============================================================

// ============================================================
// 获取当前时间，格式YYYY-MM-DD
// begin
// ============================================================
function CommonFun_GetNowFormatDate() {
    var date = new Date();
    var seperator1 = "-";
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    var strDate = date.getDate();
    if (month >= 1 && month <= 9) {
        month = "0" + month;
    }
    if (strDate >= 0 && strDate <= 9) {
        strDate = "0" + strDate;
    }
    var currentdate = year + seperator1 + month + seperator1 + strDate;
    return currentdate;
}

function CommonFun_GetYesterdayFormatDate() {
    var date = new Date();
    var preDate = new Date(date.getTime() - 24 * 60 * 60 * 1000);
    var seperator1 = "-";
    var year = preDate.getFullYear();
    var month = preDate.getMonth() + 1;
    var strDate = preDate.getDate();
    if (month >= 1 && month <= 9) {
        month = "0" + month;
    }
    if (strDate >= 0 && strDate <= 9) {
        strDate = "0" + strDate;
    }
    var currentdate = year + seperator1 + month + seperator1 + strDate;
    return currentdate;
}
// ============================================================
// end
// 获取当前时间，格式YYYY-MM-DD
// ============================================================