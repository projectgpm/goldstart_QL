 <%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="LichSuTruyCap.aspx.cs" Inherits="BanHang.LichSuTruyCap" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <dx:ASPxGridView ID="gridLichSuTruyCap" runat="server" AutoGenerateColumns="False" KeyFieldName="ID" Width="100%">
        <SettingsEditing Mode="PopupEditForm">
        </SettingsEditing>
        <Settings ShowFilterRow="True" ShowTitlePanel="True" />
       
       
<SettingsCommandButton>
<ShowAdaptiveDetailButton ButtonType="Image"></ShowAdaptiveDetailButton>

<HideAdaptiveDetailButton ButtonType="Image"></HideAdaptiveDetailButton>
</SettingsCommandButton>

       
        <SettingsSearchPanel Visible="True" />
        <SettingsText CommandDelete="Xóa" CommandEdit="Sửa" CommandNew="Thêm" ConfirmDelete="Bạn có chắc chắn muốn xóa không?" PopupEditFormCaption="Thông tin danh mục thuế" Title="DANH SÁCH LỊCH SỬ TRUY CẬP HỆ THỐNG" EmptyDataRow="Danh sách trống." SearchPanelEditorNullText="Nhập thông tin cần tìm..." />
        <Columns>
            <dx:GridViewDataComboBoxColumn Caption="Tên Nhân Viên" FieldName="IDNhanVien" VisibleIndex="1">
                <PropertiesComboBox DataSourceID="sqlNguoiDung" TextField="TenNguoiDung" ValueField="ID">
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataDateColumn Caption="Thời Gian" FieldName="Ngay" VisibleIndex="6">
                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy hh:mm:ss tt">
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn Caption="Thao Tác" FieldName="ThaoTac" VisibleIndex="3">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Hành Động" FieldName="HanhDong" VisibleIndex="5">
            </dx:GridViewDataTextColumn>
        </Columns>
        <Styles>
            <Header Font-Bold="True" HorizontalAlign="Center">
            </Header>
            <AlternatingRow Enabled="True">
            </AlternatingRow>
            <TitlePanel Font-Bold="True" HorizontalAlign="Left">
            </TitlePanel>
        </Styles>
    </dx:ASPxGridView>
<asp:SqlDataSource ID="sqlNhomNguoiDung" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenNhom] FROM [GPM_NhomNguoiDung]">
    <SelectParameters>
        <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqlNguoiDung" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenNguoiDung] FROM [GPM_NguoiDung] WHERE ([DaXoa] = @DaXoa)">
    <SelectParameters>
        <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
</asp:Content>
