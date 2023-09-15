/**************************** 列表页面 *****************************/
function %C%List() { }
%C%List.prototype = {
    Query: function (PageIndex) {
        $.ajax({
            url: "handler/Get%C%.ashx?action=GetList",
            data: { PageIndex: PageIndex },
            dataType: "html",
            type: "post",
            cache: false,
            async: true,
            success: function (o) {
                $("#container").html(o);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert(jqXHR.responseText);
            },
            beforeSend: function (XMLHttpRequest) {
                toggleloading("");
            },
            complete: function (XMLHttpRequest, textStatus) {
                $(".loading").remove();
            }
        })
    },
    Init: function () {
        $(document).on("click", "#checkAll", function () {
            var isChecked = $(this).prop("checked");
            $("input[name='select_item']").prop("checked", isChecked);
        });

        $(document).on("click", "#btnDel", function () {
            var lgg = language();
            var checkboxes = document.getElementsByName('select_item');
            var cheval = "";
            for (var i = 0; i < checkboxes.length; i++) {
                var checkbox = checkboxes[i];
                if (checkbox.checked) {
                    cheval += checkbox.value + ",";
                }
            }
            if (cheval == "") {
                alert(message["IDNotNull"][lgg]);
                return false;
            }
            var s = confirm(message["isDel"][lgg]);
            if (s == true) {
                $.ajax({
                    url: "handler/Get%C%.ashx?action=Delete",
                    data: { cheID: cheval },
                    dataType: "text",
                    type: "post",
                    cache: false,
                    async: true,
                    success: function (o) {
                        if (o == "Success") {
                            alert(message["deletedSuccessfully"][lgg]);
                            if (pageobj != undefined) {
                                pageobj.Query(1);
                            }
                            else {
                                window.location.href = "%C%.aspx";
                            }                           
                        }
                        else {
                            alert(o);
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        alert(jqXHR.responseText);
                    },
                    beforeSend: function (XMLHttpRequest) {
                        toggleloading("");
                    },
                    complete: function (XMLHttpRequest, textStatus) {
                        $(".loading").remove();
                    }
                })
            }
        });

        //点击查询按钮事件
        $(document).on("click", "#btnQuery", function () {
            Query(1);
        });

        //点击添加按钮页面跳
        $(document).on("click", "#btnAdd", function () {
            window.location.href = "Add%C%.aspx";
        });
    }
}

/**************************** 新增页面 *****************************/
function %C%Add() { }
%C%Add.prototype = {
    Init: function () {
        LoadTnTargetStage();
        $(document).on("click", "#btnAdd", function () {
            var lgg = language();
%LP%      
            var $1 = $("#txt$1").val();
%ELP%
            if (stage == "" || stage == "-请选择-" || stage == "-Please Select-") {
                alert(message["logisticsStage"][lgg]);
                return false;
            }
            $.ajax({
                url: "handler/Get%C%.ashx?action=Add",
                data: {
                    %LP% $1: $1, %ELP%
                },
                dataType: "text",
                type: "post",
                cache: false,
                async: true,
                success: function (o) {
                    if (o == "Success") {
                        alert(message["addedSuccessfully"][lgg]);
                    }
                    else {
                        alert(o);
                    }
 %LP%
                    $("#txt$1").val("");
%ELP%
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert(jqXHR.responseText);
                },
                beforeSend: function (XMLHttpRequest) {
                    toggleloading("");
                },
                complete: function (XMLHttpRequest, textStatus) {
                    $(".loading").remove();
                }
            })
        });
    }
}

/**************************** 修改页面 *****************************/
function %C%Update() { }
%C%Update.prototype = {
    Init: function () {
        LoadTnTargetStage();
        Load%C%();
        $(document).on("click", "#btnUpdate", function () {
            var lgg = language();
            var gid = $("#hidId").val();
%LP%      
            var $1 = $("#txt$1").val();
%ELP%
            if (1=1) {
                alert(message["logisticsStage"][lgg]);
                return false;
            }
            $.ajax({
                url: "handler/Get%C%.ashx?action=Update",
                data: {
                    gid: gid, %LP% $1: $1, %ELP%
                },
                dataType: "text",
                type: "post",
                cache: false,
                async: false,
                success: function (msg) {
                    if (msg == "Success") {
                        alert(message["modifySuccessful"][lgg]);
                        window.history.back(-1);
                    }
                    else {
                        alert(msg);
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert(jqXHR.responseText);
                },
                beforeSend: function (XMLHttpRequest) {
                    toggleloading("");
                },
                complete: function (XMLHttpRequest, textStatus) {
                    $(".loading").remove();
                }
            })
        });
    }
}

function Load%C%() {
    var id = $("#hidId").val();
    $.ajax({
        url: "handler/Get%C%.ashx?action=GetUpdateData",
        dataType: "json",
        data: { id: id },
        type: "get",
        cache: false,
        async: true,
        success: function (o) {
%LP%
            $("#txt$1").val(o.$1);
%ELP%
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(jqXHR.responseText);
        },
        beforeSend: function (XMLHttpRequest) {
            toggleloading("");
        },
        complete: function (XMLHttpRequest, textStatus) {
            $(".loading").remove();
        }
    })
}
﻿/**************************** 列表页面 *****************************/
function TnOwnerList() { }
TnOwnerList.prototype = {
    Query: function (PageIndex) {
        $.ajax({
            url: "handler/GetTnOwner.ashx?action=GetList",
            data: { PageIndex: PageIndex },
            dataType: "html",
            type: "post",
            cache: false,
            async: true,
            success: function (o) {
                $("#container").html(o);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert(jqXHR.responseText);
            },
            beforeSend: function (XMLHttpRequest) {
                toggleloading("");
            },
            complete: function (XMLHttpRequest, textStatus) {
                $(".loading").remove();
            }
        })
    },
    Init: function () {
        $(document).on("click", "#checkAll", function () {
            var isChecked = $(this).prop("checked");
            $("input[name='select_item']").prop("checked", isChecked);
        });

        $(document).on("click", "#btnDel", function () {
            var lgg = language();
            var checkboxes = document.getElementsByName('select_item');
            var cheval = "";
            for (var i = 0; i < checkboxes.length; i++) {
                var checkbox = checkboxes[i];
                if (checkbox.checked) {
                    cheval += checkbox.value + ",";
                }
            }
            if (cheval == "") {
                alert(message["IDNotNull"][lgg]);
                return false;
            }
            var s = confirm(message["isDel"][lgg]);
            if (s == true) {
                $.ajax({
                    url: "handler/GetTnOwner.ashx?action=Delete",
                    data: { cheID: cheval },
                    dataType: "text",
                    type: "post",
                    cache: false,
                    async: true,
                    success: function (o) {
                        if (o == "Success") {
                            alert(message["deletedSuccessfully"][lgg]);
                            if (pageobj != undefined) {
                                pageobj.Query(1);
                            }
                            else {
                                window.location.href = "TnOwner.aspx";
                            }                           
                        }
                        else {
                            alert(o);
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        alert(jqXHR.responseText);
                    },
                    beforeSend: function (XMLHttpRequest) {
                        toggleloading("");
                    },
                    complete: function (XMLHttpRequest, textStatus) {
                        $(".loading").remove();
                    }
                })
            }
        });

        //点击查询按钮事件
        $(document).on("click", "#btnQuery", function () {
            Query(1);
        });

        //点击添加按钮页面跳
        $(document).on("click", "#btnAdd", function () {
            window.location.href = "AddTnOwner.aspx";
        });
    }
}

/**************************** 新增页面 *****************************/
function TnOwnerAdd() { }
TnOwnerAdd.prototype = {
    Init: function () {
        LoadTnTargetStage();
        $(document).on("click", "#btnAdd", function () {
            var lgg = language();
%LP%      
            var $1 = $("#txt$1").val();
%ELP%
            if (stage == "" || stage == "-请选择-" || stage == "-Please Select-") {
                alert(message["logisticsStage"][lgg]);
                return false;
            }
            $.ajax({
                url: "handler/GetTnOwner.ashx?action=Add",
                data: {
                     GID: GID,  SUPPLIER_CODE: SUPPLIER_CODE,  SITE_CODE: SITE_CODE,  KPSN_GID: KPSN_GID,  FG_SN: FG_SN,  FG_PN: FG_PN,  BU_PLANT: BU_PLANT,  T5_STARTDATE_1: T5_STARTDATE_1,  T5_STARTDATE_2: T5_STARTDATE_2,  T5_ENDDATE: T5_ENDDATE,  IS_END: IS_END,  MATERIAL_NUMBER: MATERIAL_NUMBER,  MATERIAL_DESCRIPTION: MATERIAL_DESCRIPTION,  PN_SIZE: PN_SIZE,  INBOUND_TYPE: INBOUND_TYPE,  SAP_CLIENT_IN: SAP_CLIENT_IN,  INBOUND_DN: INBOUND_DN,  INBOUND_DN_ITEM: INBOUND_DN_ITEM,  INBOUND_DATE: INBOUND_DATE,  MOVEIN_TYPE: MOVEIN_TYPE,  MOVEIN_TYPE_DESC: MOVEIN_TYPE_DESC,  PLANT_CODE: PLANT_CODE,  STORE_LOC_IN: STORE_LOC_IN,  SAP_CLIENT_OUT: SAP_CLIENT_OUT,  OUTBOUND_DN: OUTBOUND_DN,  OUTBOUND_DN_ITEM: OUTBOUND_DN_ITEM,  OUTBOUND_DATE: OUTBOUND_DATE,  MOVEOUT_TYPE: MOVEOUT_TYPE,  MOVEOUT_TYPE_DESC: MOVEOUT_TYPE_DESC,  PLANT_CODE_OUT: PLANT_CODE_OUT,  STORE_LOC_OUT: STORE_LOC_OUT,  DC_SHIP_TO: DC_SHIP_TO,  TRANSFER_FLAG: TRANSFER_FLAG,  TRANSFER_SITE: TRANSFER_SITE,  TRANSFER_DATE: TRANSFER_DATE,  SAP_CLIENT_BACK: SAP_CLIENT_BACK,  BACK_DN: BACK_DN,  BACK_DN_ITEM: BACK_DN_ITEM,  BACK_DATE: BACK_DATE,  DEDUCT_DAYS: DEDUCT_DAYS,  KPJOB_STATE: KPJOB_STATE,  LAST_STORE_LOC: LAST_STORE_LOC,  LAST_OP_TYPE: LAST_OP_TYPE,  LAST_SESSION_GID: LAST_SESSION_GID,  LAST_LOG_GID: LAST_LOG_GID,  LAST_OP_TIME: LAST_OP_TIME,  DEL_FLAG: DEL_FLAG,  DEL_REASON: DEL_REASON,  CREATE_DATE: CREATE_DATE,  CREATE_BY: CREATE_BY,  UPDATE_DATE: UPDATE_DATE,  UPDATE_BY: UPDATE_BY,  IS_SYNC_OUT: IS_SYNC_OUT,  SYNC_OUT_DATE: SYNC_OUT_DATE,  SYNC_OUT_MESSAGE: SYNC_OUT_MESSAGE, 
                },
                dataType: "text",
                type: "post",
                cache: false,
                async: true,
                success: function (o) {
                    if (o == "Success") {
                        alert(message["addedSuccessfully"][lgg]);
                    }
                    else {
                        alert(o);
                    }
 %LP%
                    $("#txt$1").val("");
%ELP%
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert(jqXHR.responseText);
                },
                beforeSend: function (XMLHttpRequest) {
                    toggleloading("");
                },
                complete: function (XMLHttpRequest, textStatus) {
                    $(".loading").remove();
                }
            })
        });
    }
}

/**************************** 修改页面 *****************************/
function TnOwnerUpdate() { }
TnOwnerUpdate.prototype = {
    Init: function () {
        LoadTnTargetStage();
        LoadTnOwner();
        $(document).on("click", "#btnUpdate", function () {
            var lgg = language();
            var gid = $("#hidId").val();
%LP%      
            var $1 = $("#txt$1").val();
%ELP%
            if (1=1) {
                alert(message["logisticsStage"][lgg]);
                return false;
            }
            $.ajax({
                url: "handler/GetTnOwner.ashx?action=Update",
                data: {
                    gid: gid,  GID: GID,  SUPPLIER_CODE: SUPPLIER_CODE,  SITE_CODE: SITE_CODE,  KPSN_GID: KPSN_GID,  FG_SN: FG_SN,  FG_PN: FG_PN,  BU_PLANT: BU_PLANT,  T5_STARTDATE_1: T5_STARTDATE_1,  T5_STARTDATE_2: T5_STARTDATE_2,  T5_ENDDATE: T5_ENDDATE,  IS_END: IS_END,  MATERIAL_NUMBER: MATERIAL_NUMBER,  MATERIAL_DESCRIPTION: MATERIAL_DESCRIPTION,  PN_SIZE: PN_SIZE,  INBOUND_TYPE: INBOUND_TYPE,  SAP_CLIENT_IN: SAP_CLIENT_IN,  INBOUND_DN: INBOUND_DN,  INBOUND_DN_ITEM: INBOUND_DN_ITEM,  INBOUND_DATE: INBOUND_DATE,  MOVEIN_TYPE: MOVEIN_TYPE,  MOVEIN_TYPE_DESC: MOVEIN_TYPE_DESC,  PLANT_CODE: PLANT_CODE,  STORE_LOC_IN: STORE_LOC_IN,  SAP_CLIENT_OUT: SAP_CLIENT_OUT,  OUTBOUND_DN: OUTBOUND_DN,  OUTBOUND_DN_ITEM: OUTBOUND_DN_ITEM,  OUTBOUND_DATE: OUTBOUND_DATE,  MOVEOUT_TYPE: MOVEOUT_TYPE,  MOVEOUT_TYPE_DESC: MOVEOUT_TYPE_DESC,  PLANT_CODE_OUT: PLANT_CODE_OUT,  STORE_LOC_OUT: STORE_LOC_OUT,  DC_SHIP_TO: DC_SHIP_TO,  TRANSFER_FLAG: TRANSFER_FLAG,  TRANSFER_SITE: TRANSFER_SITE,  TRANSFER_DATE: TRANSFER_DATE,  SAP_CLIENT_BACK: SAP_CLIENT_BACK,  BACK_DN: BACK_DN,  BACK_DN_ITEM: BACK_DN_ITEM,  BACK_DATE: BACK_DATE,  DEDUCT_DAYS: DEDUCT_DAYS,  KPJOB_STATE: KPJOB_STATE,  LAST_STORE_LOC: LAST_STORE_LOC,  LAST_OP_TYPE: LAST_OP_TYPE,  LAST_SESSION_GID: LAST_SESSION_GID,  LAST_LOG_GID: LAST_LOG_GID,  LAST_OP_TIME: LAST_OP_TIME,  DEL_FLAG: DEL_FLAG,  DEL_REASON: DEL_REASON,  CREATE_DATE: CREATE_DATE,  CREATE_BY: CREATE_BY,  UPDATE_DATE: UPDATE_DATE,  UPDATE_BY: UPDATE_BY,  IS_SYNC_OUT: IS_SYNC_OUT,  SYNC_OUT_DATE: SYNC_OUT_DATE,  SYNC_OUT_MESSAGE: SYNC_OUT_MESSAGE, 
                },
                dataType: "text",
                type: "post",
                cache: false,
                async: false,
                success: function (msg) {
                    if (msg == "Success") {
                        alert(message["modifySuccessful"][lgg]);
                        window.history.back(-1);
                    }
                    else {
                        alert(msg);
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert(jqXHR.responseText);
                },
                beforeSend: function (XMLHttpRequest) {
                    toggleloading("");
                },
                complete: function (XMLHttpRequest, textStatus) {
                    $(".loading").remove();
                }
            })
        });
    }
}

function LoadTnOwner() {
    var id = $("#hidId").val();
    $.ajax({
        url: "handler/GetTnOwner.ashx?action=GetUpdateData",
        dataType: "json",
        data: { id: id },
        type: "get",
        cache: false,
        async: true,
        success: function (o) {
%LP%
            $("#txt$1").val(o.$1);
%ELP%
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(jqXHR.responseText);
        },
        beforeSend: function (XMLHttpRequest) {
            toggleloading("");
        },
        complete: function (XMLHttpRequest, textStatus) {
            $(".loading").remove();
        }
    })
}
