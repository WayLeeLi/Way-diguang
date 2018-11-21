<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="horse.ascx.cs" Inherits="DTcms.Web.Usercontrol.horse" %>
<div class="str1 str_wrap" style="height: 45px;">
    <asp:Repeater ID="rptList" runat="server">
        <ItemTemplate>
            <p><a href='<%#Eval("link_url") %>'><%#Eval("title") %></a></p>
        </ItemTemplate>
    </asp:Repeater>
</div>
