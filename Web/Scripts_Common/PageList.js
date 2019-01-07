// ============================================================
// 数据处理
// begin
// ============================================================
function PageListFun_Data() {
    return {
        "Guid": "A",
        "PageList_Index": 1,
        "PageList_Count": 15,
        "UserID": "",
        "OrgCode": "",
        "RRoleCode": "",
        "DRoleType": "",
        "Para1": "",
        "Para2": "",
        "Para3": "",
        "Para4": "",
        "Para5": "",
        "Para6": "",
        "Para7": "",
        "Para8": "",
        "Para9": "",
        "PageList_Json": function () {
            var json = "";
            json += "PageList_Index=" + this.PageList_Index;
            json += "&PageList_Count=" + this.PageList_Count;
            json += "&UserID=" + this.UserID;
            json += "&OrgCode=" + this.OrgCode;
            json += "&RRoleCode=" + this.RRoleCode;
            json += "&DRoleType=" + this.DRoleType;

            if ($("#Para1").val() != null) {
                this.Para1 = $("#Para1").val();
            }
            if ($("#Para2").val() != null) {
                this.Para2 = $("#Para2").val();
            }
            if ($("#Para3").val() != null) {
                this.Para3 = $("#Para3").val();
            }
            if ($("#Para4").val() != null) {
                this.Para4 = $("#Para4").val();
            }
            if ($("#Para5").val() != null) {
                this.Para5 = $("#Para5").val();
            }
            if ($("#Para6").val() != null) {
                this.Para6 = $("#Para6").val();
            }
            if ($("#Para7").val() != null) {
                this.Para7 = $("#Para7").val();
            }
            if ($("#Para8").val() != null) {
                this.Para8 = $("#Para8").val();
            }
            if ($("#Para9").val() != null) {
                this.Para9 = $("#Para9").val();
            }

            if (this.Para1 != "") {
                json += "&Para1=" + this.Para1;
            }
            if (this.Para2 != "") {
                json += "&Para2=" + this.Para2;
            }
            if (this.Para3 != "") {
                json += "&Para3=" + this.Para3;
            }
            if (this.Para4 != "") {
                json += "&Para4=" + this.Para4;
            }
            if (this.Para5 != "") {
                json += "&Para5=" + this.Para5;
            }
            if (this.Para6 != "") {
                json += "&Para6=" + this.Para6;
            }
            if (this.Para7 != "") {
                json += "&Para7=" + this.Para7;
            }
            if (this.Para8 != "") {
                json += "&Para8=" + this.Para8;
            }
            if (this.Para9 != "") {
                json += "&Para9=" + this.Para9;
            }
            return json;
        },

        "PageList_Data": null,
        "PageList_RowCount": 1,
        "PageList_Num": [],
        "PageList_BtnInfo": {
            "Data": new Array(),
            "BtnI": 0,
            "Btns": new Array()
        },

        "Fun_Loading": function () { },

        "Fun_InitData": function (data) {
            if (data == null || data.length == 0) {
                if (this.PageList_Index > 1) {
                    this.PageList_Index--;
                    this.Fun_Loading();
                    return false;
                } else {
                    this.PageList_Data = null;
                    this.PageList_RowCount = 1;
                }
            } else {

                this.PageList_Data = data;
                if (data[0].c != null && parseFloat(data[0].c) > 0) {
                    this.PageList_RowCount = parseFloat(data[0].c);
                } else {
                    this.PageList_RowCount = 1;
                }
            }
            this.Fun_ResetList();
        },
        "Fun_ResetList": function () {
            if (this.PageList_RowCount <= 0) {
                this.PageList_Num = [];
            } else {
                var max = Math.ceil(1.00 * this.PageList_RowCount / this.PageList_Count);

                if (this.PageList_Index > max) {
                    this.PageList_Index = max;
                    return false;
                }

                var list = [];
                for (var i = this.PageList_Index - 5; i < this.PageList_Index; i++) {
                    if (i > 0) {
                        list.push(i);
                    }
                }
                for (var i = this.PageList_Index; i < this.PageList_Index + 6 && i <= max; i++) {
                    list.push(i);
                }

                this.PageList_Num = list;
            }
        },

        "Fun_InitDiv": function (divID) {
            var list = this.PageList_Num;

            var html = "";
            html += "<ul class='pagination' id='" + this.Guid + "'>";
            html += "<li class='Li_1'>";
            html += "<a href='javascript:void(0);' aria-label='Previous'>";
            html += "<span aria-hidden='true'>&laquo;</span>";
            html += "</a>";
            html += "</li>";
            for (var i = 0; i < list.length; i++) {
                if (list[i] == this.PageList_Index) {
                    html += "<li class='active Li_2' val='" + list[i] + "'><a href='javascript:void(0);'>" + list[i] + "</a></li>";
                } else {
                    html += "<li class='Li_2' val='" + list[i] + "'><a href='javascript:void(0);'>" + list[i] + "</a></li>";
                }
            }
            html += "<li class='Li_3'>";
            html += "<a href='javascript:void(0);' aria-label='Next'>";
            html += "<span aria-hidden='true'>&raquo;</span>";
            html += "</a>";
            html += "</li>";
            html += "</ul>";

            $("#" + divID).html(html);

            this.Fun_Bind_Div();
        },
        "Fun_GoTo": function (ind) {
            var max = Math.ceil(1.00 * this.PageList_RowCount / this.PageList_Count);

            if (ind == -2) {
                if (this.PageList_Index >= 2) {
                    this.PageList_Index--;
                } else {
                    this.PageList_Index = 1;
                }
            } else if (ind == -1) {
                if (this.PageList_Index <= max - 1) {
                    this.PageList_Index++;
                } else {
                    this.PageList_Index = max;
                }
            } else {
                if (1 <= ind && ind <= max) {
                    this.PageList_Index = ind;
                } else {
                    this.PageList_Index = 1;
                }
            }

            this.Fun_Loading();
        },
        "Fun_Bind_Div": function () {
            var obj = $(this)[0];

            $("#" + this.Guid + " li.Li_1").unbind().click(function () {
                obj.Fun_GoTo(-2);
            });
            $("#" + this.Guid + " li.Li_2").unbind().click(function () {
                var val = $(this).attr("val");
                obj.Fun_GoTo(val);
            });
            $("#" + this.Guid + " li.Li_3").unbind().click(function () {
                obj.Fun_GoTo(-1);
            });
        },

        "Fun_InitTable": function (bodyID, key) {
            var th_list = $("#" + bodyID).parent().find("thead th");
            var th_count = th_list.length;

            var html = "";
            for (var i = 0; i < this.PageList_Count; i++) {
                if (key == null || key == "") {
                    html += "<tr name='" + this.Guid + "'>";
                } else {
                    html += "<tr name='" + this.Guid + "' id='{" + i + "}'>";
                }
                for (var j = 0; j < th_count; j++) {
                    html += "<td {" + i + "}>{" + i + "}</td>";
                }
                html += "</tr>";
            }

            var obj = this.PageList_Data;
            var obj_length = 0;
            if (obj != null && obj != "") {
                obj_length = obj.length;
            } else {
                obj_length = 0;
            }

            this.PageList_BtnInfo.Data = new Array();
            for (var i = 0; i < obj_length; i++) {
                obj[i].BtnI = i;
                this.PageList_BtnInfo.Data.push(obj[i]);
                this.PageList_BtnInfo.BtnI = i;

                if (key == null || key == "") {
                } else {
                    html = html.replace("{" + i + "}", this.Fun_GetObjValue(obj[i], key));
                }
                for (var j = 0; j < th_count; j++) {
                    var th = $(th_list[j]);
                    if (th.attr("class") == "c") {
                        html = html.replace("{" + i + "}", "style='text-align:center;'");
                    }
                    if (th.attr("class") == "l") {
                        html = html.replace("{" + i + "}", "style='text-align:left;'");
                    }
                    if (th.attr("class") == "r") {
                        html = html.replace("{" + i + "}", "style='text-align:right;'");
                    }
                    html = html.replace("{" + i + "}", this.Fun_GetObjValue(obj[i], th.attr("name")));
                }
            }
            for (var i = obj_length; i < this.PageList_Count; i++) {
                if (key == null || key == "") {
                } else {
                    html = html.replace("{" + i + "}", "");
                }
                for (var j = 0; j < th_count; j++) {
                    html = html.replace("{" + i + "}", "");
                    if (j == 1) {
                        html = html.replace("{" + i + "}", "&nbsp");
                    } else {
                        html = html.replace("{" + i + "}", "");
                    }
                }
            }

            $("#" + bodyID).html(html);

            this.Fun_Bind_Table();
        },
        "Fun_Bind_Table": function () {
            var btns = this.PageList_BtnInfo.Btns;
            var guid = this.Guid;
            var data = this.PageList_BtnInfo.Data;

            $.each(btns, function (i, btn) {
                $(".table tr[name='" + guid + "'] a[name='" + btn.Tag + "']").unbind().click(function () {
                    var btni = $(this).attr("BtnI");
                    for (var i = 0 ; i < data.length; i++) {
                        if (data[i].BtnI == btni) {
                            btn.Fun_Click(data[i]);
                        }
                    }
                });
            });
        },
        "Fun_GetObjValue": function (obj, id) {
            var ind = 0;
            var ret = "";

            if (id == "op") {
                var btns = this.PageList_BtnInfo.Btns;
                if (btns != null && btns.length > 0) {
                    for (var j = 0; j < btns.length; j++) {
                        var btn = btns[j];

                        ret = this.Fun_GetOPHtml(ret, obj, btn);
                    }
                }
            } else {
                $.each(obj, function (val1, val2) {
                    if (val1 == id) {
                        ret = val2;
                    }

                    ind++;
                });
            }
            return ret;
        },
        "Fun_GetOPHtml": function (ret, obj, btn) {
            var html = "";
            if (btn.Fun_Callback(obj)) {
                if (btn.Tag == "ToggleStatus") {
                    if (ret == "") {
                        html = "<a href='javascript:void(0);' name='" + btn.Tag + "' BtnI='" + this.PageList_BtnInfo.BtnI + "'>" + (obj.Del == "0" ? "禁用" : "启用") + "</a>";
                    } else {
                        html = "<a href='javascript:void(0);' name='" + btn.Tag + "' BtnI='" + this.PageList_BtnInfo.BtnI + "' style='margin-left:10px;'>" + (obj.Del == "0" ? "禁用" : "启用") + "</a>";
                    }
                }
                else {
                    if (ret == "") {
                        html = "<a href='javascript:void(0);' name='" + btn.Tag + "' BtnI='" + this.PageList_BtnInfo.BtnI + "'>" + btn.Title + "</a>";
                    } else {
                        html = "<a href='javascript:void(0);' name='" + btn.Tag + "' BtnI='" + this.PageList_BtnInfo.BtnI + "' style='margin-left:10px;'>" + btn.Title + "</a>";
                    }
                }
            }

            return ret + html;
        }
    }
}
// ============================================================
// end
// 数据处理
// ============================================================