<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HubPlant.aspx.cs" Inherits="PanelTracking.PC.SystemDesign.HubPlant" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../css/PCcss/icon.css" rel="stylesheet" />
    <link href="../../css/PCcss/reset.css" rel="stylesheet" />
    <link href="../../css/PCcss/response.css" rel="stylesheet" />
    <link href="../../css/PCcss/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="content clearFix">
            <!-- content-start -->
            <div class="contentbox">
                <div class="wrap">
                    <div class="wrapbox">
                        <div class="wrap-title">
                            <h3 runat="server" id="h3ViewHistory">
                                <i class="myicon-home"></i>%DES%</h3>
                        </div>
                       <%-- <div class="reportbox clear">
                            <div class="reportQuery-btn fl">
                                <input type="button" class="btn btnQuery fl  mr10" runat="server" id="btnQuery" value="查询" />
                            </div>
                        </div>--%>
                        <div class="reportbox h50 ">
                            <input type="button" class="btn btnQuery fl  mr10" id="btnAdd" value="新增" runat="server" />
                            <input type="button" class="btn btnQuery fl  mr10" id="btnDel" value="删除" runat="server" />
                        </div>
                        <div id="container">
                        </div>
                    </div>
                </div>
            </div>
            <!-- content-over -->
        </div>
    </form>
    <script src="../../js/PCjs/jquery-1.8.3.min.js"></script>
    <script src="../../Scripts/PC/Message.js?v=20190620004"></script>
    <script src="../../js/PCjs/common.js?v=20190620005"></script>
    <script src="../../Scripts/PC/SystemDesign/HubPlant.js?v=20220930001"></script>
    <script type="text/javascript">
        var pageobj = new HubPlantList();
        $().ready(function () {
            pageobj.Init();
            pageobj.Query(1);
        });
    </script>
</body>
</html>
