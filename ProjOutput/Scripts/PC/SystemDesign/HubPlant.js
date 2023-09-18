/**************************** 列表页面 *****************************/
function HubPlantList() { }
HubPlantList.prototype = {
    Query: function (PageIndex) {
        $.ajax({
            url: "handler/GetHubPlant.ashx?action=GetList",
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
                    url: "handler/GetHubPlant.ashx?action=Delete",
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
                                window.location.href = "HubPlant.aspx";
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
            window.location.href = "AddHubPlant.aspx";
        });
    }
}

/**************************** 新增页面 *****************************/
function HubPlantAdd() { }
HubPlantAdd.prototype = {
    Init: function () {
        LoadTnTargetStage();
        $(document).on("click", "#btnAdd", function () {
            var lgg = language();
                  var GID = $("#txtGID").val();
                  var COMPANY = $("#txtCOMPANY").val();
                  var PLANT = $("#txtPLANT").val();
                  var PLANT_NAME = $("#txtPLANT_NAME").val();
                  var SAP_CLIENT = $("#txtSAP_CLIENT").val();
                  var NOTE = $("#txtNOTE").val();
                  var CREATE_DATE = $("#txtCREATE_DATE").val();
                  var UPDATE_DATE = $("#txtUPDATE_DATE").val();
            if (stage == "" || stage == "-请选择-" || stage == "-Please Select-") {
                alert(message["logisticsStage"][lgg]);
                return false;
            }
            $.ajax({
                url: "handler/GetHubPlant.ashx?action=Add",
                data: {
                     GID: GID, 
 COMPANY: COMPANY, 
 PLANT: PLANT, 
 PLANT_NAME: PLANT_NAME, 
 SAP_CLIENT: SAP_CLIENT, 
 NOTE: NOTE, 
 CREATE_DATE: CREATE_DATE, 
 UPDATE_DATE: UPDATE_DATE, 
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
                     $("#txtGID").val("");
                    $("#txtCOMPANY").val("");
                    $("#txtPLANT").val("");
                    $("#txtPLANT_NAME").val("");
                    $("#txtSAP_CLIENT").val("");
                    $("#txtNOTE").val("");
                    $("#txtCREATE_DATE").val("");
                    $("#txtUPDATE_DATE").val("");
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
function HubPlantUpdate() { }
HubPlantUpdate.prototype = {
    Init: function () {
        LoadTnTargetStage();
        LoadHubPlant();
        $(document).on("click", "#btnUpdate", function () {
            var lgg = language();
            var gid = $("#hidId").val();
                  var GID = $("#txtGID").val();
                  var COMPANY = $("#txtCOMPANY").val();
                  var PLANT = $("#txtPLANT").val();
                  var PLANT_NAME = $("#txtPLANT_NAME").val();
                  var SAP_CLIENT = $("#txtSAP_CLIENT").val();
                  var NOTE = $("#txtNOTE").val();
                  var CREATE_DATE = $("#txtCREATE_DATE").val();
                  var UPDATE_DATE = $("#txtUPDATE_DATE").val();
            if (1=1) {
                alert(message["logisticsStage"][lgg]);
                return false;
            }
            $.ajax({
                url: "handler/GetHubPlant.ashx?action=Update",
                data: {
                    gid: gid,  GID: GID, 
 COMPANY: COMPANY, 
 PLANT: PLANT, 
 PLANT_NAME: PLANT_NAME, 
 SAP_CLIENT: SAP_CLIENT, 
 NOTE: NOTE, 
 CREATE_DATE: CREATE_DATE, 
 UPDATE_DATE: UPDATE_DATE, 
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

function LoadHubPlant() {
    var id = $("#hidId").val();
    $.ajax({
        url: "handler/GetHubPlant.ashx?action=GetUpdateData",
        dataType: "json",
        data: { id: id },
        type: "get",
        cache: false,
        async: true,
        success: function (o) {
            $("#txtGID").val(o.GID);
            $("#txtCOMPANY").val(o.COMPANY);
            $("#txtPLANT").val(o.PLANT);
            $("#txtPLANT_NAME").val(o.PLANT_NAME);
            $("#txtSAP_CLIENT").val(o.SAP_CLIENT);
            $("#txtNOTE").val(o.NOTE);
            $("#txtCREATE_DATE").val(o.CREATE_DATE);
            $("#txtUPDATE_DATE").val(o.UPDATE_DATE);
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
