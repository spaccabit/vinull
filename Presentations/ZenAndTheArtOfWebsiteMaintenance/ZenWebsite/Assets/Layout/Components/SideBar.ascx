<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SideBar.ascx.cs" Inherits="Assets_Layout_Components_SideBar" %>
<!-- start sidebar -->
<div id="sidebar">
    <div id="search">
        <asp:LoginView ID="LoginView1" runat="server">
            <AnonymousTemplate>
                <h2>
                    User Login</h2>
                <div id="searchform">
                    <asp:Login ID="Login1" runat="server" CreateUserUrl="~/NewUser.aspx">
                        <LayoutTemplate>
                            <asp:TextBox ID="UserName" runat="server" Width="85px"></asp:TextBox>
                            <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="85px"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                Display="Dynamic" ErrorMessage="User Name is required." ToolTip="User Name is required."
                                ValidationGroup="ctl00$Login1">User Name is required.<br /></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                Display="Dynamic" ErrorMessage="Password is required." ToolTip="Password is required."
                                ValidationGroup="ctl00$Login1">Password is required.<br /></asp:RequiredFieldValidator>
                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                            <asp:Button ID="LoginButton" runat="server" Width="65px" CommandName="Login" Text="Log In"
                                ValidationGroup="ctl00$Login1" />
                            <asp:Button ID="RegisterButton" runat="server" Width="65px" CommandName="CreateUser"
                                Text="Register" CausesValidation="false" PostBackUrl="~/NewUser.aspx" />
                        </LayoutTemplate>
                    </asp:Login>
                </div>
            </AnonymousTemplate>
            <LoggedInTemplate>
                <h2>Welcome!</h2>
                <div id="searchform">
                    Logged on as: <asp:LoginName ID="LoginName1" runat="server" />
                    <asp:LoginStatus ID="LoginStatus1" runat="server" />
                </div>
            </LoggedInTemplate>
        </asp:LoginView>
    </div>
    <ul>
        <li id="calendar">
            <h2>
                Calendar</h2>
            <div id="calendar_wrap">
                <table id="wp-calendar" summary="Calendar">
                    <caption>
                        August 2007
                    </caption>
                    <thead>
                        <tr>
                            <th abbr="Monday" scope="col" title="Monday">
                                M</th>
                            <th abbr="Tuesday" scope="col" title="Tuesday">
                                T</th>
                            <th abbr="Wednesday" scope="col" title="Wednesday">
                                W</th>
                            <th abbr="Thursday" scope="col" title="Thursday">
                                T</th>
                            <th abbr="Friday" scope="col" title="Friday">
                                F</th>
                            <th abbr="Saturday" scope="col" title="Saturday">
                                S</th>
                            <th abbr="Sunday" scope="col" title="Sunday">
                                S</th>
                        </tr>
                    </thead>
                    <tfoot>
                        <tr>
                            <td abbr="July" colspan="3" id="prev">
                                <a href="#">&laquo; Jul</a></td>
                            <td class="pad">
                                &nbsp;</td>
                            <td abbr="September" colspan="3" id="next" class="pad">
                                <a href="#">Sep &raquo;</a></td>
                        </tr>
                    </tfoot>
                    <tbody>
                        <tr>
                            <td colspan="2" class="pad">
                                &nbsp;</td>
                            <td>
                                1</td>
                            <td>
                                2</td>
                            <td>
                                3</td>
                            <td>
                                4</td>
                            <td>
                                5</td>
                        </tr>
                        <tr>
                            <td>
                                6</td>
                            <td>
                                7</td>
                            <td>
                                8</td>
                            <td>
                                9</td>
                            <td>
                                10</td>
                            <td>
                                11</td>
                            <td>
                                12</td>
                        </tr>
                        <tr>
                            <td>
                                13</td>
                            <td>
                                14</td>
                            <td>
                                15</td>
                            <td>
                                16</td>
                            <td>
                                17</td>
                            <td>
                                18</td>
                            <td>
                                19</td>
                        </tr>
                        <tr>
                            <td>
                                20</td>
                            <td>
                                21</td>
                            <td>
                                22</td>
                            <td>
                                23</td>
                            <td id="today">
                                24</td>
                            <td>
                                25</td>
                            <td>
                                26</td>
                        </tr>
                        <tr>
                            <td>
                                27</td>
                            <td>
                                28</td>
                            <td>
                                29</td>
                            <td>
                                30</td>
                            <td>
                                31</td>
                            <td class="pad" colspan="2">
                                &nbsp;</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </li>
        <li>
            <h2>
                Recent Posts</h2>
            <ul>
                <li><a href="#">Code Post</a></li>
                <li><a href="#">Praesent scelerisque scelerisque</a></li>
                <li><a href="#">Ut nonummy rutrum sem</a></li>
                <li><a href="#">Pellentesque tempus quam quis</a></li>
                <li><a href="#">Fusce ultrices fringilla metus</a></li>
                <li><a href="#">Praesent mattis condimentum</a></li>
            </ul>
        </li>
    </ul>
</div>
<!-- end sidebar -->
