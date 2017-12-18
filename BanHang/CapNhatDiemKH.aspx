<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CapNhatDiemKH.aspx.cs" Inherits="BanHang.CapNhatDiemKH" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server">
            <Items>
                <dx:LayoutGroup Caption="">
                    <Items>
                        <dx:LayoutItem Caption="Khách hàng">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxComboBox ID="cmbKhachHang" runat="server" DataSourceID="sqlKhachHang" TextField="TenKhachHang" ValueField="ID">
                                    </dx:ASPxComboBox>
                                    <asp:SqlDataSource ID="sqlKhachHang" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenKhachHang], [MaKhachHang] FROM [GPM_KhachHang] WHERE (([DaXoa] = @DaXoa) AND ([ID] &lt;&gt; @ID))">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                            <asp:Parameter DefaultValue="1" Name="ID" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Số tiền">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxSpinEdit ID="txtSoTien" runat="server" DisplayFormatString="N0">
                                    </dx:ASPxSpinEdit>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxButton ID="btnCapNhat" runat="server" OnClick="btnCapNhat_Click" Text="Cập nhật">
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:LayoutGroup>
            </Items>
        </dx:ASPxFormLayout>
    
    </div>
    </form>
</body>
</html>
