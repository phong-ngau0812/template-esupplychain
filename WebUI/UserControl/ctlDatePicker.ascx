<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlDatePicker.ascx.cs" Inherits="UserControl_ctlDatePicker" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<link href="../../css/telerik.css" rel="stylesheet" type="text/css" />
<telerik:RadScriptManager runat="server" ID="src"></telerik:RadScriptManager>
<div class="row" id="DateP">
    <div class="col-lg-2 col-xs-12">
        <label class="mb-3">Thời gian:</label>
        <telerik:RadComboBox ID="cboDateType" runat="server" Skin="MetroTouch" AutoPostBack="True" OnSelectedIndexChanged="cboDateType_SelectedIndexChanged">
            <Items>
                <telerik:RadComboBoxItem runat="server" Text="Tùy chọn" Value="0" />
                <telerik:RadComboBoxItem runat="server" Text="Hôm nay" Value="5" />
                <telerik:RadComboBoxItem runat="server" Text="Tuần này" Value="1" />
                <telerik:RadComboBoxItem runat="server" Text="Tuần trước" Value="2" />
                <telerik:RadComboBoxItem runat="server" Text="Tháng này" Value="3" />
                <telerik:RadComboBoxItem runat="server" Text="Tháng trước" Value="4" />
                <telerik:RadComboBoxItem runat="server" Text="Tất cả" Value="-1" />
            </Items>
        </telerik:RadComboBox>
    </div>
    <div class="col-lg-2 col-xs-12">
        <label class="mb-3">Từ ngày:</label>
        <telerik:RadDatePicker ID="dpkFromDate" AutoPostBack="true" Skin="MetroTouch" runat="server" MaxDate="9999-12-31" MinDate="1900-01-01" OnSelectedDateChanged="dpkFromDate_SelectedDateChanged" >
            <DateInput DateFormat="dd/MM/yyyy">
            </DateInput>
        </telerik:RadDatePicker>
    </div>
    <div class="col-lg-2 col-xs-12">
        <label class="mb-3">Đến ngày:</label>
        <telerik:RadDatePicker ID="dpkToDate" AutoPostBack="true" Skin="MetroTouch" runat="server" MaxDate="9999-12-31" MinDate="1900-01-01" OnSelectedDateChanged="dpkToDate_SelectedDateChanged">
            <DateInput DateFormat="dd/MM/yyyy">
            </DateInput>
        </telerik:RadDatePicker>
    </div>
</div>



