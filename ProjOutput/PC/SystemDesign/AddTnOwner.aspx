<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add%C%.aspx.cs" Inherits="PanelTracking.PC.SystemDesign.Add%C%" %>

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
                            <a href="%C%.aspx" class="fr mt10 querycor"><i class="myicon-menuBtn4"></i></a>
                        </div>
                        <div class="box">
                            <div class="message">
                                <div class="wrap-title-sm">
                                    <h4 runat="server" id="requestinfo">
                                        <i class="myicon-apply1"></i>新增</h4>
                                </div>
                                <div class="message-content clear">
%LP%
                                    <div class="messagebox">
                                        <span class="message-title fl w160" runat="server" id="span$1">$3:</span>
                                        <div class="messagebox-input fl mr10">
                                            <input class="input-text" type="text" name="txt$1" id="txt$1" runat="server" value="" />
                                        </div>
                                    </div>
%LP%									
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
    <script src="../../Scripts/PC/SystemDesign/%C%.js?v=20220930001"></script>
    <script type="text/javascript">
        var pageobj = new %C%Add();
        $().ready(function () {
            pageobj.Init();
        });
    </script>
</body>
</html>
﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddTnOwner.aspx.cs" Inherits="PanelTracking.PC.SystemDesign.AddTnOwner" %>

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
                            <a href="TnOwner.aspx" class="fr mt10 querycor"><i class="myicon-menuBtn4"></i></a>
                        </div>
                        <div class="box">
                            <div class="message">
                                <div class="wrap-title-sm">
                                    <h4 runat="server" id="requestinfo">
                                        <i class="myicon-apply1"></i>新增</h4>
                                </div>
                                <div class="message-content clear">
%LP%
                                    <div class="messagebox">
                                        <span class="message-title fl w160" runat="server" id="span$1">$3:</span>
                                        <div class="messagebox-input fl mr10">
                                            <input class="input-text" type="text" name="txt$1" id="txt$1" runat="server" value="" />
                                        </div>
                                    </div>
%LP%									
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
    <script src="../../Scripts/PC/SystemDesign/TnOwner.js?v=20220930001"></script>
    <script type="text/javascript">
        var pageobj = new TnOwnerAdd();
        $().ready(function () {
            pageobj.Init();
        });
    </script>
</body>
</html>
