﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="lunbo_edit.aspx.cs" Inherits="DTcms.Web.admin.lunbo_edit"
    ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>編輯圖片管理資料</title>
    <link href="../scripts/ui/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="images/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../scripts/jquery/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../scripts/jquery/jquery.form.js"></script>
    <script type="text/javascript" src="../scripts/jquery/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../scripts/jquery/messages_cn.js"></script>
    <script type="text/javascript" src="../scripts/ui/js/ligerBuild.min.js"></script>
    <script type='text/javascript' src="../scripts/swfupload/swfupload.js"></script>
    <script type='text/javascript' src="../scripts/swfupload/swfupload.queue.js"></script>
    <script type="text/javascript" src="../scripts/swfupload/swfupload.handlers.js"></script>
    <script type="text/javascript" src="js/function.js"></script>
    <script type="text/javascript" charset="utf-8" src="../editor/kindeditor-min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../editor/lang/zh_CN.js"></script>
    <script language="javascript" type="text/javascript" src="js/getdate/WdatePicker.js"></script>
    <script type="text/javascript">
        //表單驗證
        $(function () {
            $("#form1").validate({
                errorPlacement: function (lable, element) {
                    element.ligerTip({ content: lable.html(), appendIdTo: lable });
                },
                success: function (lable) {
                    lable.ligerHideTip();
                }
            });
        });
        //功能表事件處理
        $(function () {
            //初始化按鈕事件
            $("#nav_box tr").each(function (i) {
                initButton(i);
            });
            //添加按鈕(點擊綁定)
            $("#navAddButton").click(function () {
                var navSize = $('#nav_box tr').size();
                var navRow = getTr(navSize);
                $("#nav_box").append(navRow);
                initButton(navSize);
            });
        });



        //表格行的功能表內容
        function getTr(indexValue) {
            var navRow = '<tr class="td_c">'
        + '<td><input name="nav_id" type="hidden" value="0" /><input name="nav_title" type="text" class="txtInput small" /></td>'
        + '<td><input name="nav_url" type="text" class="txtInput middle" /></td>'
        + '<td><input name="nav_sort" type="text" value="' + indexValue + '" class="txtInput" style="width:30px;" /></td>'
		+ '<td><img alt="刪除" src="../images/icon_del.gif" class="operator" /></td>'
		+ '</tr>';
            return navRow;
        }

        //初始化按鈕事件
        function initButton(indexValue) {
            //功能操作按鈕
            $("#nav_box tr:eq(" + indexValue + ") .operator").each(function (i) {
                switch (i) {
                    //刪除                       
                    case 0:
                        $(this).click(function () {
                            var obj = $(this);
                            $.ligerDialog.confirm("您確定要刪除嗎？", "提示資料", function (result) {
                                if (result) {
                                    obj.parent().parent().remove(); //刪除節點
                                }
                            });
                        });
                        break;
                }
            });
        }

    </script>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
    <div class="navigation">
        <a href="javascript:history.go(-1);" class="back">後退</a>首頁 &gt; 圖片管理</div>
    <div id="contentTab">
        <ul class="tab_nav">
            <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">編輯圖片管理資料</a></li>
        </ul>
        <div class="tab_con" style="display: block;">
            <table class="form_table">
                <col width="180px">
                <col>
                <tbody>
                    <tr>
                        <th>
                            類型:
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlType" runat="server">
                                <asp:ListItem Text="首頁廣告圖" Value="22" />
                                <asp:ListItem Text="首頁Banner" Value="1" />
                                <asp:ListItem Text="出售" Value="2" />
                                <asp:ListItem Text="出租" Value="3" />
                                <asp:ListItem Text="空間規劃" Value="4" />
                                <asp:ListItem Text="帝光精品" Value="5" />
                                <asp:ListItem Text="便利生活" Value="6" />
                                <asp:ListItem Text="知識分享" Value="7" />
                                <asp:ListItem Text="VIP" Value="8" />
                                <asp:ListItem Text="市場需求" Value="9" />
                                <asp:ListItem Text="留言諮詢" Value="10" />
                                <asp:ListItem Text="3D空間規劃中間輪轉圖" Value="11" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            標題：
                        </th>
                        <td>
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="txtInput normal" minlength="2"
                                MaxLength="100"></asp:TextBox><label>*</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            排 序：
                        </th>
                        <td>
                            <asp:TextBox ID="txtSortId" runat="server" CssClass="txtInput normal small required number"></asp:TextBox><label>*</label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            link 地址：
                        </th>
                        <td>
                            <asp:TextBox ID="txtlink_url" runat="server" CssClass="txtInput"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th valign="top" style="padding-top: 10px;">
                            上傳圖片：
                        </th>
                        <td>
                            <asp:FileUpload ID="fileUpImage" runat="server" />
                        </td>
                    </tr>
                    <!--擴展屬性.開始-->
                    <asp:Literal ID="LitAttributeList" runat="server"></asp:Literal>
                    <!--擴展屬性.結束-->
                    <tr>
                        <th>
                        </th>
                        <td>
                            <asp:Button ID="btnSubmit" runat="server" Text="儲存送出" CssClass="btnSubmit" OnClick="btnSubmit_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
