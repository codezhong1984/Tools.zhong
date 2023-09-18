<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddHubPlant.aspx.cs" Inherits="PanelTracking.PC.SystemDesign.AddHubPlant" %>

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
                            <h3 runat="server" id="title">
                                <i class="myicon-home"></i>
                                <span class="titlebox" id="h3ViewHistory" runat="server">%DESC%</span>
                            </h3>
                            <a href="HubPlant.aspx" class="fr mt10 querycor"><i class="myicon-menuBtn4"></i></a>
                        </div>
                        <div class="box">
                            <div class="message">
                                <div class="wrap-title-sm">
                                    <h4 runat="server" id="requestinfo">
                                        <i class="myicon-apply1"></i>新增</h4>
                                </div>
                                <div class="message-content clear">

                                    <div class="messagebox">
                                        <span class="message-title fl w160" runat="server" id="spanGID">:</span>
                                        <div class="messagebox-input fl mr10">
                                            <input class="input-text" type="text" name="txtGID" id="txtGID" runat="server" value="" />
                                        </div>
                                    </div>

                                    <div class="messagebox">
                                        <span class="message-title fl w160" runat="server" id="spanCOMPANY">Company AOC/PHP:</span>
                                        <div class="messagebox-input fl mr10">
                                            <input class="input-text" type="text" name="txtCOMPANY" id="txtCOMPANY" runat="server" value="" />
                                        </div>
                                    </div>

                                    <div class="messagebox">
                                        <span class="message-title fl w160" runat="server" id="spanPLANT">PLANT AOC：1110、1290、1310，PHP：1200、1230、1160、1120:</span>
                                        <div class="messagebox-input fl mr10">
                                            <input class="input-text" type="text" name="txtPLANT" id="txtPLANT" runat="server" value="" />
                                        </div>
                                    </div>

                                    <div class="messagebox">
                                        <span class="message-title fl w160" runat="server" id="spanPLANT_NAME">:</span>
                                        <div class="messagebox-input fl mr10">
                                            <input class="input-text" type="text" name="txtPLANT_NAME" id="txtPLANT_NAME" runat="server" value="" />
                                        </div>
                                    </div>

                                    <div class="messagebox">
                                        <span class="message-title fl w160" runat="server" id="spanSAP_CLIENT">SAP CLIENT NAME:</span>
                                        <div class="messagebox-input fl mr10">
                                            <input class="input-text" type="text" name="txtSAP_CLIENT" id="txtSAP_CLIENT" runat="server" value="" />
                                        </div>
                                    </div>

                                    <div class="messagebox">
                                        <span class="message-title fl w160" runat="server" id="spanNOTE">:</span>
                                        <div class="messagebox-input fl mr10">
                                            <input class="input-text" type="text" name="txtNOTE" id="txtNOTE" runat="server" value="" />
                                        </div>
                                    </div>

                                    <div class="messagebox">
                                        <span class="message-title fl w160" runat="server" id="spanCREATE_DATE">:</span>
                                        <div class="messagebox-input fl mr10">
                                            <input class="input-text" type="text" name="txtCREATE_DATE" id="txtCREATE_DATE" runat="server" value="" />
                                        </div>
                                    </div>

                                    <div class="messagebox">
                                        <span class="message-title fl w160" runat="server" id="spanUPDATE_DATE">:</span>
                                        <div class="messagebox-input fl mr10">
                                            <input class="input-text" type="text" name="txtUPDATE_DATE" id="txtUPDATE_DATE" runat="server" value="" />
                                        </div>
                                    </div>
									
                                    <div class="messagebox disblock hauto wp45 clear mb5">
                                        <span class="message-title messagebox-textarea fl w160" runat="server" id="SpanID">&nbsp;</span>
                                        <div class="messagebox-input fl">
                                            <button class="btn btnQuery marauto" type="button" runat="server" name="" id="btnAdd">
                                                <i class="myicon-login1"></i>保存</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
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
        var pageobj = new HubPlantAdd();
        $().ready(function () {
            pageobj.Init();
        });
    </script>
</body>
</html>
