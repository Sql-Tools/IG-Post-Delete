Imports System.Net, System.Text.RegularExpressions, System.Threading

Public Class Form1
    '[+]         Sql-Tools Open SRC !        [+]'
    '[+] Instagram Priv Api Used : 165.1 Ver [+]'
    '[+]         Sql-Tools Open SRC !        [+]'
    '[+] Instagram Priv Api Used : 165.1 Ver [+]'
#Region "Publics"
    Public StopDelete As Boolean : Public StopGet As Boolean
    Public PostsList As New List(Of String) : Public HttpHeader As New WebHeaderCollection
    Public Cookie As String : Public PostsCount As String
#End Region
#Region "Controls"
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Microsoft.VisualBasic.CompilerServices.ProjectData.EndApp()
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        With HttpHeader
            .Add("X-FB-HTTP-ENGINE: Liger")
            .Add("X-FB-Client-Ip: True")
            .Add(HttpRequestHeader.AcceptLanguage, "en-US")
            .Add("X-IG-connection-type: WIFI")
            .Add("X-IG-capabilities: 3brTvx8=")
            .Add("X-IG-app-id: 567067343352427")
        End With
        CheckForIllegalCrossThreadCalls = False
    End Sub
    Private Sub LoginB_Click(sender As Object, e As EventArgs) Handles LoginB.Click
        If LoginB.Text = "Login" Then
            Dim T As Thread = New Thread(New ThreadStart(Sub()
                                                             Dim LoginResp As String = Me.Login(UserBox.Text, PassBox.Text)
                                                             If LoginResp.Contains("logged_in_user") Then
                                                                 HttpHeader.Add(HttpRequestHeader.Cookie, Cookie) : HttpHeader.Add("X-Mid", Regex.Match(Cookie, "mid=(.*?);").Groups(1).Value)
                                                                 Me.PostsCount = GetPostsCount(Regex.Match(Cookie, "ds_user_id=(.*?);").Groups(1).Value) : LoginB.Text = "Start" : MsgBox("Done Login")
                                                             Else
                                                                 MsgBox($"Error : {LoginResp}")
                                                             End If
                                                         End Sub)) : T.Start()
        ElseIf LoginB.Text = "Start" Then
            Me.StopGet = False
            Dim T2 As Thread = New Thread(New ThreadStart(AddressOf GetPosts)) : T2.Start() : LoginB.Text = "Stop"
        ElseIf LoginB.Text = "Stop" Then
            Me.StopGet = True : LoginB.Text = "Start"
        End If
    End Sub
#End Region
#Region "Api"
    ''' <summary>
    ''' Get All Posts Id(s) On Your IG Profile
    ''' </summary>
    Sub GetPosts()
        Dim ProfId As String = Regex.Match(Cookie, "ds_user_id=(.*?);").Groups(1).Value
        Dim Uri As String = $"https://i.instagram.com/api/v1/feed/user/{ProfId}/?exclude_comment=true&only_fetch_first_carousel_media=false"
        While Not Me.StopGet
            If PostsList.Count = Me.PostsCount Then Me.StopGet = True : Me.StopDelete = False : Dim T As Thread = New Thread(New ThreadStart(AddressOf DeletePosts)) : T.Start() : Exit While
            Dim Resp As String = GetPosts(Uri)
            For Each M As Match In Regex.Matches(Resp, """taken_at"": (.*?), ""pk"": (.*?), """)
                If Not PostsList.Contains(M.Groups(2).Value) Then PostsList.Add(M.Groups(2).Value) : GrabbedC.Text = PostsList.Count 'منع تكرار :) | Duplicate Fix :)
            Next
            Dim MaxId As String = Regex.Match(Resp, """next_max_id"": ""(.*?)"", """).Groups(1).Value
            Uri = $"https://i.instagram.com/api/v1/feed/user/{ProfId}/?exclude_comment=true&max_id={MaxId}&only_fetch_first_carousel_media=false"
            Thread.Sleep(New Random().Next(1, 6) * New Random().Next(500, 1000)) 'Random Sleep
        End While
    End Sub
    ''' <summary>
    ''' Delete All Posts In List :)
    ''' </summary>
    Sub DeletePosts()
        While Not Me.StopDelete
            If Me.PostsList.Count = 1 Then Me.StopDelete = True : MsgBox($"Done Delete {PostsCount} Posts :)") : Exit While
            If Me.Delete(Me.PostsList(0)) Then Log.AppendText($"Done Delete > {Me.PostsList(0)}{vbCrLf}") : Me.PostsList.RemoveAt(0)
            Thread.Sleep(New Random().Next(10, 20) * New Random().Next(500, 1000)) 'Random Sleep
        End While
    End Sub
    ''' <summary>
    ''' Return (HttpRequestResponse) To Regex It In GetPosts() !
    ''' </summary>
    ''' <param name="Uri">We Must Get (MaxId) To Get All Post Pages So We Make It Changeble :)</param>
    Function GetPosts(Uri As String) As String
        Dim Resp As String = ""
        Try
            Dim AJ As HttpWebRequest = WebRequest.CreateHttp(Uri)
            With AJ
                .Headers = HttpHeader
                .Method = "GET"
                .Host = "i.instagram.com"
                .UserAgent = "Instagram 165.1.0.29.119 Android (29/10; 420dpi; 1080x2144; Xiaomi; Xiaomi Mi A2 Lite; daisy; qcom; en_US; 253447809)"
                .ContentType = "application/x-www-form-urlencoded; charset=UTF-8"
            End With
            Dim Response As HttpWebResponse = DirectCast(AJ.GetResponse, HttpWebResponse)
            Dim reader As New IO.StreamReader(Response.GetResponseStream) : Resp = reader.ReadToEnd : reader.Dispose() : reader.Close() : Response.Dispose() : Response.Close()
        Catch ex As WebException : Resp = New IO.StreamReader(ex.Response.GetResponseStream()).ReadToEnd() : End Try
        Return Resp
    End Function
    ''' <summary>
    ''' Return True If Post Deleted (Booleaned Function)
    ''' </summary>
    ''' <param name="PostId">The ID We Have Grabbed :)</param>
    Function Delete(PostId As String) As Boolean
        Dim IsDeleted As Boolean = False
        Try
            Dim Bytes As Byte() = New Text.UTF8Encoding().GetBytes("signed_body=SIGNATURE.{""igtv_feed_preview"":""false"",""media_id"":""" + PostId + """,""_csrftoken"":""" + Regex.Match(Cookie, "csrftoken=(.*?);").Groups(1).Value + """,""_uid"":""" + Regex.Match(Cookie, "ds_user_id=(.*?);").Groups(1).Value + """,""_uuid"":""" + Guid.NewGuid().ToString() + """}")
            Dim AJ As HttpWebRequest = WebRequest.CreateHttp($"https://i.instagram.com/api/v1/media/{PostId}/delete/?media_type=PHOTO")
            With AJ
                .Headers = HttpHeader
                .Method = "POST"
                .Host = "i.instagram.com"
                .UserAgent = "Instagram 165.1.0.29.119 Android (29/10; 420dpi; 1080x2144; Xiaomi; Xiaomi Mi A2 Lite; daisy; qcom; en_US; 253447809)"
                .ContentType = "application/x-www-form-urlencoded; charset=UTF-8"
            End With
            Dim Stream As IO.Stream = AJ.GetRequestStream() : Stream.Write(Bytes, 0, Bytes.Length) : Stream.Dispose() : Stream.Close()
            Dim Reader As New IO.StreamReader(DirectCast(AJ.GetResponse(), HttpWebResponse).GetResponseStream()) : Dim Text As String = Reader.ReadToEnd : Reader.Dispose() : Reader.Close()
            If Text.Contains("""did_delete"": true, """) Then IsDeleted = True Else IsDeleted = False
        Catch ex As WebException : IsDeleted = False : End Try
        Return IsDeleted
    End Function
    ''' <summary>
    ''' Return All Posts Count
    ''' </summary>
    ''' <param name="Id">Ds_User_Id</param>
    Function GetPostsCount(Id As String) As String
        Dim Count As String = String.Empty
        Try
            Dim AJ As HttpWebRequest = WebRequest.CreateHttp($"https://i.instagram.com/api/v1/users/{Id}/info/")
            With AJ
                .Headers = HttpHeader
                .Method = "GET"
                .Host = "i.instagram.com"
                .UserAgent = "Instagram 165.1.0.29.119 Android (29/10; 420dpi; 1080x2144; Xiaomi; Xiaomi Mi A2 Lite; daisy; qcom; en_US; 253447809)"
                .ContentType = "application/x-www-form-urlencoded; charset=UTF-8"
            End With
            Dim Response As HttpWebResponse = DirectCast(AJ.GetResponse, HttpWebResponse)
            Dim reader As New IO.StreamReader(Response.GetResponseStream) : Dim Resp As String = reader.ReadToEnd : reader.Dispose() : reader.Close() : Response.Dispose() : Response.Close()
            Count = Regex.Match(Resp, """media_count"": (.\w+), """).Groups(1).Value
        Catch ex As WebException : MsgBox("Error Grabbing Posts Count !") : End Try
        Return Count
    End Function
    ''' <summary>
    ''' Return HttpRequest Response String Get Right Responses In LoginB() !
    ''' </summary>
    ''' <param name="User">The User You Will Log In It</param>
    ''' <param name="Pass">The Pass Of Ur Username  :)</param>
    Function Login(User As String, Pass As String) As String
        Dim Resp As String = ""
        Try
            Dim uuid As String = Guid.NewGuid().ToString().ToLower()
            Dim _PostData As String = "{""phone_id"":""" + uuid + """,""username"":""" + User + """,""adid"":""" + uuid + """,""guid"":""" + uuid + """,""device_id"":""android-" + uuid.Split("-"c)(4) + """,""password"":""" + Pass + """,""login_attempt_count"":""1""}"
            Dim _HashedPostData As String = HashGen(_PostData, "5c4329e54efbd47469e127b11732a26abeccd975032fca3057ad1cdf3caf20cf")
            Dim _FinalPostData As String = "signed_body=" + Uri.EscapeDataString(_HashedPostData.ToLower() + "." + _PostData) + "&ig_sig_key_version=4"
            Dim Bytes As Byte() = New Text.UTF8Encoding().GetBytes(_FinalPostData)
            Dim AJ As HttpWebRequest = WebRequest.CreateHttp("https://i.instagram.com/api/v1/accounts/login/")
            With AJ
                .Method = "POST"
                .Headers.Add("X-IG-Capabilities: 3brTBw==")
                .Headers.Add("X-IG-App-ID: 567067343352427")
                .UserAgent = "Instagram 35.0.0.20.96 Android (25/7.1.2; 191dpi; 576x1024; samsung; SM-N975F; SM-N975F; intel; en_US; 95414346)"
                .Headers.Add("Accept-Language: en-US")
                .ContentType = "application/x-www-form-urlencoded; charset=UTF-8"
                .AutomaticDecompression = Net.DecompressionMethods.Deflate Or Net.DecompressionMethods.GZip
                .Host = "i.instagram.com"
                .Headers.Add("X-FB-HTTP-Engine: Liger")
                .KeepAlive = True
            End With
            Dim Stream As IO.Stream = AJ.GetRequestStream() : Stream.Write(Bytes, 0, Bytes.Length) : Stream.Dispose() : Stream.Close()
            Dim Reader As New IO.StreamReader(DirectCast(AJ.GetResponse(), Net.HttpWebResponse).GetResponseStream()) : Dim Text As String = Reader.ReadToEnd : Reader.Dispose() : Reader.Close()
#Region "Cookies"
            Dim CookieGrab As HttpWebResponse = DirectCast(AJ.GetResponse(), HttpWebResponse)
            Dim RegexSetting As String = CookieGrab.GetResponseHeader("Set-Cookie")
            Dim Csrftoken As String = Regex.Match(RegexSetting, "csrftoken=(.*?);").Groups(1).Value
            Dim Rur As String = Regex.Match(RegexSetting, "rur=(.*?);").Groups(1).Value
            Dim SessionId As String = Regex.Match(RegexSetting, "sessionid=(.*?);").Groups(1).Value
            Dim DsUserId As String = Regex.Match(RegexSetting, "ds_user_id=(.*?);").Groups(1).Value
            Dim DsUser As String = Regex.Match(RegexSetting, "ds_user=(.*?);").Groups(1).Value
            Me.Cookie = $"is_starred_enabled=yes; sessionid={SessionId}; ds_user={DsUser}; ds_user_id={DsUserId}; csrftoken={Csrftoken}; igfl={DsUser}; rur={Rur};"
#End Region
            Resp = Text
        Catch ex As WebException
            Resp = New IO.StreamReader(ex.Response.GetResponseStream()).ReadToEnd()
        End Try
        Return Resp
    End Function
#End Region
#Region "Hash"
    ''' <summary>
    ''' Sha256 Hash
    ''' </summary>
    ''' <param name="_PostString">PostData</param>
    ''' <param name="_Hash">Hash String</param>
    ''' <returns>Hashed PostData In Sha256(Format)</returns>
    Public Function HashGen(_PostString As String, _Hash As String) As String
        Dim Sha256 As System.Security.Cryptography.HMACSHA256 = New System.Security.Cryptography.HMACSHA256(System.Text.Encoding.UTF8.GetBytes(_Hash))
        Return BitConverter.ToString(Sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(_PostString))).Replace("-", "").ToLower()
    End Function
#End Region
    '[+]         Sql-Tools Open SRC !        [+]'
    '[+] Instagram Priv Api Used : 165.1 Ver [+]'
    '[+]         Sql-Tools Open SRC !        [+]'
    '[+] Instagram Priv Api Used : 165.1 Ver [+]'
End Class