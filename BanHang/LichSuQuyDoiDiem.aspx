<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="LichSuQuyDoiDiem.aspx.cs" Inherits="BanHang.LichSuQuyDoiDiem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%">
        <Items>
            <dx:LayoutGroup Caption="">
                <Items>
                    <dx:LayoutItem Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxGridView ID="gridLichSuTruyCap" runat="server" AutoGenerateColumns="False" KeyFieldName="ID" Width="100%" DataSourceID="sqlLichSuQuyDoiDiem">
                                    <SettingsEditing Mode="PopupEditForm">
                                    </SettingsEditing>
                                    <Settings ShowFilterRow="True" ShowTitlePanel="True" />
       
       
                                    <SettingsCommandButton>
                                    <ShowAdaptiveDetailButton ButtonType="Image"></ShowAdaptiveDetailButton>

                                    <HideAdaptiveDetailButton ButtonType="Image"></HideAdaptiveDetailButton>
                                    </SettingsCommandButton>

       
                                    <SettingsSearchPanel Visible="True" />
                                    <SettingsText CommandDelete="Xóa" CommandEdit="Sửa" CommandNew="Thêm" ConfirmDelete="Bạn có chắc chắn muốn xóa không?" PopupEditFormCaption="Thông tin" Title="DANH SÁCH LỊCH SỬ THAY ĐỔI ĐIỂM" EmptyDataRow="Danh sách trống." SearchPanelEditorNullText="Nhập thông tin cần tìm..." />
                                    <Columns>
                                        <dx:GridViewDataComboBoxColumn Caption="Khách Hàng" FieldName="IDKhachHang" VisibleIndex="0">
                                            <PropertiesComboBox DataSourceID="sqlKhachHang" TextField="TenKhachHang" ValueField="ID">
                                            </PropertiesComboBox>
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewDataTextColumn Caption="Nội Dung" FieldName="NoiDung" VisibleIndex="4">
                                            <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy hh:mm:ss tt">
                                            </PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn Caption="Ngày" FieldName="Ngay" VisibleIndex="5">
                                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy hh:mm" DisplayFormatInEditMode="True" EditFormat="DateTime" EditFormatString="dd/MM/yyyy hh:mm">
                                            </PropertiesDateEdit>
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataSpinEditColumn Caption="Điểm Củ" FieldName="SoDiemCu" ShowInCustomizationForm="True" VisibleIndex="1">
                                            <PropertiesSpinEdit DisplayFormatInEditMode="True" DisplayFormatString="{0:00.0}" NumberFormat="Custom">
                                            </PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn Caption="Điểm Mới" FieldName="SoDiemMoi" ShowInCustomizationForm="True" VisibleIndex="3">
                                            <PropertiesSpinEdit DisplayFormatInEditMode="True" DisplayFormatString="{0:00.0}" NumberFormat="Custom">
                                            </PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
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
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>
    
    <asp:SqlDataSource ID="sqlLichSuQuyDoiDiem" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT * FROM [GPM_LichSuQuyDoiDiem] ORDER BY [ID] DESC"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlKhachHang" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenKhachHang] FROM [GPM_KhachHang]"></asp:SqlDataSource>
</asp:Content>
