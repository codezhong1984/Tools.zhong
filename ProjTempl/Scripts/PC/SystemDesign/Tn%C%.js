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
